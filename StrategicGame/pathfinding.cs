using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WPF_giera1
{
    public struct stop_point
    {
        public stop_point(int h, double speed)
        {
            index = h;
            speedLeftToUse = speed ;
        }
        public int index;
        public double speedLeftToUse;

    }
    public class RangeLayout
    {
        public List<stop_point> Path_range(double speed, List<int> path, Dictionary<int, List<Path_>> roadmap) 
        {
            path.Reverse();
            if (path.Count == 1) { return new List<stop_point> {new stop_point(path[0], speed) }; }
            int previous = path[0];
            int next = path[1];
            List<stop_point> All_stops = new List<stop_point> { new stop_point(previous, speed) };
            for (int k=1; k < path.Count;k++)
            {
                foreach (Path_ direction in roadmap[previous])
                {
                    if (direction.Destination == next)
                    {
                        All_stops.Add(new stop_point(next, speed - direction.path_cost));
                        speed -= direction.path_cost;
                        if (k < path.Count - 1)
                        {
                            previous = path[k];
                            next = path[k + 1];
                        }

                    }

                }
                
            }
            return All_stops;
        }



        public List<int> Path_by_a_star(Dictionary<int, List<Path_>> graph, List<double> heuristic,int init,int goal) 
        {
            Dictionary<int, int> came_from = new Dictionary<int, int>();
            List<int> openset = new List<int>{ init };
            List<double> g_score = new List<double>();
            for (int i = 0; i < graph.Count; i++){ g_score.Add(99999.0);}
            g_score[init] = 0;
            List<double> f_score = new List<double>();
            for (int i = 0; i < graph.Count; i++) { f_score.Add(99999.0); }
            f_score[init] = heuristic[init];
            while (openset.Count > 0) 
            {
                double min = 99999.0;
                int current = -1;
                foreach (int i in openset) 
                {
                    if (f_score[i] < min) 
                    {
                        min = f_score[i];
                        current = i;
                    }
                }
                if (current == goal) { return get_path(came_from, current);}
                openset.Remove(current);
                for (int i = 0; i < graph[current].Count; i++) 
                {
                    if (graph[current][i].path_cost != 99999.0) 
                    {
                        double tentative_gscore = g_score[current] + graph[current][i].path_cost;
                        if (tentative_gscore < g_score[graph[current][i].Destination]) 
                        {
                            came_from[graph[current][i].Destination] = current;
                            g_score[graph[current][i].Destination] = tentative_gscore;
                            f_score[graph[current][i].Destination] = tentative_gscore + heuristic[graph[current][i].Destination];
                            if (!openset.Contains(graph[current][i].Destination)) { openset.Add(graph[current][i].Destination);}
                        }
                    }
                } 
            }
            return null;
        }

        private List<int> get_path(Dictionary<int,int> visted, int current)
        {
            List<int> path = new List<int> { current };
            while (visted.ContainsKey(current)) 
            {
                current = visted[current];
                path.Add(current);
            }
            return path;
        }



        public void DrawPath(Button[,] grid ,Grid_state Game_state,int start_point, int final_point,int gridSize,double speed ) 
        {
            List<double> h = new List<double>();
            for (int i = 0; i < gridSize * gridSize; i++) { h.Add(0.5); }
            List<int> path = Path_by_a_star(Game_state.Road_grid, h, start_point, final_point);
            List<stop_point> stops = Path_range(speed, path, Game_state.Road_grid);
            foreach (stop_point sp in stops)
            {
                Vertex ver = Game_state.hash_dict[sp.index];
                int i = ver.Row;
                int j = ver.Column;
                if (sp.speedLeftToUse >= 0.0 && !Game_state.Grid[i,j].is_active_unit)
                {
                    grid[i, j].Background = Brushes.Blue;
                    grid[i, j].Opacity = 0.4;
                    grid[i, j].Content = sp.speedLeftToUse;
                    grid[i, j].FontSize = 0.1;
                    
                }
                else
                {
                    grid[i, j].Background = Brushes.Red;
                    grid[i, j].Opacity = 0.4;

                }

            }
        }

    }
}
