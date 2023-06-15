using InControl;

namespace Sensen.Components
{
    public class LnxInputDevice
    {
        public InputDevice Device { get; private set; }
        public string Type { get; private set; }
        public string Id { get; private set; }
        public LnxInputDevice(string id, string type, InputDevice device)
        {
            Device = device;
            Type = type;
            Id = id;
        }
    }
}
