using System.Collections.Generic;
using InControl;

namespace Sensen.Components
{
    public class LnxInputDeviceRegistry
    {
        public static readonly InputDevice NativeKeyboardDevice = new("native-keyboard");
        private readonly Dictionary<string, LnxInputDevice> _devices = new();
        public LnxInputDevice GetOrCreate(InputDevice rawDevice)
        {
            string id = IdentifyRawDevice(rawDevice);
            if (!_devices.ContainsKey(id))
            {
                LnxInputDevice device = new(id, IdentifyRawDeviceType(rawDevice), rawDevice);
                _devices.Add(id, device);
            }
            return _devices[id];
        }

        public LnxInputDevice GetOrCreateNativeKeyboard()
        {
            return GetOrCreate(NativeKeyboardDevice);
        }

        public LnxInputDevice Dispose(InputDevice rawDevice)
        {
            string id = IdentifyRawDevice(rawDevice);
            LnxInputDevice device = _devices.GetValueOrDefault(id);
            if (device != null) _devices.Remove(id);
            return device;
        }

        public static string IdentifyRawDevice(InputDevice rawDevice)
        => $"{IdentifyRawDeviceType(rawDevice)}|{rawDevice.GUID}";

        public static string IdentifyRawDeviceType(InputDevice rawDevice) =>
        rawDevice == NativeKeyboardDevice
        ? "native-keyboard|keyboard|keyboard"
        : $"{rawDevice.Name}|{rawDevice.DeviceClass}|{rawDevice.DeviceStyle}".ToLower();
    }
}
