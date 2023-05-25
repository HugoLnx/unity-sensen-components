using System.Collections;
using System.Collections.Generic;
using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class SpeedTransitioner : MonoBehaviour
    {
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deacceleration;
        private MovementSpeed _speed;
        private MovementTargetSpeed _target;

        private float TransitionAcceleration => _speed.Value > _target.TargetValue ? _deacceleration : _acceleration;

        [Autofetch]
        private void Prepare(
            MovementSpeed speed,
            MovementTargetSpeed target)
        {
            _speed = speed;
            _target = target;
        }

        private void Update()
        {
            _speed.Value = Mathf.MoveTowards(
                _speed.Value,
                _target.TargetValue,
                TransitionAcceleration * Time.deltaTime
            );
        }
    }
}
