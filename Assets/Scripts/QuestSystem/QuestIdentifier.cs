namespace QuestSystem
{
    public class QuestIdentifier : IQuestIdentifier
    {
        private int id;
        private int sourceID;
        private int chainQuestID;

        public int ID
        {
            get
            {
                return id;
            }
        }

        public int ChainQuestID
        {
            get
            {
                return chainQuestID;
            }
        }


        public int SourceID
        {
            get
            {
                return sourceID;
            }
        }
    }
}


