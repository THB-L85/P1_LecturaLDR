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
            foreach (String puerto in puertos)
            {
                comboBox1.Items.Add(puerto);
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

        private void Abrir_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                try
                { 
                    if(!serialPort1.IsOpen)
                    {
                        serialPort1.Open();
                        checkBox1.Text = "Puerto Abierto";
                        checkBox1.BackColor = Color.LightGreen;
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkBox1.Checked = false;
                }
            }
            else
            {
                try
                {
                    if(serialPort1.IsOpen)
                    {
                        serialPort1.Close();
                        checkBox1.Text = "Puerto Cerrado";
                        checkBox1.BackColor = Color.LightPink;
                    }
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message, "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkBox1.Checked = false;
                }
            }
        }

        private void ComboPuertos_SelectIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.SelectedItem.ToString();
        }

        private void AnalizarCadena(string cadena)
        {
            char[] separadores = { '=', ';', '=' };
            string[] trama = cadena.Split(separadores);
            foreach(string elemento in trama)
            {
                elemento.Trim();
            }
            progressBar1.Value = Convert.ToInt32(trama[1]);
            label1.Text = trama[3] + "Volts";
        }

    }
}
