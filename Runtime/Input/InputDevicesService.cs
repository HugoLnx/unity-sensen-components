using System.Collections;
using System.Collections.Generic;
using LnxArch;
using UnityEngine;
using InControl;
using System;
using SensenToolkit.Observables;

namespace Sensen.Components
{
    [LnxService(Persistent = true)]
    public class InputDevicesService : MonoBehaviour
    {
        private readonly Dictionary<string, DeviceActivationRequestObserver> _activationObservers = new();
        private readonly Dictionary<string, LnxInputDevice> _activeDevices = new();
        private UpdateObservable _updateObservable;

        public delegate void ActivationRequested(LnxInputDevice device, Action finishActivation);
        public event ActivationRequested OnActivationRequested;
        public event Action<LnxInputDevice> OnDetached;

        [LnxInit]
        private void Init(UpdateObservable updateObservable)
        {
            _updateObservable = updateObservable;
            SubscribeToInControlAttachEvents();
        }

        private void AttachDevice(LnxInputDevice device)
        {
            Debug.Log($"Device ATTACHED waiting to be Activated: {device.Id}");
            DeviceActivationRequestObserver activationObserver = new((LnxInputDevice)device, _updateObservable);
            activationObserver.OnActivationRequest += RequestActivation;
            _activationObservers.Add(device.Id, activationObserver);
        }

        private void DetachDevice(LnxInputDevice device)
        {
            Debug.Log($"Device DETACHED: {device.Id}");
            OnDetached?.Invoke(device);
        }

        private void RequestActivation(LnxInputDevice device)
        {
            Debug.Log($"Device ACTIVATION REQUESTED by {device.Id}");
            OnActivationRequested?.Invoke(device, () => FinishActivation(device));
        }

        private void FinishActivation(LnxInputDevice device)
        {
            DeviceActivationRequestObserver activationObserver = _activationObservers.GetValueOrDefault(device.Id);
            if (activationObserver == null) return;
            activationObserver.Dispose();
            _activationObservers.Remove(device.Id);
            _activeDevices.Add(device.Id, device);
        }

        private void SubscribeToInControlAttachEvents()
        {
            LnxInputDeviceRegistry deviceRegistry = new();
            AttachDevice(deviceRegistry.GetOrCreateNativeKeyboard());
            foreach (InputDevice device in InputManager.Devices)
            {
                AttachDevice(deviceRegistry.GetOrCreate(device));
            }
            InputManager.OnDeviceAttached +=
                (rawDevice) => AttachDevice(deviceRegistry.GetOrCreate(rawDevice));
            InputManager.OnDeviceDetached +=
                (rawDevice) => DetachDevice(deviceRegistry.Dispose(rawDevice));
        }
    }
}
