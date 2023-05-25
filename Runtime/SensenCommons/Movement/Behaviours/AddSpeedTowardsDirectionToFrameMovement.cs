using System.Collections;
using System.Collections.Generic;
using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class AddSpeedTowardsDirectionToFrameMovement : MonoBehaviour
    {
        #region Properties
        private Direction3D _direction3D;
        private MovementSpeed _speed;
        private FrameMovement _frameMovement;
        #endregion

        #region Constructors
        [Autofetch]
        private void Prepare(
            Direction3D direction3D,
            MovementSpeed speed,
            FrameMovement frameMovement)
        {
            _direction3D = direction3D;
            _speed = speed;
            _frameMovement = frameMovement;
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
            _frameMovement.Value += _speed.Value * Time.deltaTime * _direction3D.Value;
        }
        #endregion
    }
}
