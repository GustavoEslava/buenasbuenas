using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//OpenCV
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using System.Drawing;
using System.Windows.Forms;

//Internal Components
using CAPXS_FT.Components.Configuration;
using CAPXS_FT.Components.CLI;
using CAPXS_FT.Components.ComputerVision;


namespace CAPXS_FT
{
    public partial class AlignmentTest : Form
    {
        GeminiCLI _capxsTerminal = new GeminiCLI();
        ImageRetriever _imageRetriever = new ImageRetriever();
        Opencv _opencv = new Opencv();
        
        public AlignmentTest()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //do
            //{
            //    do
            //    {
            //        try
            //        {
            //            _capxsTerminal.connectTo(Config.SERIALCOM);
            //            _capxsTerminal.login(Config.USERCAPXS, Config.PASSCAPXS, lbl_status);
            //            _capxsTerminal.getCP(lbl_DUT);
            //        }
            //        catch (Exception ex)
            //        {
            //            StatusController.isConnected = false;
            //        }
            //    } while (!StatusController.isConnected && StatusController.retryRequested);

            //    do
            //    {
            //        _imageRetriever.stopCameraProcess();
            //    } while (!StatusController.isCameraFree);

            //    _imageRetriever.stopCameraProcess();
            //    _imageRetriever.getInternalImage();
            //    _imageRetriever.encodeImage();
            //    _imageRetriever.concatenateFile();
            //    _imageRetriever.decodeImage();

            //    _opencv.displayImage(pictureBox1);
            //    _opencv.detectLens(pictureBox1);

            //    _capxsTerminal.disconnect();
            //} while (StatusController.isRunning && false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                _capxsTerminal.connectTo(Config.SERIALCOM);
                _capxsTerminal.login(Config.USERCAPXS, Config.PASSCAPXS, lbl_status);
                _capxsTerminal.getCP(lbl_DUT);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.displayConnectionError();
            }

            try
            {
                _imageRetriever.stopCameraProcess();
                _imageRetriever.getInternalImage();

                _imageRetriever.encodeImage();
                _imageRetriever.concatenateFile();
                _imageRetriever.decodeImage();

                _opencv.detectLensTwo(pictureBox1);

            } catch (Exception exc) {
                Console.WriteLine(exc.Message);
                this.displayConnectionError();
            }
            finally
            {
                _capxsTerminal.disconnect();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StatusController.retryRequested = true;
        }

        private void displayConnectionError()
        {
            this.lbl_status.ForeColor = Color.IndianRed;
            this.lbl_status.Text = "Excecution error";

            this.lbl_DUT.ForeColor = Color.Silver;
            this.lbl_DUT.Text = "[None]";
        }
    }
}
