using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CAPXS_FT.Components.Configuration;

namespace CAPXS_FT.Components.CLI
{
    class ImageRetriever
    {
        GeminiCLI myCLI = new GeminiCLI();

        public void stopCameraProcess() {
            String process = "";

            Console.WriteLine("Stopping camera process");
            
            myCLI.sendCommand("fuser -k /dev/video*");
            process = myCLI.readOutput("#");
            Console.Write(process);

            StatusController.isCameraFree = true;
        }

        public void getInternalImage() {
            myCLI.sendCommand("ffmpeg -y -f video4linux2 -video_size 1920x1080 -i /dev/video2 -vf scale=640:360 -frames 1 " + Config.FILENAME + ".jpg");
            myCLI.sendCommand(String.Format("ffmpeg -y -f video4linux2 -video_size 1920x1080 -i /dev/video2 -vf scale=640:360 -frames 1 {0}.jpg", Config.FILENAME));
            try
            {
                myCLI.readOutput("unknown\r\n#");
            }
            catch (System.TimeoutException e)
            {
                Console.WriteLine("ERR: Could not retrieve image");
                throw;
            }
            Thread.Sleep(50);
        }

        public void encodeImage() {
            // myCLI.sendCommand("base64 testImage.jpg > testImage.b64");
            myCLI.sendCommand(String.Format("base64 {0}.jpg > {0}.b64", Config.FILENAME));
            myCLI.readOutput("#");
            Thread.Sleep(50);
        }

        public void concatenateFile() {
            String code = "";
            String path = String.Format("{0}{1}.b64", Config.WORKINGDIRECTORY, Config.FILENAME);
            //String path = Config.WORKINGDIRECTORY + Config.FILENAME + ".b64";

            //myCLI.sendCommand("cat testImage.b64");
            myCLI.sendCommand(String.Format("cat {0}.b64", Config.FILENAME));
            code = myCLI.readOutput("#");

            File.WriteAllText(path, code.Split("\r\n#",2)[0]);
        }

        public void decodeImage() {
            string strCmdText;
            strCmdText = String.Format("/C certutil -f -decode {0}.b64 {0}.jpg", Config.FILENAME);
            System.Environment.CurrentDirectory = Config.WORKINGDIRECTORY;
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }
    }
}
