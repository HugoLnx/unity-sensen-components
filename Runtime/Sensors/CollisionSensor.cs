using UnityEngine;

namespace SensenToolkit.Sensors
{
    public class CollisionSensor : SensorBase
    {
        private void OnCollisionEnter(Collision collision)
        {
            OnEnter(collision.collider);
        }

        private void OnCollisionExit(Collision collision)
        {
            OnExit(collision.collider);
        }
    }
}
