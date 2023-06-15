using InControl;
using System;
using SensenToolkit.Observables;

namespace Sensen.Components
{
    public class DeviceActivationRequestObserver : PlayerActionSet, IDisposable
    {
        private readonly PlayerAction _startAction;
        private readonly UpdateObservable _updateObservable;
        private readonly LnxInputDevice _device;

        public event Action<LnxInputDevice> OnActivationRequest;

        public DeviceActivationRequestObserver(LnxInputDevice device, UpdateObservable updateObservable)
        {
            _updateObservable = updateObservable;
            _device = device;
            Device = _device.Device;
            _startAction = AddBindings(CreatePlayerAction("Activate"));

            _updateObservable.Callbacks += Tick;
        }

        private void Tick()
        {
            if (_startAction.WasPressed)
            {
                OnActivationRequest?.Invoke(_device);
            }
        }

        public void Dispose()
        {
            _updateObservable.Callbacks -= Tick;
            Destroy();
        }

        private static PlayerAction AddBindings(PlayerAction startAction)
        {
            // Keyboard
            startAction.AddDefaultBinding(Key.Space);
            startAction.AddDefaultBinding(Key.PadEnter);
            startAction.AddDefaultBinding(Key.Return);

            // Controller Start
            startAction.AddDefaultBinding(InputControlType.Start);
            startAction.AddDefaultBinding(InputControlType.Menu);
            startAction.AddDefaultBinding(InputControlType.Options);
            startAction.AddDefaultBinding(InputControlType.Pause);
            startAction.AddDefaultBinding(InputControlType.Command);
            startAction.AddDefaultBinding(InputControlType.RightCommand);
            startAction.AddDefaultBinding(InputControlType.LeftCommand);

            // Controller Select
            startAction.AddDefaultBinding(InputControlType.Select);
            startAction.AddDefaultBinding(InputControlType.View);
            startAction.AddDefaultBinding(InputControlType.Share);
            startAction.AddDefaultBinding(InputControlType.Back);

            // Controller System
            startAction.AddDefaultBinding(InputControlType.System);
            startAction.AddDefaultBinding(InputControlType.Power);
            startAction.AddDefaultBinding(InputControlType.Home);
            return startAction;
        }
    }
}
