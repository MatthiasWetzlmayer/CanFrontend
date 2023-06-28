using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanFrontendWPF
{
    internal class DeviceConnection
    {
        private BluetoothClient _bluetoothClient;
        public BluetoothDeviceInfo Device { get; set; }
        public BluetoothClient Client {
            get
            {
                if (_bluetoothClient == null)
                    _bluetoothClient = new BluetoothClient();

                return _bluetoothClient;
            }
        }
        public DeviceConnection(BluetoothDeviceInfo device)
        {
            Device = device;
        }
        public DeviceConnection() { }

        internal void resetClient()
        {
            this._bluetoothClient = new BluetoothClient();
        }
    }
}
