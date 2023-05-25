using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class DeriveDirection2DFromDirection3D : LnxComponentPairDerivation<Direction2D, Direction3D, Vector2, Vector3>
    {
        protected override Vector3 SyncOnWrite(Vector2 newDirection, Direction2D _)
        {
            return newDirection.X0Y();
        }

        protected override Vector2 ReadReplacement(Direction3D direction3d)
        {
            return direction3d.Value.XZ();
        }
    }
}
