using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementSystemIdea.Core
{
    public class AchievementSystem
    {
        public Dictionary<AchievementItem.ID, AchievementItem> Achievements { get; set; }
        public Action<AchievementItem>? OnAchievementAdded { get; set; }

        public AchievementSystem()
        {
            Achievements = new Dictionary<AchievementItem.ID, AchievementItem>();
        }

        // Notice any time an achievement is added, the AchievementSystem class invokes the
        // OnAchievementAdded() delegate, thus emitting an event signal to any systems that
        // are subscribed to the AchievementSystem such as the UI, Steam, etc.
        public void AddAchievement(AchievementItem.ID ID, AchievementItem achievement)
        {
            Achievements.Add(ID, achievement);
            OnAchievementAdded?.Invoke(achievement);
        }
    }
}
