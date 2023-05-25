using System;
using System.Collections;
using System.Collections.Generic;
using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class SetAnimatorSpeedTransition : MonoBehaviour
    {
#region Properties
        [SerializeField] private string _parameterName;
        private Animator _animator;
        private MovementSpeedTransition _transition;
        private int? _parameterId;

        private int ParameterId => _parameterId ??= Animator.StringToHash(_parameterName);
#endregion

#region Constructors
        [Autofetch]
        private void Prepare(
            Animator animator,
            MovementSpeedTransition transition)
        {
            _animator = animator;
            _transition = transition;

            _transition.OnChange += (_, _, _) => OnTransitionChanged(_transition.Value);
        }

        private void OnTransitionChanged(float value)
        {
            _animator.SetFloat(ParameterId, value);
        }
        #endregion
    }
}
