using System.Collections;
using System.Collections.Generic;
using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class ApplyFrameMovementWithCharacterController : MonoBehaviour
    {
        private CharacterController _controller;

        [Autofetch]
        private void Prepare(FrameMovement frameMovement, CharacterController controller)
        {
            _controller = controller;
            frameMovement.OnApply += OnApply;
        }

        private void OnApply(Vector3 movement)
        {
            _controller.Move(movement);
        }
    }
}
