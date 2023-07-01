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
using System.Windows.Controls.Primitives;
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
        private String[] headers;

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
                this.headers = GenerateDataGrid();
                thread = new Thread(() => DoWork(connection));
                thread.Start();
                button_read.Content = "Stop Reading";
                reading = true;
            }

           

            
        }


        private String[] GenerateDataGrid()
        {
            string[] headers = CSVFileContend.Split(';');

            // Erstelle die Tabelle dynamisch
            dg_show.AutoGenerateColumns = true;

            // Erstelle die Spalten für die Überschriften
            foreach (string header in headers)
            {
                dg_show.Columns.Add(new DataGridTextColumn() { Header = header });
            }

            return headers;


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
                        String[] values = data.Split(';');  
                        Dictionary<string, string> rowData = new Dictionary<string, string>();
                        for (int i = 0; i < this.headers.Length; i++) {
                            rowData.Add(headers[i], values[i]);
                        }

                        
                        Application.Current.Dispatcher.Invoke(new Action(() => dg_show.Items.Add(rowData)));
                    }
                    catch (IOException)
                    {
                        
                        break;
                    }
                }
            }
            catch(ThreadAbortException ex) { }
           
        }
    }
}
