namespace BattleSimulator.Characters
{
    public class Mage : Character, IModifiersCon, IRollable
    {
        public int CalculateWithMod()
        {
            int totaldamage = 0;

            totaldamage += AttackPower;

            totaldamage += Roll();

            if (Mana > 0 && !NeedRefilMana)
            {
                totaldamage += 2;
                Mana--;
                ManaUsed = true;
                if (Mana == 0)
                    NeedRefilMana = true;
            }

            return totaldamage;
        }

        public override int TakeDamage(int attack)
        {
            int damage = 0;

            if (attack >= Health && ManaShield)
            {
                Console.WriteLine($"Mana shield is activated! Lethal damage is voided!");
                ManaShield = false;
                return damage;
            }

            Health -= attack;

            return damage += attack;
        }

        public int Roll()
        {
            Random random = new();
            return random.Next(1, 7);
        }
        public override void Attack(Character target)
        {
            Console.WriteLine($"\n{Name} attacked {target.Name}!");
            int dealtDamage = target.TakeDamage(CalculateWithMod());

            if (target.Health == 0)
            {
                if (ManaUsed)
                {
                    Console.WriteLine($"{Name} using 1 mana to buff magic missle and attacks with all might dealing {dealtDamage} damage! {target.Name} dies from injuries.");
                }
                else
                {
                    Console.WriteLine($"{Name} attacks with all might dealing {dealtDamage} damage! {target.Name} dies from injuries.");
                }
                target.Dead = true;
            }
            else
            {
                if (ManaUsed)
                {
                    Console.WriteLine($"{Name} is using 1 mana to buff magic missle to deal 2 bonus damage. {Name} deals {dealtDamage} damage and {target.Name} has {target.Health} HP left.");
                }
                else
                {
                    Console.WriteLine($"{Name} is using maggic missle and deals {dealtDamage} damage! {target.Name} has {target.Health} HP left!");
                }
            }

            ManaUsed = false;

            if (Mana == 0 && NeedRefilMana)
            {
                ManaCounter++;
                if (ManaCounter == 3)
                {
                    NeedRefilMana = false;
                }
            }
        }

        public bool ManaShield { get; set; } = true;
        public int Mana { get; set; } = 3;
        public int ManaCounter { get; set; } = 0;
        public bool ManaUsed { get; set; } = false;
        public bool NeedRefilMana { get; set; } = false;
        public Mage(string name) : base(40, 5, name)
        {
            Console.WriteLine("\nMage is created!");
            Info();
        }

        public override void Info()
        {
            Console.WriteLine($"\nHealth: {Health}\nAttackPower: {AttackPower}\nName: {Name}\nAbbilities: when attacks mage spends 1 of the 3 available mana points to deal 2 extra damage. Mana refills automatically after 3 turns. Once per game can block lethal damage");
        }
    }
}