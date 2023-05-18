using System;
using System.Collections.Generic;
using OpenCvSharp;

namespace OpenCVSharp_Devices
{
    internal class Program
    {
        private static readonly List<int> CamIdList = new List<int>();
        private static readonly OpenCvDeviceEnumerator Enumerator = new OpenCvDeviceEnumerator();
        
        public static void Main(string[] args)
        {
            Enumerator.EnumerateCameras(CamIdList);
            CamIdList.ForEach(Console.WriteLine);

            foreach (var cam in CamIdList)
            {
                Console.WriteLine($"Ready to take a picture with camera: {cam}. Press Any key when ready!");
                Console.ReadKey();
                using (var capture = new VideoCapture(cam))
                {
                    using (var image = new Mat())
                    {
                        capture.Read(image);
                        image.SaveImage($"image_from_cam_{cam}.png");
                    }
                }
                
            }
        }
    }
}