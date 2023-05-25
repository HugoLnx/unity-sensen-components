using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class MainCamera : LnxComponent<Camera>
    {
        private Camera _camera;

        public override Camera PlainValue {
            get => _camera ??= Camera.main;
            set => throw new System.NotImplementedException();
        }
    }
}
