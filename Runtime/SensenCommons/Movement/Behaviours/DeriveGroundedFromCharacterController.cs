using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class DeriveGroundedFromCharacterController : LnxComponentDerivation<IsGrounded, bool>
    {
        protected override bool SyncWhenWritingOnTarget => false;
        private CharacterController _controller;

        [Autofetch]
        private void Prepare(CharacterController controller)
        {
            _controller = controller;
        }

        protected override bool ReadReplacement()
        {
            return _controller.isGrounded;
        }

        protected override void SyncOnWrite(bool valueWritten)
        {
            // Can't force it to be grounded
            throw new System.NotImplementedException();
        }
    }
}
