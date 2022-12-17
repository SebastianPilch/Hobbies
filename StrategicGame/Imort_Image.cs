using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_giera1
{
    public static class Terrain_Images
    {


        public static readonly List<ImageSource> ImagesByID = new List<ImageSource> 
        { 
            LoadImage("MeadowKrita.png"),
            LoadImage("Lasopodobne.png"),
            LoadImage("Wzgorzopodobne.png") 
        };

        private static ImageSource LoadImage(string fileName)
        {
            return new BitmapImage(new Uri($"assets/{fileName}", UriKind.Relative));
        }


    }

    public static class Unit_images
    {


        public static readonly List<ImageSource> UnitByID = new List<ImageSource>
        {
            LoadImage("cos.png")
        };

        private static ImageSource LoadImage(string fileName)
        {
            return new BitmapImage(new Uri($"assets/{fileName}", UriKind.Relative));
        }


    }

}
