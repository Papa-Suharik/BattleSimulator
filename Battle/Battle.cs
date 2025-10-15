using BattleSimulator.Characters;

namespace BattleSimulator.Battle
{
    public class BattleManager
    {
        private Character player1;
        private Character player2;

        public BattleManager(Character first, Character second)
        {
            player1 = first;
            player2 = second;
        }
        public void StartBattle()
        {
            Console.WriteLine($"\n||=====BATTLE BEGINS=====||");
            while (!player1.Dead || !player2.Dead)
            {
                player1.Attack(player2);
                if (player2.Dead)
                {
                    Console.WriteLine($"||====={player1.Name} HAS WON THE BATTLE!=====||");
                    break;
                }
                
                player2.Attack(player1);
                if(player1.Dead)
                {
                    Console.WriteLine($"||====={player2.Name} HAS WON THE BATTLE!=====||");
                    break;
                }
            }
        }
    }
}