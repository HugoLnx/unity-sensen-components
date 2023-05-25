using LnxArch;
using SensenToolkit.Commons;
using UnityEngine;

namespace Gtv3rdCombatAndTraversal
{
    public class FadingForce : MonoBehaviour
    {
        [SerializeField] private float _fadingSpeed = 1f;
        private FrameMovement _frameMovement;
        private Vector3 _velocity;

        [Autofetch]
        private void Prepare(FrameMovement frameMovement)
        {
            _frameMovement = frameMovement;
        }

        public void ApplyForce(Vector3 force)
        {
            _velocity += force;
        }

        private void Update()
        {
            _frameMovement.Value += _velocity * Time.deltaTime;
        }

        private void FixedUpdate()
        {
            _velocity = Vector3.MoveTowards(_velocity, Vector3.zero, _fadingSpeed * Time.fixedDeltaTime);
        }
    }
}
