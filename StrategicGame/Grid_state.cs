

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Media;
using System.Xaml;
using WPF_giera1;

namespace WPF_giera1
{
    public struct Vertex
    {
        public Vertex(int r, int c)
        {
            Row = r;
            Column = c;
        }
        public int Row { get; }
        public int Column { get; }
    }
    public struct Path_
    {
        public Path_(int d, double c)
        {
            Destination = d;
            path_cost = c;

        }
        public int Destination { get; }
        public double path_cost { get; set; }
    }
    public class Grid_state
    {
        public int Map_size { get; }
        public GridValue[,] Grid { get; set; }

        public Dictionary<int, Vertex> hash_dict = new Dictionary<int, Vertex>();

        public Dictionary<int, List<Path_>> Road_grid = new Dictionary<int, List<Path_>>();

        public Terrain.Terr_creators Terrain_list;
        public Unit.Unit_creators Unit_list;

        public double Move_cost( int Start_Row, int Start_column, int Final_Row, int Final_column) 
        {
            Terrain Start_Terrain = Grid[Start_Row, Start_column].Field;
            Terrain Final_Terrain = Grid[Final_Row, Final_column].Field;
            double move = Final_Terrain.difficulty();
            if ((Start_Row != Final_Row) && (Start_column != Final_column))
            { move = 1.5 * move; }
            return move;
        }


        public Grid_state(int size)
        {
            Map_size = size;
            Terrain_list = new Terrain.Terr_creators();
            Unit_list = new Unit.Unit_creators();

            Grid = new GridValue[size, size];
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {  
                    int Hash_code_to_dict = i * size + j;
                    Vertex new_ver = new Vertex(i, j);
                    hash_dict[Hash_code_to_dict] = new_ver;
                    Random rnd = new Random();
                    Grid[i, j] = new GridValue(i, j, Terrain_list.creators[rnd.Next(0,3)].FactoryMethod() , false, Unit_list.creators[0].FactoryMethod(), false, 0);

                }

            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++) 
                { 
                     List<Path_> path_list = new List<Path_>();
                 if ( i == 0) {
                    if (j == 0){
                            path_list.Add(new Path_((i * size + j + 1), Move_cost(i,j,i,j+1)));

                            path_list.Add(new Path_(((i + 1) * size + j + 1), Move_cost(i, j, i+1, j + 1)));
                            path_list.Add(new Path_(((i + 1) * size + j), Move_cost(i, j, i+1, j)));
                        }
                    if (j == size - 1)
                        {
                            path_list.Add(new Path_((i * size + j - 1), Move_cost(i, j, i, j-1)));

                            path_list.Add(new Path_(((i + 1) * size + j - 1), Move_cost(i, j, i + 1, j-1)));
                            path_list.Add(new Path_(((i + 1) * size + j), Move_cost(i, j, i + 1, j)));
                        } 
                    if (j > 0 & j < size - 1)
                        {

                            path_list.Add(new Path_((i * size + j - 1), Move_cost(i, j, i, j-1)));
                            path_list.Add(new Path_((i * size + j + 1), Move_cost(i, j, i, j+1)));

                            path_list.Add(new Path_(((i + 1) * size + j + 1), Move_cost(i, j, i + 1, j+1)));
                            path_list.Add(new Path_(((i + 1) * size + j - 1), Move_cost(i, j, i + 1, j-1)));
                            path_list.Add(new Path_(((i + 1) * size + j), Move_cost(i, j, i + 1, j)));
                        } 
                    
                    }
                    if (i == size - 1)
                    {
                        if (j == 0)
                        {
                            path_list.Add(new Path_(((i - 1) * size + j), Move_cost(i, j, i - 1, j)));
                            path_list.Add(new Path_(((i - 1) * size + j + 1), Move_cost(i, j, i - 1,j+1)));

                            path_list.Add(new Path_((i * size + j + 1), Move_cost(i, j, i , j+1)));

                        }
                        if (j == size - 1)
                        {
                            path_list.Add(new Path_(((i - 1) * size + j - 1), Move_cost(i, j, i - 1, j-1)));
                            path_list.Add(new Path_(((i - 1) * size + j), Move_cost(i, j, i - 1, j)));

                            path_list.Add(new Path_((i * size + j - 1), Move_cost(i, j, i, j-1)));

                        }
                        if (j > 0 & j < size - 1)
                        {
                            path_list.Add(new Path_(((i - 1) * size + j - 1), Move_cost(i, j, i - 1, j-1)));
                            path_list.Add(new Path_(((i - 1) * size + j), Move_cost(i, j, i - 1, j)));
                            path_list.Add(new Path_(((i - 1) * size + j + 1), Move_cost(i, j, i - 1, j+1)));

                            path_list.Add(new Path_((i * size + j - 1), Move_cost(i, j, i, j-1)));
                            path_list.Add(new Path_((i * size + j + 1), Move_cost(i, j, i, j+1)));
                        }

                    }
                    if (i > 0 & i < size-1)
                    {
                        if (j == 0)
                        {
                            path_list.Add(new Path_(((i - 1) * size + j), Move_cost(i, j, i - 1, j)));
                            path_list.Add(new Path_(((i - 1) * size + j + 1), Move_cost(i, j, i - 1, j+1)));

                            path_list.Add(new Path_((i * size + j + 1), Move_cost(i, j, i, j+1)));

                            path_list.Add(new Path_(((i + 1) * size + j + 1), Move_cost(i, j, i + 1, j+1)));
                            path_list.Add(new Path_(((i + 1) * size + j), Move_cost(i, j, i + 1, j)));
                        }
                        if (j == size - 1)
                        {
                            path_list.Add(new Path_(((i - 1) * size + j - 1), Move_cost(i, j, i - 1, j-1)));
                            path_list.Add(new Path_(((i - 1) * size + j), Move_cost(i, j, i - 1, j)));

                            path_list.Add(new Path_((i * size + j - 1), Move_cost(i, j, i , j-1)));

                            path_list.Add(new Path_(((i + 1) * size + j - 1), Move_cost(i, j, i + 1, j-1)));
                            path_list.Add(new Path_(((i + 1) * size + j), Move_cost(i, j, i + 1, j)));
                        }
                        if (j > 0 & j < size - 1)
                        {
                            path_list.Add(new Path_(((i - 1) * size + j - 1), Move_cost(i, j, i - 1, j-1)));
                            path_list.Add(new Path_(((i - 1) * size + j), Move_cost(i, j, i - 1, j)));
                            path_list.Add(new Path_(((i - 1) * size + j + 1), Move_cost(i, j, i - 1, j+1)));

                            path_list.Add(new Path_((i * size + j - 1), Move_cost(i, j, i, j-1)));
                            path_list.Add(new Path_((i * size + j + 1), Move_cost(i, j, i, j+1)));

                            path_list.Add(new Path_(((i + 1) * size + j + 1), Move_cost(i, j, i + 1, j+1)));
                            path_list.Add(new Path_(((i + 1) * size + j - 1), Move_cost(i, j, i + 1, j-1)));
                            path_list.Add(new Path_(((i + 1) * size + j), Move_cost(i, j, i + 1, j)));

                        }

                    }

                    Road_grid[i*size + j] = path_list;



                }
            }



        }

    }
}


