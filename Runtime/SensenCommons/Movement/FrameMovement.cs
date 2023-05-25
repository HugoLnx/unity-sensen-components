using System;
using System.Collections;
using System.Collections.Generic;
using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    [DefaultExecutionOrder(10000)]
    public class FrameMovement : LnxComponent<Vector3>
    {
        public event Action<Vector3> OnApply;
        private void Update()
        {
            OnApply?.Invoke(Value);
            Value = Vector3.zero;
        }
    }
}
