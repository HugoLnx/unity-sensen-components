using System;
using System.Collections;
using System.Collections.Generic;
using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class AddGravityToVelocity3D : MonoBehaviour
    {
        private MovementVelocity3D _velocity;
        private IsGrounded _grounded;

        [Autofetch]
        private void Prepare(MovementVelocity3D velocity, IsGrounded grounded = null)
        {
            _velocity = velocity;
            _grounded = grounded;
        }

        private void FixedUpdate()
        {
            float deltaGravity = Physics.gravity.y * Time.fixedDeltaTime;
            if (_grounded.Value)
            {
                _velocity.Value = _velocity.Value.Clone(y: deltaGravity);
            }
            else
            {
                _velocity.Value += Vector3.up * deltaGravity;
            }
        }
    }
}
