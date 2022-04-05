using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

using System.Drawing;
using CAPXS_FT.Components.Configuration;
using System.Threading;
using System.IO;

namespace CAPXS_FT.Components.ComputerVision
{
    class Opencv
    {
        public void displayImage(PictureBox pb) {
            String path = String.Format("{0}{1}.jpg", Config.WORKINGDIRECTORY, Config.FILENAME);
            //String path = @"C:\Users\lreyes5\OneDrive - The Chamberlain Group, Inc\Documents\lreyes5\CAPXSCamera\ComputerVision\testImage.jpg";
            Image<Bgr, byte> imgDisplay = new Image<Bgr, byte>(path);
            pb.Image = imgDisplay.ToBitmap();
        }

        public void detectLens(PictureBox pb2)
        {
            String path = @"C:\Users\lreyes5\OneDrive - The Chamberlain Group, Inc\Documents\lreyes5\CAPXSCamera\ComputerVision\testImage.jpg";
            Image<Bgr, byte> imgSrc = new Image<Bgr, byte>(path);

            Image<Gray, byte> imgGray = imgSrc.Convert<Gray, byte>();
            Image<Gray, byte> imgBlur = imgGray.Copy();
            Image<Gray, byte> imgThresh = imgGray.Copy();
            Image<Gray, byte> imgOpen = imgGray.Copy();
            Image<Gray, byte> imgCnt = imgGray.Copy();
            Image<Gray, byte> imgAprox = imgGray.Copy();

            CvInvoke.MedianBlur(imgGray, imgBlur, 9);
            CvInvoke.Threshold(imgBlur, imgThresh, 0, 255, ThresholdType.Binary + (int)ThresholdType.Otsu * 2);

            IInputArray kernel = CvInvoke.GetStructuringElement(ElementShape.Ellipse, new System.Drawing.Size(3, 3), new System.Drawing.Point(-1,-1));
            CvInvoke.MorphologyEx(imgThresh, imgOpen, MorphOp.Open, kernel, new System.Drawing.Point(-1, -1), 3, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue);
            Emgu.CV.Util.VectorOfVectorOfPoint cnts = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Mat hier = new Mat();

            CvInvoke.FindContours(imgOpen, cnts, hier, RetrType.External, ChainApproxMethod.ChainApproxSimple);
            CvInvoke.DrawContours(imgSrc, cnts, -1, new Bgr(Color.Aquamarine).MCvScalar, 1);
            //CvInvoke.GaussianBlur(imgGray, imgBlur, new System.Drawing.Size(5, 5), 0);

            //for (int i = 0; i < cnts.Size; i++)
            //{
            //    using (Emgu.CV.Util.VectorOfPoint c = cnts[i])
            //    using (Emgu.CV.Util.VectorOfPoint approx = new Emgu.CV.Util.VectorOfPoint())
            //    {
            //        CvInvoke.ApproxPolyDP(c, approx, CvInvoke.ArcLength(c, true) * .01, true);
            //        double area = CvInvoke.ContourArea(approx);


            //        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
            //        if (approx.Length > 5 && area > 10000 && area < 500000)
            //        {
            //            System.Tuple<int, int> leftmost = Tuple.Create(c[c[:, :, 0].argmin()], 1);

            //            //match the shape to the square
            //            double ratio = CvInvoke.MatchShapes(_square, approx, Emgu.CV.CvEnum.ContoursMatchType.I3);

            //            if (ratio < .05)
            //            {
            //                var M = CvInvoke.Moments(c);
            //                int cx = (int)(M.M10 / M.M00);
            //                int cy = (int)(M.M01 / M.M00);

            //                //filter out any that are too close 
            //                if (!tiles.Any(x => Math.Abs(x.X - cx) < 50 && Math.Abs(x.Y - cy) < 50))
            //                {
            //                    tiles.Add(new System.Drawing.Point(cx, cy));
            //                    for (int j = 0; j < approx.Size; j++)
            //                    {
            //                        int second = j + 1 == approx.Size ? 0 : j + 1;

            //                        //do some detection for upsidedown/right side up here....

            //                        CvInvoke.Line(image,
            //                            new System.Drawing.Point(approx[j].X, approx[j].Y),
            //                            new System.Drawing.Point(approx[second].X, approx[second].Y),
            //                            new MCvScalar(255, 255, 255, 255), 4);
            //                    }
            //                }
            //            }
            //        }
            //        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
            //    }

            pb2.Image = imgSrc.ToBitmap();
        }

