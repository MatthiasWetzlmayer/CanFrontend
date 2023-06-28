using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CanFrontendWPF
{
    /// <summary>
    /// Interaktionslogik für ReadData.xaml
    /// </summary>
    public partial class ReadData : Window
    {
        private DeviceConnection connection;
        private Thread thread;

        internal DeviceConnection Connection
        {
            get { return connection; }
            set { 
                connection = value;
                Title = value.Device.DeviceName;
            }
        }

        private bool reading = false;
        public ReadData()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(reading)
            {
                button_read.Content = "Start Reading";
                reading = false;
                thread.Abort();
            }else{
                thread = new Thread(() => DoWork(connection));
                thread.Start();
                button_read.Content = "Stop Reading";
                reading = true;
            }

           

            
        }
        void DoWork(DeviceConnection connection)
        {
            try
            {
                NetworkStream stream = connection.Client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead;

                while (true)
                {
                    try
                    {
                        bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                        Application.Current.Dispatcher.Invoke(new Action(() => tb_show.Text= tb_show.Text+data));
                    }
                    catch (IOException)
                    {
                        // Handle any exceptions that may occur during reading
                        // For example, the client may have disconnected
                        break;
                    }
                }
            }
            catch(ThreadAbortException ex) { }
           
        }
    }
}
