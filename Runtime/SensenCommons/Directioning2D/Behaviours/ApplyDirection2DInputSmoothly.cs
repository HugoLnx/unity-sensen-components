using System;
using System.Collections;
using System.Collections.Generic;
using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class ApplyDirection2DInputSmoothly : MonoBehaviour
    {
        #region Properties
        [SerializeField] private bool _relativeToCamera;
        [SerializeField] private float _rotationSpeed;
        private Direction2DInput _input;
        private Direction2D _direction;
        private MainCameraForward2D _cameraForward;
        private Vector2 CameraForward => _cameraForward.Value.normalized;
        private Vector2 CameraRight => CameraForward.RotateBy(-90f);
        #endregion

        #region Constructors
        [Autofetch]
        private void Prepare(
            Direction2DInput input,
            Direction2D direction,
            MainCameraForward2D cameraForward = null)
        {
            _input = input;
            _direction = direction;
            _cameraForward = cameraForward;
        }
        #endregion

        #region Methods
        public void Enable()
        {
            this.enabled = true;
        }

        public void Disable()
        {
            this.enabled = false;
        }
        #endregion

        #region Lifecycle
        private void Update()
        {
            Vector2 inputValue = _input.Value.normalized;
            if (inputValue != Vector2.zero)
            {
                Vector2 targetDirection = InputToTargetDirection(inputValue);
                _direction.Value = RotateDirectionSmoothlyTowards(
                    current: _direction.Value,
                    target: targetDirection,
                    speed: _rotationSpeed,
                    deltaTime: Time.deltaTime,
                    preferredDirection: CameraForward
                );
            }
        }

        private static Vector2 RotateDirectionSmoothlyTowards(
            Vector2 current, Vector2 target, float speed, float deltaTime, Vector2 preferredDirection)
        {
            if (speed <= 0f) return target;

            return current.RotateTowards(
                target,
                maxRadiansDelta: speed * deltaTime * Mathf.Deg2Rad,
                preferredDirection: preferredDirection
            );
        }

        private Vector2 InputToTargetDirection(Vector2 inputValue)
        {
            if (_relativeToCamera)
            {
                return (CameraForward * inputValue.y) + (CameraRight * inputValue.x);
            }
            else
            {
                return inputValue;
            }
        }
        #endregion
    }
}
