using System.Collections;
using System.Collections.Generic;
using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class Directioning2D : MonoBehaviour
    {
        #region Properties
        [SerializeField] private float _rotateSpeed = 180f;
        private Direction2D _direction;
        private Vector2 _targetDirection;
        #endregion

        #region Initialization
        [Autofetch]
        private void Prepare(Direction2D direction)
        {
            _direction = direction;
        }
        #endregion

        #region PublicMethods
        public void TurnTo(Vector2 direction)
        {
            _targetDirection = direction.normalized;
        }
        #endregion

        #region Lifecycle
        private void Update()
        {
            float angle = _rotateSpeed * Time.deltaTime * Mathf.Deg2Rad;
            _direction.Value = _direction.Value.RotateTowards(_targetDirection, maxRadiansDelta: angle);
        }
        #endregion
    }
}
