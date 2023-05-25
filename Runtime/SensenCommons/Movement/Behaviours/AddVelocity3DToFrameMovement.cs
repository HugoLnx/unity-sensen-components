using System.Collections;
using System.Collections.Generic;
using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class AddVelocity3DToFrameMovement : MonoBehaviour
    {
        private MovementVelocity3D _movementVelocity3D;
        private FrameMovement _frameMovement;

        [Autofetch]
        private void Prepare(MovementVelocity3D movementVelocity3D, FrameMovement frameMovement)
        {
            _movementVelocity3D = movementVelocity3D;
            _frameMovement = frameMovement;
        }

        private void Update()
        {
            _frameMovement.Value += _movementVelocity3D.Value * Time.deltaTime;
        }
    }
}
