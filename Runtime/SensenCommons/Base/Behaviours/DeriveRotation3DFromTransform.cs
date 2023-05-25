using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class DeriveRotation3DFromTransform : LnxComponentDerivation<Rotation3D, Quaternion>
    {
        protected override void SyncOnWrite(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        protected override Quaternion ReadReplacement()
        {
            return transform.rotation;
        }
    }
}
