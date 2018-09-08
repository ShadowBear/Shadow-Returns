using System.Collections.Generic;
namespace QuestSystem
{
    public class Quest
    {
        private IQuestInformation information;
        public IQuestInformation Information
        {
            get { return information; }
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
        private List<IQuestObject> objectives;
            //Collection Objectives
                //kill 4 ...
                //5 feathers
            //Location Objective
                // a to b
                // Timebased

        //bonusObjectives
        //rewards
        //events
        //    on completetion
        //    on failed
        //    on update

        private bool CheckOverallProgress()
        {
            for (int i = 0; i < objectives.Count; i++)
            {
                if(!objectives[i].IsComplete && objectives[i].IsBonus == false)
                {
                    return false;
                }
            }
            return true; //get Reward!
        }
    }
}

