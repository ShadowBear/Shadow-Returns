using UnityEngine;

namespace QuestSystem{
    public class CollectionObjective : IQuestObject
    {

        private string name;
        private string description;
        private bool isComplete;
        private bool isBonus;
        private string verb;
        private int collectionAmount; //total amount we need
        private int currentAmount; //start amount
        private GameObject itemToCollect;

        //Collect 10 feathers
        /// <summary>
        /// This constructor builds a collection object.
        /// </summary>
        /// <param name="titleVerb">Describes the type if collection</param>
        /// <param name="totalamount">Amount requiered to complete</param>
        /// <param name="item">Item to be collected</param>
        /// <param name="descrip">What will be collected</param>
        /// <param name="bonus">Is this bonus</param>
        public CollectionObjective(string titleVerb, int totalamount, GameObject item, string descrip, bool bonus)
        {
            name = titleVerb + " " + totalamount + " " + item.name;
            verb = titleVerb;
            description = descrip;
            itemToCollect = item;
            collectionAmount = totalamount;
            currentAmount = 0;
            isBonus = bonus;
            CheckProgress();
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Description {
            get
            {
                return description;
            }
        }

        public int CollectionAmount
        {
            get
            {
                return collectionAmount;
            }
        }
        public int CurrentAmount
        {
            get
            {
                return currentAmount;
            }
        }
        public GameObject ItemToCollect
        {
            get
            {
                return itemToCollect;
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
            if (currentAmount >= collectionAmount) isComplete = true;
            else isComplete = false;
        }

        public void UpdateProgress()
        {
            throw new System.NotImplementedException();
        }

        // 0/20 feather gathered!
        public override string ToString()
        {
            return currentAmount + "/" + collectionAmount + " " + itemToCollect.name + " " + verb + "ed!";
        }
    }
}

