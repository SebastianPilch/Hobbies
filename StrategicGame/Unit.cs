

using System.Runtime.ExceptionServices;

namespace WPF_giera1
{
    public abstract class Unit
    {
        public int HP { get; set; }
        public int Mana { get; set; }
        public double Stamina { get; set; }
        public double Speed { get; set; }
        public abstract string name();
        public abstract int unit_id();
        public abstract int max_hp();
        public abstract int current_hp { get; set; }
        public abstract int armour();
        public abstract int current_armour();
        public abstract int accuracy();
        public abstract int current_accuracy();
        public abstract int dodge();
        public abstract int current_dodge();
        public abstract int damage();
        public abstract int current_damage();
        public abstract int attack_range();
        public abstract int current_attack_range();
        public abstract int inventory();
        public abstract int mana();
        public abstract int current_mana { get; set; }
        public abstract double stamina();
        public abstract double current_stamina { get; set; }
        public abstract double speed();
        public abstract double current_speed { get; set; }
        public abstract int sight();
        public class Sniper : Unit
        {
            public override int unit_id() { return 0; }
            public override int max_hp() { return 30; }
            new public int HP = 30;
            
            public override int current_hp { get { return HP; } set { HP = value; } }

            public override string name() {return "Sniper"; }

            public override int armour() { return 30; }
            public override int current_armour() { return 30; }
            public override int accuracy() { return 30; }
            public override int current_accuracy() { return 30; }
            public override int dodge() { return 30; }
            public override int current_dodge() { return 30; }
            public override int damage() { return 30; }
            public override int current_damage() { return 30; }
            public override int attack_range() { return 4; }
            public override int current_attack_range() { return 30; }
            public override int inventory() { return 30; }
            public override int mana() { return 50; }
            new public int Mana = 50;
            public override int current_mana { get { return Mana; } set { Mana = value; } }
            public override double stamina() { return 40; }
            new public double Stamina = 40;
            public override double current_stamina { get { return Stamina; } set { Stamina = value; } }
            new public double Speed = 5;
            public override double speed() { return 30; }
            public override double current_speed { get { return Speed; } set { Speed = value; } }

            public override int sight() { return 15; }


        }





        public abstract class UnitCreator
        {
            public abstract Unit FactoryMethod();
        }
        public class SniperCreator : UnitCreator
        {
            public override Unit FactoryMethod()
            {
                return new Sniper();
            }
        }
        /// <summary>
        /// A 'ConcreteCreator' class
        /// </summary>



        public class Unit_creators
        {

            public UnitCreator[] creators;
            public Unit_creators()
            {
                creators = new UnitCreator[1];
                creators[0] = new SniperCreator();

            }
        }
    }

}
