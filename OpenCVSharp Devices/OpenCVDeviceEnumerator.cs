using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp;

namespace OpenCVSharp_Devices
{
    class OpenCvDeviceEnumerator
    {
        private List<CapDriver> _drivers;
        private static List<int> _camIdList = new List<int>();

        private struct CapDriver
        {
            public int EnumValue;
            public string EnumName;
            public string Comment;
        };

        public bool EnumerateCameras(List<int> camIdx)
        {
            camIdx.Clear();

            // list of all CAP drivers (see highgui_c.h)
            _drivers = new List<CapDriver>
            {
                //drivers.Add(new CapDriver { enumValue = (int)VideoCaptureAPIs.VFW, enumName = "VFW", comment = "platform native" });
                /*new CapDriver
                    { enumValue = (int)VideoCaptureAPIs.V4L, enumName = "V4L", comment = "platform native" },
                new CapDriver
                    { enumValue = (int)VideoCaptureAPIs.IEEE1394, enumName = "IEEE1394", comment = "IEEE 1394 drivers" },
                new CapDriver
                    { enumValue = (int)VideoCaptureAPIs.ANY, enumName = "Qt", comment = "Quicktime" },
                //drivers.Add(new CapDriver { enumValue = (int)VideoCaptureAPIs.Unicap, enumName = "Unicap", comment = "Unicap drivers" });
                */
                new CapDriver
                {
                    EnumValue = (int)VideoCaptureAPIs.DSHOW, EnumName = "DSHOW", Comment = "DirectShow (via videoInput)"
                }
                /*,
                new CapDriver
                    { enumValue = (int)VideoCaptureAPIs.PVAPI, enumName = "PVAPI", comment = "PvAPI, Prosilica GigE SDK" },
                new CapDriver
                    { enumValue = (int)VideoCaptureAPIs.OPENNI, enumName = "OpenNI", comment = "OpenNI(for Kinect) " },
                new CapDriver
                {
                    enumValue = (int)VideoCaptureAPIs.OPENNI_ASUS, enumName = "OpenNI_ASUS",
                    comment = "OpenNI(for Asus Xtion) "
                },
                new CapDriver
                    { enumValue = (int)VideoCaptureAPIs.ANDROID, enumName = "Android", comment = "Android" },
                new CapDriver
                    { enumValue = (int)VideoCaptureAPIs.XIAPI, enumName = "XIAPI", comment = "XIMEA Camera API" },
                new CapDriver
                {
                    enumValue = (int)VideoCaptureAPIs.AVFOUNDATION, enumName = "AVFoundation",
                    comment = "AVFoundation framework for iOS (OS X Lion will have the same API)"
                },
                new CapDriver
                {
                    enumValue = (int)VideoCaptureAPIs.GIGANETIX, enumName = "Giganetix",
                    comment = "Smartek Giganetix GigEVisionSDK"
                },
                new CapDriver
                {
                    enumValue = (int)VideoCaptureAPIs.MSMF, enumName = "MSMF",
                    comment = "Microsoft Media Foundation (via videoInput)"
                },
                new CapDriver
                {
                    enumValue = (int)VideoCaptureAPIs.WINRT, enumName = "WinRT",
                    comment = "Microsoft Windows Runtime using Media Foundation"
                },
                new CapDriver
                {
                    enumValue = (int)VideoCaptureAPIs.INTELPERC, enumName = "IntelPERC",
                    comment = "Intel Perceptual Computing SDK"
                },
                new CapDriver
                    { enumValue = (int)VideoCaptureAPIs.OPENNI2, enumName = "OpenNI2", comment = "OpenNI2 (for Kinect)" },
                new CapDriver
                {
                    enumValue = (int)VideoCaptureAPIs.OPENNI2_ASUS, enumName = "OpenNI2_ASUS",
                    comment = "OpenNI2 (for Asus Xtion and Occipital Structure sensors)"
                },
                new CapDriver
                    { enumValue = (int)VideoCaptureAPIs.GPHOTO2, enumName = "GPhoto2", comment = "gPhoto2 connection" },
                new CapDriver
                    { enumValue = (int)VideoCaptureAPIs.GSTREAMER, enumName = "GStreamer", comment = "GStreamer" },
                new CapDriver
                {
                    enumValue = (int)VideoCaptureAPIs.FFMPEG, enumName = "FFMPEG",
                    comment = "Open and record video file or stream using the FFMPEG library"
                },
                new CapDriver
                {
                    enumValue = (int)VideoCaptureAPIs.IMAGES, enumName = "Images",
                    comment = "OpenCV Image Sequence (e.g. img_%02d.jpg)"
                },
                new CapDriver
                    { enumValue = (int)VideoCaptureAPIs.V4L2, enumName = "V4L2", comment = "Same as V4L" },
                new CapDriver
                    { enumValue = (int)VideoCaptureAPIs.ARAVIS, enumName = "ARAVIS", comment = "Aravis SDK" }
                    */
            };

            var frame = new Mat();
            //Console.WriteLine("Searching for cameras IDs...");
            const int maxId = 100;
            for (var drv = 0; drv < _drivers.Count; drv++)
            {
                var driverName = _drivers[drv].EnumName;
                var driverEnum = _drivers[drv].EnumValue;
                //Console.WriteLine("Testing driver " + driverName);
                var found = false;

                for (var idx = 0; idx < maxId; idx++)
                {

                    var cap = new VideoCapture(driverEnum + idx); // open the camera
                    if (cap.IsOpened()) // check if we succeeded
                    {
                        found = true;
                        camIdx.Add(driverEnum + idx); // vector of all available cameras
                        cap.Read(frame);
                        if (frame.Empty())
                            Console.WriteLine(driverName + "+" + idx + "\t opens: OK \t grabs: FAIL");
                        else
                            Console.WriteLine(driverName + "+" + idx + "\t opens: OK \t grabs: OK");
                    }

                    cap.Release();
                }

                if (!found) Console.WriteLine("Nothing !");

            }

            //Console.WriteLine(camIdx.Count() + " camera IDs has been found ");
            //Console.WriteLine("Press a key...");
            //Console.ReadKey();

            return (camIdx.Any()); // returns success
        }
    }
}