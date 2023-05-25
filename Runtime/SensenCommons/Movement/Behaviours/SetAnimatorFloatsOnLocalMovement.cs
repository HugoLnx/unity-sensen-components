using System;
using System.Collections;
using System.Collections.Generic;
using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class SetAnimatorFloatsOnLocalMovement : MonoBehaviour
    {
        [SerializeField] private string _xParameterName = "localMovementX";
        [SerializeField] private string _yParameterName = "localMovementY";
        private Direction2D _direction2D;
        private FrameMovement _frameMovement;
        private Animator _animator;
        private int? _xHash;
        private int? _yHash;

        private int XHash => _xHash ??= Animator.StringToHash(_xParameterName);
        private int YHash => _yHash ??= Animator.StringToHash(_yParameterName);

        [Autofetch]
        private void Prepare(Direction2D direction2D, FrameMovement frameMovement, Animator animator)
        {
            _direction2D = direction2D;
            _frameMovement = frameMovement;
            _animator = animator;
            _frameMovement.OnApply += OnApplyMovement;
        }

        private void OnApplyMovement(Vector3 movement)
        {
            Vector2 movementDirection2D = movement.XZ().normalized;
            Vector2 localMovement2D = movementDirection2D.AsLocalOf(_direction2D.Value);
            _animator.SetFloat(XHash, localMovement2D.x);
            _animator.SetFloat(YHash, localMovement2D.y);
        }
    }
}
