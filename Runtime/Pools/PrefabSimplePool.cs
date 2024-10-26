using UnityEngine;

namespace Sensen.Components
{
    public abstract class PrefabSimplePool<TPool, TPrefab> : PrefabPoolBase<TPool, TPrefab, TPrefab>
    where TPrefab : Component
    where TPool : PrefabSimplePool<TPool, TPrefab>
    {
        protected override TPrefab InstantiateNew()
        {
            TPrefab instance = Instantiate(_prefab, this.transform);
            instance.name = $"[{Creations.Count + 1}] {_prefab.name}";
            return instance;
        }
    }
}
