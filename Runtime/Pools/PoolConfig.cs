using System;

namespace Sensen.Components
{
    [Serializable]
    public struct PoolConfig
    {
        public int MinSize;
        public int MaxCreations;
        public bool Prefill;
    }
}