        public void detectLensTwo(PictureBox pb)
        {
            String path = String.Format("{0}{1}.jpg", Config.WORKINGDIRECTORY, Config.FILENAME);

            do
            {
                Thread.Sleep(10);
            } while (!File.Exists(path));

            int ROI_SIDE;
            int IMAGE_HEIGHT;
            int IMAGE_WIDTH;

            Image<Bgr, byte> imgSrc = new Image<Bgr, byte>(path);

            Image<Gray, byte> imgGray = imgSrc.Convert<Gray, byte>();
            Image<Gray, byte> imgBlur = imgGray.Copy();

            CvInvoke.GaussianBlur(imgGray, imgBlur, new System.Drawing.Size(3, 3), 0);
            //CvInvoke.MedianBlur(imgGray, imgBlur, 9);

            ROI_SIDE = imgSrc.Height / 3;
            IMAGE_HEIGHT = imgSrc.Height;
            IMAGE_WIDTH = imgSrc.Width;

            for (int i = 0; i<4; i++){
                switch (i) {
                    case 0:
                        imgBlur.ROI = new Rectangle(0,0, ROI_SIDE, ROI_SIDE);
                        imgSrc.ROI = new Rectangle(0, 0, ROI_SIDE, ROI_SIDE);
                        break;
                    case 1:
                        imgBlur.ROI = new Rectangle(IMAGE_WIDTH -ROI_SIDE, 0, ROI_SIDE, ROI_SIDE);
                        imgSrc.ROI = new Rectangle(IMAGE_WIDTH - ROI_SIDE, 0, ROI_SIDE, ROI_SIDE);
                        break;
                    case 2:
                        imgBlur.ROI = new Rectangle(0, IMAGE_HEIGHT - ROI_SIDE, ROI_SIDE, ROI_SIDE);
                        imgSrc.ROI = new Rectangle(0, IMAGE_HEIGHT - ROI_SIDE, ROI_SIDE, ROI_SIDE);
                        break;
                    case 3:
                        imgBlur.ROI = new Rectangle(IMAGE_WIDTH - ROI_SIDE, IMAGE_HEIGHT - ROI_SIDE, ROI_SIDE, ROI_SIDE);
                        imgSrc.ROI = new Rectangle(IMAGE_WIDTH - ROI_SIDE, IMAGE_HEIGHT - ROI_SIDE, ROI_SIDE, ROI_SIDE);
                        break;
                    default:
                        break;
                }

                double average = imgBlur.GetAverage().Intensity;
                Image<Gray, byte> lowThresh = imgBlur.Copy();
                Image<Gray, byte> highThresh = imgBlur.Copy();

                CvInvoke.Threshold(imgBlur, lowThresh, average*0.7, 255, ThresholdType.BinaryInv);
                CvInvoke.Threshold(imgBlur, highThresh, average * 0.4, 255, ThresholdType.BinaryInv);

                Emgu.CV.Util.VectorOfVectorOfPoint lowCnts = new Emgu.CV.Util.VectorOfVectorOfPoint();
                Mat lowHier = new Mat();
                Emgu.CV.Util.VectorOfVectorOfPoint highCnts = new Emgu.CV.Util.VectorOfVectorOfPoint();
                Mat highHier = new Mat();

                CvInvoke.FindContours(lowThresh, lowCnts, lowHier, RetrType.External, ChainApproxMethod.ChainApproxSimple);
                CvInvoke.FindContours(highThresh, highCnts, highHier, RetrType.External, ChainApproxMethod.ChainApproxSimple);

                CvInvoke.DrawContours(imgSrc, lowCnts, -1, new Bgr(Color.Yellow).MCvScalar, 1);
                CvInvoke.DrawContours(imgSrc, highCnts, -1, new Bgr(Color.Red).MCvScalar, 1);
            }

            imgSrc.ROI = Rectangle.Empty;

            pb.Image = imgSrc.ToBitmap();
        }
    }
}
