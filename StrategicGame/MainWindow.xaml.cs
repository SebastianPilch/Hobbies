using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;


namespace WPF_giera1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int Msize = 9;
        private Image[,] Main_Canvas;
        private Image[,] Main_Unit;
        private Button[,] Main_Range;

        //private bool GameOver = false;
        private bool attackmode = false;
        private Grid_state Game_state = new Grid_state(9);
        private bool DoubleClick = false;
        private int PressedRow;
        private int PressedCol;
        private bool IsUnitContainerEmpty = true;
        private Unit Unit_Container;
        private int Unit_Container_row;
        private int Unit_Container_col;
        //private bool IsMoveAllowed = false;
        RangeLayout RanLay = new RangeLayout();
        public MainWindow()
        {
            InitializeComponent();
            Main_Canvas = SetUpTerrainCanvas(Msize);
            Main_Unit = SetUpUnitCanvas(Msize);
            Main_Range = SetUpRangeCanvas(Msize);
            SetUpButtons(Msize);
            //GameLoop();
        }
        private partial Image[,] SetUpTerrainCanvas(int Map_size);
        private partial Image[,] SetUpUnitCanvas(int Map_size);
        private partial Button[,] SetUpRangeCanvas(int Map_size);
        private partial void SetUpButtons(int Map_size);

        private void ButtonCreatedByCode_Click(object sender, RoutedEventArgs e)
        {
            string senderName = ((FrameworkElement)sender).Name;
            char r = senderName[1];
            char c = senderName[3];
            int irow = r - '0';
            int icol = c - '0';

            if (!IsUnitContainerEmpty && !Game_state.Grid[irow, icol].is_active_unit)
            {
                ClearRange();
                RanLay.DrawPath(Main_Range, Game_state, Unit_Container_row*Msize + Unit_Container_col, irow * Msize + icol, Msize, Unit_Container.current_speed);
            }

            if (Game_state.Grid[irow, icol].is_active_unit  && !DoubleClick)
            {
                ClearRange();
                Unit_Container = Game_state.Grid[irow, icol].Unit_;
                Unit_Container_row = irow;
                Unit_Container_col = icol;

                NewUnitStateOnCanvas();

                IsUnitContainerEmpty = false;
                Container_Empty.Background = Brushes.Green;
                Container_X.Content = String.Format("Conteiner_X  {0}", Unit_Container_row);
                Container_Y.Content = String.Format("Container_Y  {0}", Unit_Container_col);

            }
            if (DoubleClick) { DoubleClick = !DoubleClick; }
            PressedRow = irow;
            PressedCol = icol;
        }
        private void ButtonCreatedByCode_RightClick(object sender, RoutedEventArgs e) 
        {
            string senderName = ((FrameworkElement)sender).Name;
            char r = senderName[1];
            char c = senderName[3];
            int irow = r - '0';
            int icol = c - '0';
            //MessageBox.Show(String.Format("jeseś dzban {0} {1}", irow, icol));

        }
        private void GridDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                string senderName = ((FrameworkElement)sender).Name;
                char r = senderName[1];
                char c = senderName[3];
                int irow = r - '0';
                int icol = c - '0';
                

                DoubleClick = true;
                if (!IsUnitContainerEmpty && !Game_state.Grid[irow, icol].is_active_unit && Main_Range[irow, icol].Background == Brushes.Blue)
                {

                    if (Unit_Container.current_speed > 0 && Unit_Container.current_stamina > 0) 
                    {
                        double move_left = (double)Main_Range[irow, icol].Content;
                        double move_cost = Unit_Container.current_speed - move_left;
                        Unit_Container.current_stamina -= move_cost;
                        Unit_Container.current_speed = move_left;
                        Game_state.Grid[irow, icol].is_active_unit = true;
                        Game_state.Grid[Unit_Container_row, Unit_Container_col].is_active_unit = false;

                        Game_state.Grid[irow, icol].Unit_ = Unit_Container;
                        Game_state.Grid[irow, icol].is_active_unit = true;
                        Drawmap();

                        IsUnitContainerEmpty = true;
                        Container_Empty.Background = Brushes.Red;

                        Move_active.Background = Brushes.Red;
                        UnitStats.Opacity = 0.0;

                        ClearRange();
                    }


                }
            }
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e) {

            Drawmap();
        }



        private async Task GameLoop()
        {
            //while (!GameOver)
            //{
            //    await Task.Delay(1000);
                    
            //    Drawmap();

            //}
           
            //MessageBox.Show("Game Over");
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsUnitContainerEmpty)
            {
                Unit_Container.current_hp -= 4;
                if (Unit_Container.current_hp < 0) { Unit_Container.current_hp = 0;}
                Unit_Container.current_mana -= 14;
                if (Unit_Container.current_mana < 0) { Unit_Container.current_mana = 0; }
                Unit_Container.current_stamina -= 2;
                if (Unit_Container.current_stamina < 0) { Unit_Container.current_stamina = 0; }
                NewUnitStateOnCanvas();

            }

            //GameOver = !GameOver;


            //MessageBox.Show(String.Format("Game state is {0: d}", GameOver));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            Game_state.Grid[2, 2].is_active_unit = true;
            Game_state.Grid[2, 2].Unit_ = Game_state.Unit_list.creators[0].FactoryMethod();
            //btn2.IsEnabled = false;
            //btn2.Height = 0;
            Drawmap();

            //GameOver = !GameOver;


            //MessageBox.Show(String.Format("Game state is {0: d}", GameOver));
        }


        private void Drawmap()
        {
            for (int r = 0; r < Msize; r++) {
                for (int c = 0; c < Msize; c++) 
                {
                    int Field_id = Game_state.Grid[r, c].Field.get_id_();
                    Main_Canvas[r, c].Source = Terrain_Images.ImagesByID[Field_id];

                    if (Game_state.Grid[r, c].is_active_unit)
                    {
                        int Unit_id = Game_state.Grid[r, c].Unit_.unit_id();
                        Main_Unit[r, c].Source = Unit_images.UnitByID[Unit_id];
                    }
                    else { Main_Unit[r, c].Source = null; }

                }      
            }
        }
        private void DrawAttackRange(int r, int c, int Attrange)
        {
            if (!IsUnitContainerEmpty)
            {
                for (int i = 0; i < Msize; i++)
                {
                    for (int j = 0; j < Msize; j++)
                    {
                        if (Math.Pow((i - r), 2) + Math.Pow((j - c), 2) <= Math.Pow(Attrange, 2))
                        {
                            Main_Range[i, j].Background = Brushes.Red;
                            Main_Range[i, j].Opacity = 0.4;
                        }
                    }
                }
            }
        }
        private void ClearRange()
        {
            for (int i = 0; i < Msize; i++)
            {
                for (int j = 0; j < Msize; j++)
                {
                    Main_Range[i, j].Background = null;
                    Main_Range[i, j].Opacity = 0;
                    Main_Range[i, j].Content = null;


                }
            }
        }


        private void PrintWhatINeed(object sender, RoutedEventArgs e) 
        {  
           
            int end = PressedRow*Msize + PressedCol;
            RanLay.DrawPath(Main_Range, Game_state,20, end, 9,5);


        }
        private void NewUnitStateOnCanvas() 
        {
            double max_mana_label = Mana_Label.ActualWidth;
            int max_mana = Unit_Container.mana();
            int current_mana = Unit_Container.current_mana;
            double ManaRatio = (double)current_mana / (double)max_mana;
            Mana.Width = ManaRatio * max_mana_label;
            Mana_text.Text = String.Format("{0}/{1}", current_mana, max_mana);

            int max_hp = Unit_Container.max_hp();
            int current_hp = Unit_Container.current_hp;
            double max_hp_label = HP_Label.ActualWidth;
            double HpRatio = (double)current_hp / (double)max_hp;
            HP.Width = HpRatio * max_hp_label;
            HP_text.Text = String.Format("{0}/{1}", current_hp, max_hp);

            double max_stamina = Unit_Container.stamina();
            double current_stamina = Unit_Container.current_stamina;
            double max_stamina_label = Stamina_Label.ActualWidth;
            double StaminaRatio = current_stamina / max_stamina;
            Stamina.Width = StaminaRatio * max_stamina_label;
            Stamina_text.Text = String.Format("{0}/{1}", current_stamina, max_stamina);

            ChosenUnit_Portrait.Source = Unit_images.UnitByID[Unit_Container.unit_id()];
            text_accuracy.Text = String.Format("Accuracy:  {0}%", Unit_Container.current_accuracy() * 100);
            text_armour.Text = String.Format("Armour:  {0}", Unit_Container.current_armour());
            text_attack_range.Text = String.Format("Attack Range:  {0}", Unit_Container.current_attack_range());
            text_attack_damage.Text = String.Format("Damage:  {0}", Unit_Container.current_damage());
            text_speed.Text = String.Format("Speed:  {0:0.00}", Unit_Container.current_speed);
            text_Dodge.Text = String.Format("Dodge chance:  {0}%", Unit_Container.current_damage() * 100);
            text_sight.Text = String.Format("Sight Range:  {0}", Unit_Container.sight());
            text_type.Text = String.Format("Unit Type:  {0}", Unit_Container.name());
            UnitStats.Opacity = 1.0;
        }

        private void Skill_1_Click(object sender, RoutedEventArgs e) { }
        private void Skill_2_Click(object sender, RoutedEventArgs e) { }
        private void Skill_3_Click(object sender, RoutedEventArgs e) { }
        private void Skill_4_Click(object sender, RoutedEventArgs e) { }

        private void BasicAttack_Click(object sender, RoutedEventArgs e) 
        {
            attackmode = !attackmode;
            if (!IsUnitContainerEmpty && attackmode) 
            {
                ClearRange();
                DrawAttackRange(Unit_Container_row,Unit_Container_col,Unit_Container.attack_range());
                BasicAttack.Background = Brushes.Green;
            }
            if (!attackmode) { BasicAttack.Background = null; ClearRange(); }
            
        
        }



    }

    











}



