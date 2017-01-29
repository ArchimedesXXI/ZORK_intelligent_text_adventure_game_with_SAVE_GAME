using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Game_engine_library
{
    /// <summary>
    /// This class is forming a MAP of what happens if we try to use two Items on each other. 
    /// On instantiation this class defines two Items that we want to use on each other,
    /// what the resulting Items will be (the outcome), and what message is to be displayed to the user.
    /// </summary>
    public class Definition_UseItemWithItem
    {
        #region Constructors
        /// <summary>
        /// A constructor used when you want to specify everything about the "use Item with Item" interaction.
        /// </summary>
        public Definition_UseItemWithItem(Item inputItem1, Item inputItem2, bool canBeUsedTogether, 
                                            List<Item> outcomeItems, string messageOnUseAttempt, bool actionCanBeRepeated)
        {
            this.inputItem1 = inputItem1;
            this.inputItem2 = inputItem2;
            this.canBeUsedTogether = canBeUsedTogether;
            this.outcomeItems = outcomeItems;
            this.messageOnUseAttempt = messageOnUseAttempt;
            this.actionCanBeRepeated = actionCanBeRepeated;
            this.hasBeenTriedBefore = false;
        }

        /// <summary>
        /// A constructor useful when concerning two Items, that CANNOT be used together,
        /// however you  want to specify a unique message for such user attempt.
        /// </summary>
        public Definition_UseItemWithItem 
            (Item inputItem1, Item inputItem2, string messageOnUseAttempt, bool actionCanBeRepeated = true)
            : this(inputItem1, inputItem2, false, new List<Item>() { }, messageOnUseAttempt, actionCanBeRepeated)
        { }
        
        /// <summary>
        /// A constructor useful when concerning two Items, that CANNOT be used together, with a default message.
        /// </summary>
        public Definition_UseItemWithItem(Item inputItem1, Item inputItem2)
            : this(inputItem1, inputItem2, false, new List<Item>() { }, "", true)
        {
            if (inputItem1 == inputItem2)
                this.messageOnUseAttempt = "";
            else if (inputItem1 != inputItem2)
                this.messageOnUseAttempt = string.Format("You cannot use {0} with {1}.", inputItem1.Name, inputItem2.Name);
        }
        
        /// <summary>
        /// A constructor useful when concerning two Items, that CAN be used together,
        /// but you  want to specify ONLY the outcome, leaving the standard message for that Item interaction.
        /// </summary>
        public Definition_UseItemWithItem
            (Item inputItem1, Item inputItem2, List<Item> outcomeItems, bool actionCanBeRepeated = false) 
            : this (inputItem1, inputItem2, true, outcomeItems,
            string.Format("You used {0} with {1}. The resulting items appeared in your location.", inputItem1.Name, inputItem2.Name),
            actionCanBeRepeated)
        { }
        #endregion

        #region Properties
        public Item inputItem1 { get; private set; }
        public Item inputItem2 { get; private set; }
        public bool canBeUsedTogether { get; private set; }
        public List<Item> outcomeItems { get; private set; }
        public string messageOnUseAttempt { get; private set; }
        public bool actionCanBeRepeated { get; private set; }
        public bool hasBeenTriedBefore { get; set; }
        #endregion
    }
}
