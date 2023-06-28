using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CanFrontendWPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<DeviceConnection> availableDevices;
        private List<DeviceConnection> connectedDevices;
        private BluetoothClient bluetoothClient;
        public MainWindow()
        {
            InitializeComponent();
            availableDevices = new List<DeviceConnection>();
            connectedDevices = new List<DeviceConnection>();
        }

        private void DiscoverButton_Click(object sender, RoutedEventArgs e)
        {
            availableDevices.Clear();
            AvailableDevicesListBox.Items.Clear();
            connectedDevices.Clear();
            ConnectedDevicesListBox.Items.Clear();

            // Discover nearby Bluetooth devices
            BluetoothClient bluetoothClient = new BluetoothClient();
            BluetoothDeviceInfo[] devices = bluetoothClient.DiscoverDevices().ToArray();

            foreach(BluetoothDeviceInfo device in devices.Where(x => x.Connected).ToArray()) {
                connectedDevices.Add(new DeviceConnection(device));
            }

            // Store the discovered devices and display them in the list box
            foreach (BluetoothDeviceInfo device in devices.Where(x => !x.Connected).ToArray())
            {
                availableDevices.Add(new DeviceConnection(device));
            }

            UpdateDeviceLists();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected device from the list box
            string selectedDeviceName = AvailableDevicesListBox.SelectedItem as string;
            DeviceConnection selectedConnection = availableDevices.FirstOrDefault(d => d.Device.DeviceName == selectedDeviceName);
            BluetoothDeviceInfo selectedDevice = selectedConnection?.Device;

            if (selectedDevice != null)
            {
                try
                {
                    selectedConnection.Client.Connect(selectedDevice.DeviceAddress, BluetoothService.SerialPort);

                    availableDevices.Remove(selectedConnection);
                    connectedDevices.Add(selectedConnection);
                    UpdateDeviceLists();
                    MessageBox.Show("Connected to the device!");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Something went wrong!");
                }

                
            }
            else
            {
                MessageBox.Show("Please select a device first.");
            }
        }

        private void UpdateDeviceLists()
        {
            AvailableDevicesListBox.Items.Clear();
            ConnectedDevicesListBox.Items.Clear();

            foreach (DeviceConnection device in availableDevices)
            {
                AvailableDevicesListBox.Items.Add(device.Device.DeviceName);
            }

            foreach (DeviceConnection device in connectedDevices)
            {
                ConnectedDevicesListBox.Items.Add(device.Device.DeviceName);
            }
        }

        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedDeviceName = ConnectedDevicesListBox.SelectedItem as string;
            DeviceConnection selectedConnection = connectedDevices.FirstOrDefault(d => d.Device.DeviceName == selectedDeviceName);
            BluetoothDeviceInfo selectedDevice = selectedConnection?.Device;
            if (selectedDevice != null)
            {
                selectedConnection.Client.Close();
                selectedConnection.resetClient();

                // Remove the disconnected device from the connectedDevices list
                connectedDevices.Remove(selectedConnection);

                // Add the disconnected device back to the discoveredDevices list
                availableDevices.Add(selectedConnection);

                // Update the UI lists
                UpdateDeviceLists();

                MessageBox.Show("Disconnected from the device!");
            }
            else
            {
                MessageBox.Show("Please select a connected device first.");
            }

        }

        private void ReadDataButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedDeviceName = ConnectedDevicesListBox.SelectedItem as string;
            DeviceConnection selectedConnection = connectedDevices.FirstOrDefault(d => d.Device.DeviceName == selectedDeviceName);
            BluetoothDeviceInfo selectedDevice = selectedConnection?.Device;
            ReadData window = new ReadData();
            window.Connection = selectedConnection;
            window.Show();
        }
    }
}
