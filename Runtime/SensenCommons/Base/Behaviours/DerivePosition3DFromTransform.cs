using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class DerivePosition3DFromTransform : LnxComponentDerivation<Position3D, Vector3>
    {
        protected override void SyncOnWrite(Vector3 position)
        {
            transform.position = position;
        }

        protected override Vector3 ReadReplacement()
        {
            return transform.position;
        }
    }
}
