using System;
using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class MovementSpeedTransition : LnxComponent<float>
    {
#region Properties
        private MovementSpeed _speed;
        private MovementTargetSpeed _target;
        private float _lastValue;

        public override float PlainValue
        {
            get => Mathf.InverseLerp(_target.InitialValue, _target.FinalValue, _speed.Value);
            set => _speed.Value = Mathf.Lerp(_target.InitialValue, _target.FinalValue, value);
        }
#endregion

#region Constructors
        [Autofetch]
        private void Prepare(MovementSpeed speed, MovementTargetSpeed target)
        {
            _speed = speed;
            _target = target;
            _lastValue = PlainValue;
            _speed.OnWrite += (_, _) => OnDependencyWrite();
            _target.OnWrite += (_, _) => OnDependencyWrite();
        }

        private void OnDependencyWrite()
        {
            float plainValue = PlainValue;
            EmitWrite(plainValue);
            if (PlainValue != _lastValue)
            {
                EmitChange(_lastValue, plainValue);
            }
            _lastValue = plainValue;
        }
        #endregion
    }
}
