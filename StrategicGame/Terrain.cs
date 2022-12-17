using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_giera1
{

    public abstract class Terrain
    {
        public abstract string getName();
        public abstract int get_id_();
        public abstract int Cover();
        public abstract int Hight();
        public abstract int Damage();
        public abstract int Uncertain_ground();
        public abstract double difficulty();



        public class Meadows : Terrain
        {
            public override string getName() { return "Meadows"; }
            public override int get_id_() { return 0; }
            public override int Hight() { return 1; }
            public override int Cover() { return 0; }
            public override int Uncertain_ground() { return 0; }
            public override int Damage() { return 0; }
            public override double difficulty() { return 1.0;}


        }

        public class Forest : Terrain
        {
            public override string getName() { return "Forest"; }
            public override int get_id_() { return 1; }
            public override int Hight() { return 1; }
            public override int Damage() { return 0; }
            public override int Cover() { return 1; }
            public override int Uncertain_ground() { return 0; }
            public override double difficulty() { return 1.3;}

        }

        public class Highlands : Terrain
        {
            public override string getName() { return "Forest"; }
            public override int get_id_() { return 2; }
            public override int Hight() { return 2; }
            public override int Damage() { return 0; }
            public override int Cover() { return 1; }
            public override int Uncertain_ground() { return 0; }
            public override double difficulty() { return 2.0; }


        }








        public abstract class Creator
        {
            public abstract Terrain FactoryMethod();
        }
        public class MeadowCreator : Creator
        {
            public override Terrain FactoryMethod()
            {
                return new Meadows();
            }
        }
        /// <summary>
        /// A 'ConcreteCreator' class
        /// </summary>
        public class ForestCreator : Creator
        {
            public override Terrain FactoryMethod()
            {
                return new Forest();
            }
        }


        
        public class HighlandsCreator : Creator
        {
            public override Terrain FactoryMethod()
            {
                return new Highlands();
            }

        }


        public class Terr_creators
        {

            public Creator[] creators;  
            public Terr_creators()
            {
                creators = new Creator[3];
                creators[0] = new MeadowCreator();
                creators[1] = new ForestCreator();
                creators[2] = new HighlandsCreator();

            }
        }








    }

    
    //public enum Terrain_id
    //    {
    //        Meadows ,
    //        Forest,
    //        Taiga,
    //        Tundra,
    //        Highlands,
    //        Forested_Highlands,
    //        Swamp,
    //        Erg,
    //        Serir,
    //        Hamada_hills,
    //        Deathlands,
    //        Volcano,
    //        Ruins,
    //        Mountains,
    //        Dangeon,
    //        Caves,
    //        Tranches

    //    }
    
    

}
