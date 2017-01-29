using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Game_engine_library
{
    public class Definition_OperateItem
    {
        #region Constructors
        /// <summary>
        /// A constructor used when you want to specify everything about the "operate Item" interaction.
        /// </summary>
        public Definition_OperateItem(Item inputItem, bool canBeOperated, List<string> keyCommandsAllowedToOperateItem, 
                                        List<Item> outcomeItems, string messageOnOperateAttempt, bool actionCanBeRepeated)
        {
            this.inputItem = inputItem;
            this.canBeOperated = canBeOperated;
            this.keyCommandsAllowedToOperateItem = keyCommandsAllowedToOperateItem;
            this.outcomeItems = outcomeItems;
            this.messageOnOperateAttempt = messageOnOperateAttempt;
            this.actionCanBeRepeated = actionCanBeRepeated;
            this.hasBeenTriedBefore = false;
        }

        /// <summary>
        /// A constructor useful when concerning an Item, that CANNOT be operated,
        /// however you  want to specify a unique message for such user attempt.
        /// </summary>
        public Definition_OperateItem (Item inputItem, List<string> keyCommandsAllowedToOperateItem, string messageOnUseAttempt, bool actionCanBeRepeated = true) 
            : this(inputItem, false, keyCommandsAllowedToOperateItem, new List<Item>() { }, messageOnUseAttempt, actionCanBeRepeated)
        { }
        
        /// <summary>
        /// A constructor useful when concerning an Item, that CANNOT be operated, with a default message.
        /// </summary>
        public Definition_OperateItem(Item inputItem)
            : this(inputItem, false, new List<string>() { }, new List<Item>() { }, 
                string.Format("Sorry, you cannot do this with {0}.", inputItem.Name), true)
        { }
        
        /// <summary>
        /// A constructor useful when concerning an Item, that CAN be operated,
        /// but you  want to specify ONLY the outcome, leaving the standard message for that Item interaction.
        /// </summary>
        public Definition_OperateItem (Item inputItem, List<string> keyCommandsAllowedToOperateItem, List<Item> outcomeItems, 
            bool actionCanBeRepeated = false)
            : this(inputItem, true, keyCommandsAllowedToOperateItem, outcomeItems,
            string.Format("You managed to operate the {0}. The resulting items appeared in your location.", inputItem.Name), 
            actionCanBeRepeated)
        { }
        #endregion

        #region Properties
        public Item inputItem { get; private set; }
        public bool canBeOperated { get; private set; }
        public List<string> keyCommandsAllowedToOperateItem { get; private set; }
        public List<Item> outcomeItems { get; private set; }
        public string messageOnOperateAttempt { get; private set; }
        public bool actionCanBeRepeated { get; private set; }
        public bool hasBeenTriedBefore { get; set; }
        #endregion
    }
}
