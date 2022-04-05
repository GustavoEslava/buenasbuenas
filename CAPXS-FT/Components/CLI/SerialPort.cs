using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.IO.Ports;
using System.Threading;

using CAPXS_FT.Components.Configuration;
using System.Windows.Forms;
using System.Drawing;

namespace CAPXS_FT.Components.CLI
{

    class GeminiCLI
    {
        static SerialPort _serialPort;

        public void login(String user, String password, Label label) {
            int inputChar;
            Boolean fail = false;
            String response = "";
            label.ForeColor = Color.LightGray;
            label.Text = "Connecting ...";

            _serialPort.DiscardInBuffer();
            this.sendEnter();

            do {
                inputChar = _serialPort.ReadChar();
                if ((char)inputChar != '\n' && (char)inputChar != '\r')
                    Console.Write((char)inputChar);
                response += (char)inputChar;
                if (response.Contains("timed out waiting")) fail = true;
            } while (!response.Contains("\n#") && !response.Contains(": "));

            if (response.Contains("login") || fail || response.Contains("reboot") || response.Contains("restart")) {
                sendCommand(user);
                Thread.Sleep(800);
                readOutput("Password: ");
                sendCommand(password);
                Thread.Sleep(800);
                readOutput("#");
                Console.WriteLine("\nCAPXS Terminal: Login Success");
                label.Text = "Connected";
                label.ForeColor = Color.SeaGreen;
            }

            if (response.Contains("#")) {
                Console.WriteLine("\nCAPXS Already Connected");
                label.Text = "Connected";
                label.ForeColor = Color.SeaGreen;
            }
        }

        internal void getCP(Label label)
        {
            String CP = "";
            sendCommand("cat /manufacturing/SSLCert.pem | grep TE3");
            CP = readOutput("\r\n#").Split(": ", 2)[1].Split("\r\n#",2)[0];
            label.Text = CP;
            label.ForeColor = Color.Black;
        }

        public String readOutput(string expectedOutput)
        {
            int inputchar;
            String response = "";

            do
            {
                inputchar = _serialPort.ReadChar();
                Console.Write((char)inputchar);
                response += (char)inputchar;
            } while (!response.Contains(expectedOutput));

            response = response.Split("\n", 2)[1];
            return response;
        }

        public void connectTo(String comport) {
            _serialPort = new SerialPort();

            if (!_serialPort.IsOpen) {
                Console.WriteLine("Connecting to COMPort: " + comport);

                _serialPort.PortName = Config.SERIALCOM;
                _serialPort.BaudRate = Config.BAUDRATE;
                _serialPort.ReadTimeout = Config.SERIALTIMEOUT;
                _serialPort.WriteTimeout = Config.SERIALTIMEOUT;

                _serialPort.Open();

                StatusController.isConnected = true;
                Console.WriteLine("CAPXS Terminal connected on: " + _serialPort.PortName);
            } else {
                Console.WriteLine("COMPort " + comport + " already open");
            }
        }

        public void disconnect() {
            _serialPort.Close();
            Console.WriteLine("COMPort " + _serialPort.PortName + " was closed");
        }

        public void sendCommand(string command) {
            _serialPort.DiscardInBuffer();
            _serialPort.Write(command + "\r\n");

            Console.WriteLine("Sent command: " + command);
            Thread.Sleep(500);
        }

        public void sendEnter()
        {
            _serialPort.Write(new byte[] { 13, 10 }, 0, 2);
            Console.WriteLine("Sent enter");
        }

    }
}
