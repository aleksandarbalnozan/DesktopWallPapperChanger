using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace WallPapper
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, String pvParam, UInt32 fWinIni);
        private static UInt32 SPI_SETDESKWALLPAPER = 20;
        private static UInt32 SPIF_UPDATEINIFILE = 0x1;

        private static List<string> listOfImages;
        static void Main(string[] args)
        {
            Console.WriteLine("Application is starting");
            LoadImages();
            while (true)
            {
                foreach (var item in listOfImages)
                {
                    SetImage(item);
                    System.Threading.Thread.Sleep(10000);
                }
            }
        }


        private static void SetImage(string filename)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, filename, SPIF_UPDATEINIFILE);
        }

        private static void LoadImages()
        {
            listOfImages = new List<string>();
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\.."));
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (var item in di.GetFiles("*.png"))
            {
                listOfImages.Add(item.ToString());
            }
        }
    }
}
