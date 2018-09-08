namespace QuestSystem
{
    public interface IQuest
    {
        string Name { get; }
        string DescriptionSummary { get; }
        string Hint { get; }
        string Dialog { get; }
        int SourceID { get; }
        int ChainQuestID { get; }
        int QuestID { get; }
    }
}


//Name
//DescriptionSummary
//Quest Hint
//Quest Dialog
//SourceId
//QuestId
//chain Quest and next
//chainQuestID
//Objectives
//bonusObjectives
//rewards
//events
//    on completetion
//    on failed
//    on update