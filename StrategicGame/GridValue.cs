
namespace WPF_giera1
{
    public class GridValue
    {   
        public int Row { get; }
        public int Column { get; }
        public Terrain Field { get; }
        public bool is_active_unit { get; set; }
        public Unit Unit_ { get; set; }
        public bool is_active_wall { get; set; }
        public Wall_types Wall { get; set; }


        public GridValue(int row, int column ,Terrain hex, bool is_active_unit, Unit unit_, bool is_active_wall, Wall_types wall)
        {
            Row = row;
            Column = column;
            Field = hex;
            this.is_active_unit = is_active_unit;
            Unit_ = unit_;
            this.is_active_wall = is_active_wall;
            Wall = wall;
        }


    }
}


