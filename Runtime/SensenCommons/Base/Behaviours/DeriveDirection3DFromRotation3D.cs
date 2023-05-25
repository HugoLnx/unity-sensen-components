using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class DeriveDirection3DFromRotation3D : LnxComponentPairDerivation<Direction3D, Rotation3D, Vector3, Quaternion>
    {
        protected override Quaternion SyncOnWrite(Vector3 newDirection, Direction3D _)
        {
            return Quaternion.LookRotation(newDirection);
        }

        protected override Vector3 ReadReplacement(Rotation3D rotation)
        {
            return rotation.Value * Vector3.forward;
        }
    }
}
