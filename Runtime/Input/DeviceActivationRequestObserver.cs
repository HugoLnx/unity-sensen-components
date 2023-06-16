using System;
using SensenToolkit.Observables;

namespace Sensen.Components
{
    public class DeviceActivationRequestObserver : IDisposable
    {
        private readonly DeviceActivationActionSet _actions;
        private readonly UpdateObservable _updateObservable;
        private readonly LnxInputDevice _device;

        public event Action<LnxInputDevice> OnActivationRequest;

        public DeviceActivationRequestObserver(LnxInputDevice device, UpdateObservable updateObservable)
        {
            _actions = device.IsKeyboard
                ? DeviceActivationActionSet.CreateWithKeyboardBindings()
                : DeviceActivationActionSet.CreateWithControllerBindings();

            _device = device;
            _actions.Device = device.Device;
            _updateObservable = updateObservable;
            _updateObservable.Callbacks += Tick;
        }

        private void Tick()
        {
            if (_actions.StartDevice.WasPressed)
            {
                OnActivationRequest?.Invoke(_device);
            }
        }

        public void Dispose()
        {
            _updateObservable.Callbacks -= Tick;
            _actions.Destroy();
        }
    }
}
