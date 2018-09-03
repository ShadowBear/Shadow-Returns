

namespace QuestSystem
{
    public interface IQuestObject 
    {

        string Name { get; }
        string Description { get; }
        bool IsComplete { get; }
        bool IsBonus { get; }
        void UpdateProgress();
        void CheckProgress();
    }
}

