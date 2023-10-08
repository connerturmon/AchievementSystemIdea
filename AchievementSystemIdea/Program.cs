using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AchievementSystemIdea.Core;

namespace AchievementSystemIdea
{
    public class Program
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Please bear in mind while reading through this main method that ideally everything that is
        // happening here may be split across multiple files/systems. It is all in one method to show
        // a clear order of operations. The idea, however, is that any of these methods such as AddAchievement()
        // and AddProgress() can be called from anywhere in the program due to the modular event-driven
        // design. The only catch is any other system using the AchievementSystem will need a reference to
        // our AchievementSystem.
        //
        // A possible solution to this may be to make the AchievementSystem class a singleton or static,
        // so that any other systems and classes can easily get a reference to our AchievementSystem.
        public static void Main(string[] args)
        {
            // Create our Achievement and UI systems.
            AchievementSystem achievementSystem = new AchievementSystem();
            UI uiSystem = new UI(achievementSystem);

            // Create our achievements somewhere and add them to the achievement system dictionary.
            AchievementItem kill100Goblins = new AchievementItem("Kill 100 Goblins", 100);
            AchievementItem exploreFaerun = new AchievementItem("Explore All of Faerun", 3);

            achievementSystem.AddAchievement(AchievementItem.ID.Kill100Goblins, kill100Goblins);
            achievementSystem.AddAchievement(AchievementItem.ID.ExploreFaerun, exploreFaerun);

            // |IGNORE| UI code.
            #region _TESTING_GOBLIN_KILLS_UI
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-------------------------------");
            Console.WriteLine("  Testing Adding Goblin Kills  ");
            Console.WriteLine("-------------------------------");
            Console.ResetColor();
            #endregion

            // Some code later down the line that updates the kill goblins achievement
            // if you kill a goblin. Note we first begin by declaring an AchievementItem
            // instance to hold the data returned from our AchievementSystem dictionary.
            AchievementItem? achievement;
            achievementSystem.Achievements.TryGetValue(AchievementItem.ID.Kill100Goblins, out achievement);

            for (int i = 0; i < 9; i++)
                achievement?.AddProgress(1);

            achievement?.AddProgress(90);
            achievement?.AddProgress(1);


            // |IGNORE| More UI code.
            #region _TESTING_EXPLORATION_UI
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("----------------------------");
            Console.WriteLine("    Testing Exploration");
            Console.WriteLine("----------------------------");
            Console.ResetColor();
            #endregion

            // Some code later down the line when you explore a new region that adds
            // progress to the explored region achievement.
            achievementSystem.Achievements.TryGetValue(AchievementItem.ID.ExploreFaerun, out achievement);
            achievement?.AddProgress();
            achievement?.AddProgress();
            achievement?.AddProgress();
        }
    }
}