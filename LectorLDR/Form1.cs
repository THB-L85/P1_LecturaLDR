using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace LectorLDR
{
    public partial class LectorLDR : Form
    {
        private string datoSerial;
        public LectorLDR()
        {
            InitializeComponent();
            string[] puertos = SerialPort.GetPortNames();
            foreach (string puerto in puertos)
            {
                comboBox1.Items.Add(puertos);
            }
        }

        private void RecibirTexto(object sender, EventArgs e)
        {
            textBox1.Text = datoSerial;
            AnalizarCadena(datoSerial);
        }

        private void ptoSerial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            serialPort1.ReadTimeout = 200;
            try
            {
                datoSerial = serialPort1.ReadTo("\r\n");
                Invoke(new EventHandler(RecibirTexto));
            }
            catch(Exception excepcion)
            {
                MessageBox.Show(excepcion.ToString());
                serialPort1.DiscardInBuffer();
            }
        }
    }
}
