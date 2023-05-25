using System.Collections;
using System.Collections.Generic;
using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class SetAnimatorBoolOnDirectioning : LnxComponentDerivation<Direction2DInput, Vector2>
    {
#region Properties
        [SerializeField] private string _boolParameterName = "isMoving";
        private Animator _animator;
        private int? _parameterHash;

        protected override bool ReplaceReadingOnTarget => false;
        private int ParameterHash => _parameterHash ??= Animator.StringToHash(_boolParameterName);
#endregion

#region Constructors
        [Autofetch]
        private void Prepare(Animator animator)
        {
            _animator = animator;
        }
#endregion

#region Private
        protected override void SyncOnWrite(Vector2 valueWritten)
        {
            _animator.SetBool(ParameterHash, valueWritten != Vector2.zero);
        }
        protected override Vector2 ReadReplacement()
        {
            throw new System.NotImplementedException();
        }
#endregion
    }
}
