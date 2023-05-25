using System;
using System.Collections;
using System.Collections.Generic;
using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class SetAnimatorFloatsOnDirectioning : MonoBehaviour
    {
        [SerializeField] private string _directionXName = "directionX";
        [SerializeField] private string _directionYName = "directionY";
        private Direction2D _direction2D;
        private Animator _animator;
        private int? _directionXHash;
        private int? _directionYHash;

        private int DirectionXHash => _directionXHash ??= Animator.StringToHash(_directionXName);
        private int DirectionYHash => _directionYHash ??= Animator.StringToHash(_directionYName);

        [Autofetch]
        private void Prepare(Direction2D direction2D, Animator animator)
        {
            _direction2D = direction2D;
            _animator = animator;
            _direction2D.OnChange += (_, newValue, _) => OnChangeDirection(newValue);
        }

        private void OnChangeDirection(Vector2 direction)
        {
            _animator.SetFloat(DirectionXHash, direction.x);
            _animator.SetFloat(DirectionYHash, direction.y);
        }
    }
}
