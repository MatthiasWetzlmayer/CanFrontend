using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CanFrontendWPF
{
    /// <summary>
    /// Interaktionslogik für ReadData.xaml
    /// </summary>
    public partial class ReadData : Window
    {
        private DeviceConnection connection;
        private Thread thread;
        private String[] headers;
        public DataTable dataTable;

        internal DeviceConnection Connection
        {
            get { return connection; }
            set { 
                connection = value;
                Title = value.Device.DeviceName;
            }
        }

        public String CSVFileContend { get; set; }

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
                        String[] lines = data.Split('#');
                        lines = lines.Where(x => x.Split(';').Length > 0).ToArray();

                        List<String> rowData = new List<String>();
                        foreach (string line in lines)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() => tb_show.Text += line+"\n"));

                        }

                        buffer = new byte[1024];
                        Thread.Sleep(1000);
                    }
                    catch (IOException)
                    {

                        break;
                    }
                }
            }
            catch (ThreadAbortException ex) { }

        }

        
    }
}





