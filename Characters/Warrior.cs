namespace BattleSimulator.Characters
{
    public class Warrior : Character, IModifiersCon, IRollable
    {
        public bool Rage { get; set; } = false;

        public int Roll()
        {
            Random random = new();
            return random.Next(1, 7);
        }
        public int CalculateWithMod()
        {
            int totaldamage = 0;

            totaldamage += AttackPower;

            totaldamage += Roll();

            if (Rage)
            {
                totaldamage += 2;
            }

            return totaldamage;
        }
        public override void Attack(Character target)
        {
            Console.WriteLine($"\n{Name} attacked {target.Name}!");
            int dealtDamage = target.TakeDamage(CalculateWithMod());

            if (target.Health == 0)
            {
                Console.WriteLine($"{Name} is striking with all might and deals {dealtDamage} damage! {target.Name} dies from injuries!");
                target.Dead = true;
            }
            else
            {
                Console.WriteLine($"{Name} dealt {dealtDamage} damage and {target.Name} has {target.Health} HP left!");
            }
        }
        public override int TakeDamage(int attack)
        {
            int damage = 0;

            if (Rage)
            {
                damage = attack - 2;
                Health -= damage;
                Console.WriteLine($"\n{Name} is in Rage mode, he blocks 2 damage!");
            }
            else
            {
                damage = attack - 1;
                Health -= damage;
                Console.WriteLine($"{Name} blocks 1 damage!");
            }

            if (!Rage && Health <= 10 && Health > 0)
            {
                Rage = true;
                Console.WriteLine($"{Name} HP dropeed below 4, rage mode activated!");
            }

            return damage;
        }
        public Warrior(string name) : base(50, 5, name)
        {
            Console.WriteLine("\nWarrior is created!");
            Info();
        }
        public override void Info()
        {
            Console.WriteLine($"\nHealth: {Health}\nAttackPower: {AttackPower}\nName: {Name}\nAbbilities: when being attacked warrior blocks 1 damage. If warrior's health dropped below 10- he deals 2 more damage with each attack and blocks 2 damage");
        }
    }
}