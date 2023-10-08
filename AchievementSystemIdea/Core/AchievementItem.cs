using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementSystemIdea.Core
{
    public class AchievementItem
    {
        public enum ID
        {
            Kill100Goblins,
            ExploreFaerun
        }

        private ProgressBar _progressBar;
        public string Title { get; set; }

        // Below are the event delegates that the AchievementItem class will publish, so that any
        // event subscribers can be notified when an achievement has been updated or completed, and
        // handle their logic accoordingly, thus decoupling them all from the AchievementItem and
        // AchievmentSystem classes.
        public Action? OnUpdate { get; set; }
        public Action? OnComplete { get; set; }

        public ProgressBar ProgressBar { get => _progressBar; set => _progressBar = value; }
        public bool Completed { get; set; }

        public AchievementItem(string title, int totalProgress = 0)
        {
            Completed = false;
            Title = title;
            _progressBar = new ProgressBar(totalProgress, 0);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // This is where the magic happens. Notice that any time the ProgressBar is updated from this
        // class, that we emit either an OnUpdate() event or OnComplete() event accoordingly.
        // Keep in mind, this also allows us to emit an OnUpdate() or OnComplete() event in other circumstances,
        // not just from this method alone.
        //
        // Another thing to notice is that this is a virtual method. This is so that in the future, if we
        // want to implement different logic for updating the progress bar, we can inherit from the AchievementItem
        // class, for example ExplorationAchievementItem, and override this method to include new/extended logic.
        public virtual void AddProgress(int amount = 1)
        {
            // Guard clause
            if (Completed) return;

            if (_progressBar.Current + amount >= _progressBar.Total)
            {
                _progressBar.Current = _progressBar.Total;
                Completed = true;
                OnComplete?.Invoke();
                return;
            }

            _progressBar.Current += amount;
            OnUpdate?.Invoke();
        }
    }
}
