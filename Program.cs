using BattleSimulator.Characters;
using BattleSimulator.Interfaces;
using BattleSimulator.Battle;

namespace BattleSimulator
{
    class Program
    {
        static void Main()
        {
            Warrior pepcha = new("Pepcha");
            Mage googa = new("Googa");

            BattleManager battle = new(pepcha, googa);
            battle.StartBattle();
        }
    }
}

  