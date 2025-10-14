namespace BattleSimulator.Characters
{
    public abstract class Character
    {
        public bool Dead { get; set; } = false;
        private int health;
        public virtual int Health
        {
            get => health;
            set
            {
                if (value >= 0)
                    health = value;
                else
                    health = 0;
            }
        }
        private int attackpower;
        public int AttackPower
        {
            get => attackpower;
            set
            {
                if (value >= 0)
                    attackpower = value;
            }
        }
        public string? Name { get; set; }

        public Character(int health, int attackpower, string name)
        {
            Health = health;
            AttackPower = attackpower;
            Name = name;
        }
        public virtual void Attack(Character target)
        {
            int dealtDamage = target.TakeDamage(AttackPower);
            Console.WriteLine($"\n{Name} attacked {target.Name} and dealt {dealtDamage} damage");
            Console.WriteLine($"{target} has {target.Health} left");
        }
        public virtual int TakeDamage(int attack)
        {
            Health -= attack;
            return attack;
        }
        public virtual void Info()
        {
            Console.WriteLine($"\nHealth: {Health}\nAttackPower: {AttackPower}\nName: {Name}");
        }
    }
}