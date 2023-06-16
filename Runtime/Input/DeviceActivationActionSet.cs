using InControl;

namespace Sensen.Components
{
    public sealed class DeviceActivationActionSet : PlayerActionSet
    {
        public PlayerAction StartDevice { get; }
        private DeviceActivationActionSet()
        {
            StartDevice = CreatePlayerAction("Start Device");
        }

        public static DeviceActivationActionSet CreateWithKeyboardBindings()
        {
            DeviceActivationActionSet actions = new();
            actions.ListenOptions = ListenOptionsSetup.CreateDefaultOptionsForKeyboard();

            // Keyboard
            actions.StartDevice.AddDefaultBinding(Key.Space);
            actions.StartDevice.AddDefaultBinding(Key.PadEnter);
            actions.StartDevice.AddDefaultBinding(Key.Return);
            return actions;
        }

        public static DeviceActivationActionSet CreateWithControllerBindings()
        {
            DeviceActivationActionSet actions = new();
            actions.ListenOptions = ListenOptionsSetup.CreateDefaultOptionsForController();

            // Controller Start
            actions.StartDevice.AddDefaultBinding(InputControlType.Start);
            actions.StartDevice.AddDefaultBinding(InputControlType.Menu);
            actions.StartDevice.AddDefaultBinding(InputControlType.Options);
            actions.StartDevice.AddDefaultBinding(InputControlType.Pause);
            actions.StartDevice.AddDefaultBinding(InputControlType.Command);
            actions.StartDevice.AddDefaultBinding(InputControlType.RightCommand);
            actions.StartDevice.AddDefaultBinding(InputControlType.LeftCommand);

            // Controller Select
            actions.StartDevice.AddDefaultBinding(InputControlType.Select);
            actions.StartDevice.AddDefaultBinding(InputControlType.View);
            actions.StartDevice.AddDefaultBinding(InputControlType.Share);
            actions.StartDevice.AddDefaultBinding(InputControlType.Back);

            // Controller System
            actions.StartDevice.AddDefaultBinding(InputControlType.System);
            actions.StartDevice.AddDefaultBinding(InputControlType.Power);
            actions.StartDevice.AddDefaultBinding(InputControlType.Home);
            return actions;
        }
    }
}
