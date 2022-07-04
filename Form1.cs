using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace LecturaLDR
{
    public partial class Form1 : Form
    {
        private string datoSerial;

        public Form1()
        {
            InitializeComponent();
            string[] puertos = SerialPort.GetPortNames();
            foreach(String puerto in puertos)
            {
                comboPorts.Items.Add(puerto);
            }
        }

        private void ptoSerial_DataRecived(object sender, SerialDataReceivedEventArgs e)
        {
            textBox1.ReadTimeout = 200;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}