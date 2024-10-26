using System.Collections.Generic;
using UnityEngine;
using LnxArch;
using SensenToolkit;

namespace Sensen.Components
{
    public abstract class PrefabPoolBase<TPool, TPooled, TPrefab> : ATransientSingleton<TPool>, IReleasablePool<TPooled>
    where TPrefab : Component
    where TPool : PrefabPoolBase<TPool, TPooled, TPrefab>
    {
        [SerializeField] protected TPrefab _prefab;
        [SerializeField] protected int _minSize = 20;
        [SerializeField] protected int _maxCreations = 50;
        private IReleasablePool<TPooled> _pool;
        public HashSet<TPooled> Creations => _pool?.Creations;

        protected override void AwakeSingleton()
        {
            base.AwakeSingleton();
            SimpleExpandablePool<TPooled> pool = new(
                factory: InstantiateNew,
                minSize: _minSize,
                maxCreations: _maxCreations,
                prefill: false
            );
            _pool = pool;
            pool.Prefill();
        }

        public TPooled Get()
        {
            return _pool.Get();
        }

        public virtual void Release(TPooled resource)
        {
            _pool.Release(resource);
        }

        protected abstract TPooled InstantiateNew();
    }
}
