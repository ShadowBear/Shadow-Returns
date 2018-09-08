namespace QuestSystem
{
    public class LocationObjective : IQuestObject
    {
        private string name;
        private string description;
        private bool isComplete;
        private bool isBonus;
        
        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

        public bool IsComplete
        {
            get
            {
                return isComplete;
            }
        }

        public bool IsBonus
        {
            get
            {
                return isBonus;
            }
        }

        public void CheckProgress()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateProgress()
        {
            throw new System.NotImplementedException();
        }
 
    }
}
