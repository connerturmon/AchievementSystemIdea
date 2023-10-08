using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementSystemIdea.Core
{
    ///////////////////////////////////////////////////////////////////////////////////////////////
    // Keep in mind while reading this class that if I do any UI programming for the game, this
    // will be dealt with on my end and is only here to show you how with this implementation of
    // the AchievementSystem, I can easily create systems for the UI that update itself whenever
    // the AchievementSystem class adds a new achievement, or there is an update to an achievement.
    public class UI
    {
        private AchievementSystem _achievementSystem;

        // We inject a reference to the achievementSystem so that the UI can add callbacks on
        // progressBar OnUpdate() and OnComplete(). Another solution would be to make the AchievementSystem
        // class a singleton or static class so that it can be accessed in any system without needing
        // a direct reference to the AchievementSystem class.
        public UI(AchievementSystem achievementSystem)
        {
            _achievementSystem = achievementSystem;
            _achievementSystem.OnAchievementAdded += AddAchievementAddedCallbacks;

            // Loop through our dictionary of achievement items and add our callbacks to each AchievementItem.
            foreach (KeyValuePair<AchievementItem.ID, AchievementItem> achievement in _achievementSystem.Achievements)
            {
                AddAchievementAddedCallbacks(achievement.Value);
            }
        }

        // Helper method to easily add our callbacks any time a new achievement is added to
        // the dictionary.
        private void AddAchievementAddedCallbacks(AchievementItem achievement)
        {
            // What the UI should do on update/completion. Now it does not have to rely on the AchievementItem class
            // and the responsibility has been purely delegated to the UI class to handle changes to itself, thus
            // decoupling it from the AchievementItem class.
            achievement.OnUpdate += () =>
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Achievement: ");
                Console.ResetColor();
                Console.WriteLine($"Updated {achievement.Title} ({achievement.ProgressBar.Current}/{achievement.ProgressBar.Total})!");
            };

            achievement.OnComplete += () =>
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Achievement: ");
                Console.ResetColor();
                Console.WriteLine($"Updated {achievement.Title} ({achievement.ProgressBar.Current}/{achievement.ProgressBar.Total})!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Completed Achievement: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(achievement.Title + "!");
                Console.ResetColor();
            };
        }
    }
}
