namespace QuestSystem
{

    public class QuestInformation : IQuestInformation
    {
        private string name;
        private string description;
        private string hint;
        private string dialog;
        //private int chainQuestID;
        //private int sourceID;
        //private int questID;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string DescriptionSummary
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public string Hint
        {
            get
            {
                return hint;
            }

            set
            {
                hint = value;
            }
        }

        public string Dialog
        {
            get
            {
                return dialog;
            }

            set
            {
                dialog = value;
            }
        }

        //public int ChainQuestID
        //{
        //    get
        //    {
        //        return chainQuestID;
        //    }

        //    set
        //    {
        //        chainQuestID = value;
        //    }
        //}

        //public int SourceID
        //{
        //    get
        //    {
        //        return sourceID;
        //    }

        //    set
        //    {
        //        sourceID = value;
        //    }
        //}

        //public int QuestID
        //{
        //    get
        //    {
        //        return questID;
        //    }

        //    set
        //    {
        //        questID = value;
        //    }
        //}
    }
}
