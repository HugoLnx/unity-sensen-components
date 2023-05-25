using System;
using System.Collections;
using System.Collections.Generic;
using LnxArch;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SensenToolkit.Commons
{
    public class DeriveDirection2DInputFromInputActions : MonoBehaviour
    {
#region Properties
        [SerializeField] private InputActionReference _directionActionReference;
        private Direction2DInput _input;

        private InputActionAsset ActionsAsset => ActionMap.asset;
        private InputActionMap ActionMap => DirectionAction.actionMap;
        private InputAction DirectionAction => _directionActionReference.action;
#endregion

#region Constructors
    [Autofetch]
    private void Prepare(Direction2DInput input)
    {
        _input = input;
        ActionsAsset.Enable();
        ActionMap.Enable();
        DirectionAction.performed += _ => _input.Value = ReadDirectionInput();
        DirectionAction.canceled += _ => _input.Value = Vector2.zero;
    }

#endregion

#region Private
        private Vector2 ReadDirectionInput()
        {
            return DirectionAction.ReadValue<Vector2>();
        }
#endregion
    }
}
