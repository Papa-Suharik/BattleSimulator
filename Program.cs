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

interface IModifiersCon
{
    public int CalculateWithMod();
}

interface IRollable
{
    public int Roll();
}
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
class Program
{
    static void Main()
    {
        Console.Clear();
        Warrior warrior = new("Kadudi");
        Mage mage = new("Googa");

        Game(warrior, mage);

        void Game(Character unit1, Character unit2)
        {
            while (true)
            {
                unit1.Attack(unit2);
                if (unit1.Dead || unit2.Dead)
                {
                    if (unit1.Dead == true)
                    {
                        Console.WriteLine($"\n{unit2.Name} has won the battle!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\n{unit1.Name} has won the battle!");
                        break;
                    }
                }
                unit2.Attack(unit1);
                if (unit1.Dead || unit2.Dead)
                {
                    if (unit1.Dead == true)
                    {
                        Console.WriteLine($"\n{unit2.Name} has won the battle!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\n{unit1.Name} has won the battle!");
                        break;
                    }
                }
            }
        }
    }
}

