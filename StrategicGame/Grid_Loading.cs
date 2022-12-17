using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;



namespace WPF_giera1
{
    public partial class MainWindow : Window
    {
        private partial Image[,] SetUpUnitCanvas(int Map_size)
        {

            Image[,] All_images_in_grid = new Image[Map_size, Map_size];
            UnitCanvas.Rows = Map_size;
            UnitCanvas.Columns = Map_size;

            for (int i = 0; i < Map_size; i++)
            {
                for (int j = 0; j < Map_size; j++)
                {
                    Image Imagecontrol = new Image();
                    All_images_in_grid[i, j] = Imagecontrol;
                    UnitCanvas.Children.Add(Imagecontrol);
                }

            }
            return All_images_in_grid;

        }

        private partial Button[,] SetUpRangeCanvas(int Map_size)
        {

            Button[,] All_images_in_grid = new Button[Map_size, Map_size];
            Range_canvas.Rows = Map_size;
            Range_canvas.Columns = Map_size;

            for (int i = 0; i < Map_size; i++)
            {
                for (int j = 0; j < Map_size; j++)
                {
                    Button buttoncontrol = new Button();


                    buttoncontrol.Opacity = 0.0;
                    All_images_in_grid[i, j] = buttoncontrol;
                    Range_canvas.Children.Add(buttoncontrol);
                }

            }
            return All_images_in_grid;

        }
        private partial void SetUpButtons(int Map_size)
        {

            Image[,] All_images_in_grid = new Image[Map_size, Map_size];
            Buttons_canvas.Rows = Map_size;
            Buttons_canvas.Columns = Map_size;


            for (int i = 0; i < Map_size; i++)
            {
                for (int j = 0; j < Map_size; j++)
                {

                    Button b = new Button();
                    String ButtonName = String.Format("r{0:d}C{1:d}", i, j);
                    b.Width = TerrainCanvas.Width / Map_size;
                    b.Height = TerrainCanvas.Height / Map_size;
                    b.Opacity = 0;
                    b.Name = ButtonName;
                    Buttons_canvas.Children.Add(b);
                    b.Click += new RoutedEventHandler(ButtonCreatedByCode_Click);
                    b.MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(ButtonCreatedByCode_RightClick);
                    b.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(GridDoubleClick);
                }
            }


        }
        private void B_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private partial Image[,] SetUpTerrainCanvas(int Map_size)
        {

            Image[,] All_images_in_grid = new Image[Map_size, Map_size];
            //int pixel_per_hex = 20;
            TerrainCanvas.Rows = Map_size;
            TerrainCanvas.Columns = Map_size;


            for (int i = 0; i < Map_size; i++)
            {
                for (int j = 0; j < Map_size; j++)
                {

                    Random rnd = new Random();
                    Image Imagecontrol = new Image
                    {
                        Source = Terrain_Images.ImagesByID[rnd.Next(0, 3)]
                    };

                    All_images_in_grid[i, j] = Imagecontrol;



                    TerrainCanvas.Children.Add(Imagecontrol);



                }
            }
            return All_images_in_grid;

        }

    }
}
