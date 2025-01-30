using System;
using System.Collections.Generic;
using MyBox;
using SensenToolkit;
using UnityEngine;

namespace Sensen.Components
{
    [Serializable]
    public struct PredefinedPoolConfig
    {
        public Component Prefab;
        public PoolConfig Config;
    }

    public class PrefabPools : ATransientSingleton<PrefabPools>
    {
        [SerializeField]
        private PoolConfig _defaultConfig = new()
        {
            MinSize = 15,
            MaxCreations = 50,
            Prefill = true
        };
        [SerializeField, InitializationField] private PredefinedPoolConfig[] _predefinedPoolsConfig;
        private Dictionary<Component, SimpleExpandablePool<Component>> _pools = new();

        public delegate void OnPoolCreatedAction(Component prefab, SimpleExpandablePool<Component> pool);
        private event OnPoolCreatedAction OnPoolCreated = delegate { };

        protected override void AwakeSingleton()
        {
            base.AwakeSingleton();
            foreach (PredefinedPoolConfig c in _predefinedPoolsConfig)
            {
                AddPool(c.Prefab, c.Config);
            }
        }

        public void ExecuteOncePerPool(OnPoolCreatedAction action)
        {
            foreach ((Component prefab, SimpleExpandablePool<Component> pool) in _pools)
            {
                action(prefab, pool);
            }
            OnPoolCreated += action;
        }

        public T GetInstanceOf<T>(T prefab) where T : Component
        {
            return EnsurePool(prefab).Get() as T;
        }

        public void ReleaseInstanceOf<T>(T prefab, T instance) where T : Component
        {
            EnsurePool(prefab).Release(instance);
        }

        private SimpleExpandablePool<Component> EnsurePool(Component prefab)
        {
            if (_pools.TryGetValue(prefab, out SimpleExpandablePool<Component> pool))
            {
                return pool;
            }

            return AddPool(prefab, _defaultConfig);
        }

        private SimpleExpandablePool<Component> AddPool(Component prefab, PoolConfig config)
        {
            if (_pools.ContainsKey(prefab))
            {
                throw new ArgumentException($"Pool for {prefab.name} was added twice.");
            }
            GameObject container = new($"{prefab.name} Pool Container");
            container.transform.SetParent(this.transform);
            SimpleExpandablePool<Component> pool = new(
                factory: (SimpleExpandablePool<Component> pool) =>
                {
                    Component instance = Instantiate(prefab, container.transform);
                    instance.name = $"[{pool.Creations.Count + 1}] {prefab.name}";
                    return instance;
                },
                minSize: config.MinSize,
                maxCreations: config.MaxCreations,
                prefill: config.Prefill
            );
            _pools.Add(prefab, pool);
            OnPoolCreated.Invoke(prefab, pool);
            return pool;
        }
    }
}
