using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Text_games_input_parser_library_v2;

namespace Escape_Game_engine_library
{
    public class Game_Control_Center : ITextGameAction
    {
        public Game_Control_Center()
        {
            // Instantiating Main_character instance
            this.Player = new Main_character();

            #region creating MapOfLocations and setting starting location 
            // Instantiating MapOfLocations instance (creating a grid with our Locations)
            this.GridWithLocations = new Location[2, 3] 
            { 
              { garden,     library,            kitchen },
              { null,       swimmingPoolArea,   null    } 
            };
            this.MapOfLocations = new MapOfLocations(this.GridWithLocations);

            // Assigning a starting location for the player:
            this.Player.NorthSouth_RowPosition = 0;
            this.Player.WestEast_ColumnPosition = 0;
            this.Player.Location = this.MapOfLocations.GridWithLocations[Player.NorthSouth_RowPosition, Player.WestEast_ColumnPosition];
            #endregion


            #region Defining what items can be used with what, and what are the effects
            /// A List of Definition_UseItemWithItem objects. 
            /// Each object in this list forms a definition, of what should be the result of using one Item with another Item (in the game).
            /// The definition must contain exact instances of both of interacting Items. 
            /// The definition might contain: 
            ///     the message to be displayed as a result of interaction, 
            ///     and a List of Items that is the result of the interaction.   
            this.AllDefinitions_UseItemWithItem = new List<Definition_UseItemWithItem>() 
            {
                new Definition_UseItemWithItem 
                    (swimmingPool, cleaningNet, true, new List<Item>() { swimmingPool, cleaningNet, smallPlasticBall }, string.Format("You fished a {0} out of the water.", smallPlasticBall.Name), false),
                new Definition_UseItemWithItem 
                    (rake, grassyLawn, true, new List<Item>() { grassyLawn, rake, largeKey }, string.Format("You combed the thick grass with the {0} and found a {1}.", rake.Name, largeKey.Name), false),
                new Definition_UseItemWithItem 
                    (pocketKnife, leatherSuitcase, true, new List<Item>() { leatherSuitcase, pocketKnife, smallMetalKey }, string.Format("You cut open the {0} and found a {1}.", leatherSuitcase.Name, smallMetalKey.Name), false),
                new Definition_UseItemWithItem 
                    (smallMetalKey, cupboard, true, new List<Item>() { unlockedCupboard, smallMetalKey }, string.Format("The {0} fits. You unlocked the {1}.", smallMetalKey.Name, cupboard.Name), false),
                new Definition_UseItemWithItem 
                    (largeKey, cupboard, string.Format("The {0} does not fit in the {1} lock.", largeKey.Name, cupboard.Name)),
                new Definition_UseItemWithItem 
                    (mediumKey, cupboard, string.Format("The {0} does not fit in the {1} lock.", mediumKey.Name, cupboard.Name)),
                new Definition_UseItemWithItem 
                    (wrench, gardenHose, true, new List<Item>() { disconnectedGardenHose, wrench }, string.Format("You unscrewed the {0} with the {1}.", gardenHose.Name, wrench.Name), false),
                new Definition_UseItemWithItem 
                    (pocketKnife, smallPlasticBall, true, new List<Item>() { mediumKey, pocketKnife }, string.Format("You managed to open the {0} with the {1} and found in it a {2}.", smallPlasticBall.Name, pocketKnife.Name, mediumKey.Name), false),
                new Definition_UseItemWithItem 
                    (mediumKey, secretStorageCompartment, true, new List<Item>() { secretStorageCompartment, mediumKey, electricalWaterPump }, string.Format("You unlocked the {0} and found in it an {1}.", secretStorageCompartment.Name, electricalWaterPump.Name), false),
                new Definition_UseItemWithItem 
                    (smallMetalKey, secretStorageCompartment, string.Format("Nice try, but the {0} does not fit in the {1}'s lock.", smallMetalKey.Name, secretStorageCompartment.Name)),
                new Definition_UseItemWithItem 
                    (largeKey, secretStorageCompartment, string.Format("Nice try, but the {0} does not fit in the {1}'s lock.", largeKey.Name, secretStorageCompartment.Name)),
                new Definition_UseItemWithItem 
                    (disconnectedGardenHose, electricalWaterPump, true, new List<Item>() { electricalWaterPumpWithHose }, string.Format("You attached the {0} to the {1} and thus you have put together a potentially working water pump.", disconnectedGardenHose.Name, electricalWaterPump.Name), false),        
                new Definition_UseItemWithItem 
                    (electricalWaterPump, swimmingPool, string.Format("The {0} has no tube. You can't pump water with it.", electricalWaterPump.Name), true),
                new Definition_UseItemWithItem 
                    (electricalWaterPumpWithHose, swimmingPool, true, new List<Item>() {electricalWaterPumpWithHose, drainedSwimmingPool, hiddenDoor }, string.Format("You pumped the water out of the {0}. In the bottom of the {1} you notice a {2}.", swimmingPool.Name, drainedSwimmingPool.Name, hiddenDoor.Name), false),
                new Definition_UseItemWithItem 
                    (largeKey, hiddenDoor, true, new List<Item>() { largeKey, mysteriousTunnel }, string.Format("You unlocked the {0}. Behind it lies a {1}.", hiddenDoor.Name, mysteriousTunnel.Name), false),
                new Definition_UseItemWithItem 
                    (smallMetalKey, hiddenDoor, string.Format("Nice try, but the {0} does not fit in the {1}'s lock.", smallMetalKey.Name, hiddenDoor.Name)),
                new Definition_UseItemWithItem 
                    (mediumKey, hiddenDoor, string.Format("Nice try, but the {0} does not fit in the {1}'s lock.", mediumKey.Name, hiddenDoor.Name)),
                
                //TODO: to expand the game - define new "use item with item" type interactions here 
            };
            #endregion

            #region Defining what items can be operated, and what are the effects
            /// A List of Definition_OperateItem objects. 
            /// Each object in this list forms a definition, of what should be the result of "operating an Item" (in the game).
            /// The definition must contain the exact instance of Item to be operated on. 
            /// The definition might contain: 
            ///     the message to be displayed as a result of operating attempt, 
            ///     a List of Items that are the result of operating action,  
            ///     and a List of string that define which keyCommands are allowed as legitimate ways to operate this Item.
            ///            We can chose from the following keyCommands: "operate", "move", "read", "open", "close", ...
            this.AllDefinitions_OperateItem = new List<Definition_OperateItem>()
            {            
                new Definition_OperateItem 
                    (leatherSuitcase, new List<string> {"operate", "open"}, string.Format("You tried to open the {0}, but you can't seem to open it.", leatherSuitcase.Name)),
                new Definition_OperateItem 
                    (cupboard, new List<string> {"operate", "open"}, string.Format("You tried to open the {0}. It is locked.", cupboard.Name)),
                new Definition_OperateItem 
                    (bookcase, true, new List<string> {"operate", "move"}, new List<Item>() {bookcase, secretStorageCompartment }, string.Format("You pulled the {0} and revealed a {1}.", bookcase.Name, secretStorageCompartment.Name), false),
                new Definition_OperateItem 
                    (secretStorageCompartment, new List<string> {"operate", "open"}, string.Format("You tried to open the {0}, but it is locked.", secretStorageCompartment.Name)),
                new Definition_OperateItem 
                    (unlockedCupboard, true, new List<string> {"operate", "open"}, new List<Item>() { unlockedCupboard, wrench }, string.Format("You opened the {0} and found a {1} inside.", unlockedCupboard.Name, wrench.Name), false),
                new Definition_OperateItem 
                    (gardenHose, new List<string> {"operate", "move"}, string.Format("The {0} is screwed on to the wall. You won't unscrew it with your bare hands.", gardenHose.Name)),
                new Definition_OperateItem 
                    (electricalWaterPump, new List<string> {"operate"}, string.Format("The {0} has a buit-in battery. I suppose you could pump water with it, but it has no pipe.", electricalWaterPump.Name)),
                new Definition_OperateItem 
                    (swimmingPool, new List<string> {"operate"}, string.Format("Nice idea, but I don't have time for a swim just now. :) ")),
                new Definition_OperateItem 
                    (smallPlasticBall, new List<string> {"operate", "open"}, string.Format("Upon closer inspection you notice, that the {0} seems to be composed of two halves, however you are unable to open it with your bare hands.", smallPlasticBall.Name)),
                new Definition_OperateItem 
                    (hiddenDoor, new List<string> {"operate", "open", "move"}, string.Format("The {0} is locked. No way will I get it open without some tools.", hiddenDoor.Name)),
                new Definition_OperateItem 
                    (mysteriousTunnel, new List<string> {"operate"}, string.Format(" * Hooray! * Congratulations! *   You discovered the long lost... car keys ;)   You solved the mystery of the Villa. Now it's time to relax on the sunbed ;)")),

                //TODO: define new "operate item" type interactions here 
            };
            #endregion

        }

        #region declaring fields and properties to be set in constructor
        public Main_character Player { get; internal set; }
        public Location[,] GridWithLocations { get; private set; }
        public MapOfLocations MapOfLocations { get; private set; }
        private List<Definition_UseItemWithItem> AllDefinitions_UseItemWithItem;
        private List<Definition_OperateItem> AllDefinitions_OperateItem;
        #endregion



        #region Instantiating Item instances
        // Instantiating Item instances, which are meant to be:
        //   * with the player on start-up
        public Item pocketKnife = new Item("pocket knife", 
            new List<string>() { "pocket knife", "knife" },
            "It's a swiss army pocket knife. Could come in very handy.", 
            true);
        public Item plasticComb = new Item("plastic comb", 
            new List<string>() { "plastic comb", "comb" }, 
            "It's a small plastic comb.", 
            true);
        public Item packOfKleneex = new Item("pack of kleneex", 
            new List<string>() { "pack of kleneex", "pack", "kleneex" },
            "It's an ordinary pack of disposable tissues.", 
            true);
        //   * by the swimming pool on start-up
        public Item swimmingPool = new Item("swimming pool", 
            new List<string>() { "swimming pool", "pool" },
            "The swimming pool looks gorgeous, the water is cristal clear. I would love to jump into it.",
            false);
        public Item cleaningNet = new Item("cleaning net", 
            new List<string>() { "cleaning net", "net" }, 
            "It's a typical, hand-held skimmer net on a long metal pole. It's used for cleaning the pool, especially for removing leafs and so on from the water.",
            true);
        public Item sunbed = new Item("sunbed", 
            new List<string>() { "sunbed" },
            "It's a sunbed. Very good for taking a nap while sunbathing.", 
            false);
        //   * in the garden on start-up
        public Item grassyLawn = new Item("grassy lawn", 
            new List<string>() { "grassy lawn", "lawn", "grass" }, 
            "The grass looks very healthy with its vivid green. It's so dense, I imagine some little bugs must get lost in it all the time.", 
            false);
        public Item gardenHose = new Item("garden hose", 
            new List<string>() { "garden hose", "hose" },
            "The garden hose is connected to a tap sticking out of the wall.",   ///"It seems to be screwed in pretty tightly." 
            false);
        public Item gardenChair = new Item("garden chair", 
            new List<string>() { "garden chair", "chair" }, 
            "It's a standard garden chair.", 
            false);
        public Item rake = new Item("rake", 
            new List<string>() { "rake" }, 
            "The rake is for combing the garden. And for setting \"Tom and Jerry\" type traps. ;) ", 
            true);
        //   * in the kitchen on start-up
        public Item cupboard = new Item("cupboard", 
            new List<string>() { "cupboard" }, 
            "It's a kitchen cupboard hanging on the wall above the sink.", 
            false);
        //   * in the library on start-up
        public Item antiqueArmchair = new Item("antique armchair", 
            new List<string>() { "antique armchair", "armchair" }, 
            "It's a large, comfortable looking armchair. Perfect for kicking back with an interesting novel.", 
            false);
        public Item bookcase = new Item("bookcase", 
            new List<string>() { "bookcase" }, 
            "It's a hudge, multi segment bookcase covering the whole wall of the room.",
            false);
        public Item leatherSuitcase = new Item("leather suitcase", 
            new List<string>() { "leather suitcase", "suitcase" },
            "It's a sturdy looking briefcase. I would look really important carrying it. :) ", 
            true);

        // Instantiating Item istances, which are the efect of using Items on each other: 
        public Item smallPlasticBall = new Item("small plastic ball", 
            new List<string>() { "small plastic ball", "small ball", "plastic ball", "ball" },
            "It's a plastic ball the size of an apple. It resembles a large \"Kinder Surprise\" egg.", 
            true);
        public Item smallMetalKey = new Item("small metal key", 
            new List<string>() { "small metal key", "small key", "metal key" }, 
            "It's a small key made of metal.", 
            true);
        public Item disconnectedGardenHose = new Item("disconnected garden hose", 
            new List<string>() { "disconnected garden hose", "disconnected hose", "garden hose", "hose" }, 
            "It's a coil of rubber hose, now disconnected from the tap.", 
            true);
        public Item unlockedCupboard = new Item("unlocked cupboard", 
            new List<string>() { "unlocked cupboard", "cupboard" }, 
            "It's the kitchen cupboard you unlocked.", 
            false);
        public Item secretStorageCompartment = new Item("secret storage compartment", 
            new List<string>() { "secret storage compartment", "secret compartment", "storage compartment", "compartment" },
            "You see a door behind the moved bookcase. There's probably a secret storage compartment behind this door.", 
            false);
        public Item mediumKey = new Item("medium key", 
            new List<string>() { "medium key" }, 
            "It's a medium-sized key.", 
            true);
        public Item wrench = new Item("wrench", 
            new List<string>() { "wrench" }, 
            "It's a tool - an adjustable metal wrench. Could be used for fixing things.", 
            true);
        public Item electricalWaterPump = new Item("electrical water pump", 
            new List<string>() { "electrical water pump", "electrical pump", "water pump", "pump" }, 
            "It's a battery powered water pump. It's used for pumping water I guess.", 
            true);
        public Item largeKey = new Item("large key", 
            new List<string>() { "large key" }, 
            "It's a large, brass key.", 
            true);
        public Item hiddenDoor = new Item("hidden door", 
            new List<string>() { "hidden door", "door" }, 
            "Ther's a trapdoor hidden in the bottom of the swimming pool. Amazing!", 
            false);
        public Item electricalWaterPumpWithHose = new Item("electrical water pump with hose",
            new List<string>() { "electrical water pump with hose", "electrical pump with hose", "water pump with hose", "electrical water pump", "electrical pump", "water pump", "pump with hose", "pump" },
            "It's a battery powered water pump with a rubber hose attached to it.", 
            true);
        public Item drainedSwimmingPool = new Item("drained swimming pool",
            new List<string>() { "drained swimming pool", "swimming pool", "drained pool", "pool" },
            "There's something at the bottom of the empty swimming pool.", 
            false);
        public Item mysteriousTunnel = new Item("mysterious tunnel", 
            new List<string>() { "mysterious tunnel", "tunnel" }, 
            "What a mysterious tunnel hidden in the bottom of a swimming pool...", 
            false);
        #endregion

        #region Instantiating Location instances
        // Instantiating Location instances:
        public Location kitchen = new Location("the kitchen",
@"You are in a modern and well equipped kitchen. It's stylish and well designed.
There is almost any kitchen appliance here, that you can imagine.
The only doorway looks out due west and leads straight into the library.");
        public Location library = new Location("the library",
@"You find yourself in a library which also acts as a study. 
You can't help but notice how relaxing and cozy it feels, even despite its considerable size. Whoever designed it had very good taste.
There are three exits from the library. 
You can go out of the house by walking through the door due south - to the get to the swimming pool area. 
If you take the western exit you will leave for the garden.
Or you can go east to go into the kitchen.");
        public Location swimmingPoolArea = new Location("the swimming pool area", 
@"You are outside, in the swimming pool area. 
The owner of the villa had put in a tempting looking swimming pool in their backyard.
If you go due north, you will enter the house and walk into the library.");
        public Location garden = new Location("the garden", 
@"You find yourself in a beautiful garden with a nice, dense lawn full of thick grass.
It's easy to tell, that this garden is maintained with great care.
Going east of here will take you inside the house - to the library.");
        #endregion

        #region Filling Locations and Character's inventory with initial Items on game startup
        /// <summary>
        /// This method is to be called before the game starts or whenever it is restarted. 
        /// It fills (adds) Item instances to Location instances and to the Main_character instance, 
        /// that are meant to be there, when the game starts.
        /// </summary>
        public void FillLocationAndCharacterWithInitialItemsOnGameStartup()
        {
            // Adding Item instances to Location instances (filling Locations' content with their initial Item objects) 
            this.swimmingPoolArea.Content = new List<Item>() { swimmingPool, cleaningNet, sunbed };
            this.garden.Content = new List<Item>() { grassyLawn, gardenHose, gardenChair, rake };
            this.kitchen.Content = new List<Item>() { cupboard };
            this.library.Content = new List<Item>() { bookcase, antiqueArmchair, leatherSuitcase };

            // Adding Item instances to the Main_character instance (filling Main_character's inventory with initial Item objects) 
            this.Player.Inventory = new List<Item>() { packOfKleneex, plasticComb, pocketKnife };
        }
        #endregion




        #region Implementing ITextGameAction interface - Bulk operations on Items etc...
        public string TakeItems(List<string> potentialItems)
        {
            string finalMessage = "";
            foreach (string word in potentialItems)
            {
                string newMessageFragment = this.PickUpItem(word);
                if (newMessageFragment != ""  &&  !finalMessage.Contains(newMessageFragment) )
                    finalMessage = String.Format("{0}\n{1}", finalMessage, newMessageFragment);
            }
            return finalMessage;
        }

        public string DropItems(List<string> potentialItems)
        {
            string finalMessage = "";
            foreach (string word in potentialItems)
            {
                string newMessageFragment = this.LeaveItem(word);
                if (newMessageFragment != ""  &&  !finalMessage.Contains(newMessageFragment) )
                    finalMessage = String.Format("{0}\n{1}", finalMessage, newMessageFragment);
            }
            return finalMessage;
        }

        public string ExamineItems(List<string> potentialItems)
        {
            string finalMessage = "";
            foreach (string word in potentialItems)
            {
                string newMessageFragment = this.ExamineItem(word);
                if (newMessageFragment != ""  &&  !finalMessage.Contains(newMessageFragment))
                    finalMessage = String.Format("{0}\n{1}", finalMessage, newMessageFragment);
            }
            return finalMessage;
        }

        public string OperateItems(string keyCommandWhichTriggeredOperate, List<string> potentialItems)
        {
            string finalMessage = "";
            foreach (string word in potentialItems)
            {
                string newMessageFragment = this.OperateItem(keyCommandWhichTriggeredOperate, word);
                if (newMessageFragment != "" && !finalMessage.Contains(newMessageFragment))
                    finalMessage = String.Format("{0}\n{1}", finalMessage, newMessageFragment);
            }
            return finalMessage;
        }

        public string UseItems(List<string> potentialItems)
        {
            string finalMessage = "";
            //we assume, that the player will use a grammatical construct: "use ... with ..." or "use ... on ..." 
            if (potentialItems.Contains("with") || potentialItems.Contains("on"))
            {
                //attempting to "use item with item"
                foreach (string word1 in potentialItems)
                {
                    string newMessageFragment = "";
                    foreach (string word2 in potentialItems)
                    {
                        if (word1 != word2)
                        {
                            newMessageFragment = this.UseItem(word1, word2);
                            if (newMessageFragment != "")
                                break;
                        }
                    }
                    if (newMessageFragment != "")
                    {
                        finalMessage = String.Format("{0}\n{1}", finalMessage, newMessageFragment);
                        break;
                    }
                }
            }
            // if "use item with item" didn't work, we attempt to trigger operating an item (with keyCommand = "operate")
            if (finalMessage == "" && !potentialItems.Contains("with") && !potentialItems.Contains("on") )
            {
                foreach (string word in potentialItems)
                {
                    string newMessageFragment = this.OperateItem("operate", word);
                    if (newMessageFragment != "" && !finalMessage.Contains(newMessageFragment))
                        finalMessage = String.Format("{0}\n{1}", finalMessage, newMessageFragment);
                }
            }
            return finalMessage;
        }
        #endregion

        #region Implementing ITextGameAction interface - GoTo
        public string GoTo(List<string> potentialDirectionsOrLocations)
        {
            string finalMessage = "";
            foreach (string word in potentialDirectionsOrLocations)
            {
                switch (word)
                {
                    case "north":
                        finalMessage = this.GoDue(Directions.north);
                        return finalMessage;
                    case "south":
                        finalMessage = this.GoDue(Directions.south);
                        return finalMessage;
                    case "west":
                        finalMessage = this.GoDue(Directions.west);
                        return finalMessage;
                    case "east":
                        finalMessage = this.GoDue(Directions.east);
                        return finalMessage;
                    case "northeast":
                        finalMessage = this.GoDue(Directions.northeast);
                        return finalMessage;
                    case "northwest":
                        finalMessage = this.GoDue(Directions.northwest);
                        return finalMessage;
                    case "southeast":
                        finalMessage = this.GoDue(Directions.southeast);
                        return finalMessage;
                    case "southwest":
                        finalMessage = this.GoDue(Directions.southwest);
                        return finalMessage;
                }
            }
            return finalMessage;
        }
        #endregion

        #region Implementing ITextGameAction interface - LookAround
        public string LookAround()
        {
            return String.Format("You looked around.\n{0}", this.Player.Location.Discription);
        }
        #endregion



        #region operate single Item functionality
        /// <summary>
        /// This method returns a Definition_OperateItem object (instance), which defines the effects of "operation" attempt
        /// on Item passed in as argument.
        /// If "operation" attempt was not defined for given Item, it returns a new Definition_OperateItem instance, 
        /// which is build using a constructor (with default settings) for Item that can't be operated on in the game. 
        /// </summary>
        private Definition_OperateItem GetInstanceOfDefinitionOfOperate(Item inputItem)
        {
            foreach (Definition_OperateItem definition in AllDefinitions_OperateItem)
            {
                if (definition.inputItem == inputItem)
                {
                    return definition;
                }
            }
            return new Definition_OperateItem(inputItem);
        }


        /// <summary>
        /// This method manages all the logic related to OPERATING ITEM game functionality.
        /// </summary>
        /// <param name="userInput">string input from user input text-box</param>
        /// <returns>string message to be displayed to the user</returns>
        public string OperateItem(string keyCommand, string userInput)
        {
            string message = "";

            if ( this.IsItemInstanceWithGivenNamePresent(userInput, this.Player.Inventory)
                ||  this.IsItemInstanceWithGivenNamePresent(userInput, this.Player.Location.Content) )
            {
                Item itemInQuestion;
                if (this.IsItemInstanceWithGivenNamePresent(userInput, this.Player.Inventory))
                    itemInQuestion = this.ReturnItemInstanceWithGivenName(userInput, this.Player.Inventory);
                else
                    itemInQuestion = this.ReturnItemInstanceWithGivenName(userInput, this.Player.Location.Content);
               
                Debug.Assert(this.Player.Location.Content.Contains(itemInQuestion) || Player.Inventory.Contains(itemInQuestion));

                Definition_OperateItem definition = GetInstanceOfDefinitionOfOperate(itemInQuestion);
                if (definition.keyCommandsAllowedToOperateItem.Contains(keyCommand))
                {
                    message = definition.messageOnOperateAttempt;
                    if (definition.hasBeenTriedBefore && !definition.actionCanBeRepeated)
                    {
                        message = String.Format("You've already utilized the {0}. There's no point in trying it again.", itemInQuestion.Name);
                    }
                    else if ((definition.canBeOperated && definition.actionCanBeRepeated)
                            || (definition.canBeOperated && !definition.hasBeenTriedBefore))
                    {
                        Debug.Assert(definition.actionCanBeRepeated || !definition.hasBeenTriedBefore);

                        if (Player.Inventory.Contains(itemInQuestion))
                            Player.LeaveChosenItem(itemInQuestion);
                        else if (this.Player.Location.Content.Contains(itemInQuestion))
                            this.Player.Location.TakeItemFromLocation(itemInQuestion);

                        foreach (Item outcomeItem in definition.outcomeItems)
                        {
                            this.Player.Location.AddItemToLocation(outcomeItem);
                        }
                        definition.hasBeenTriedBefore = true;
                    }
                }
                else if (!definition.keyCommandsAllowedToOperateItem.Contains(keyCommand))
                {
                    message = String.Format("You cannot {0} the {1}!", keyCommand, itemInQuestion.Name);
                }
            }
            return message;
        }
        #endregion

        #region use single Item with single Item functionality
        /// <summary>
        /// This method returns a Definition_UseItemWithItem object which defines the interacion 
        /// between Items passed in as arguments.
        /// If an interaction was not defined for given Items, it returns a new Definition_UseItemWithItem instance, 
        /// which is build using a constructor for Items that are not interacting in the game. 
        /// </summary>
        private Definition_UseItemWithItem GetInstanceOfDefinitionOfUse(Item inputItem1, Item inputItem2)
        {
            foreach (Definition_UseItemWithItem definition in AllDefinitions_UseItemWithItem)
            {
                if ((definition.inputItem1 == inputItem1 && definition.inputItem2 == inputItem2) 
                    || (definition.inputItem1 == inputItem2 && definition.inputItem2 == inputItem1))
                {
                    return definition;
                }
            }
            var result = new Definition_UseItemWithItem(inputItem1, inputItem2);
            return result;
        }

        
        /// <summary>
        /// This method manages all the logic related to USING ITEM WITH ITEM game functionality.
        /// </summary>
        /// <param name="input1">string input from 1-st text-box</param>
        /// <param name="input2">string input from 2-nd text-box</param>
        /// <returns>string message to be displayed to the user</returns>
        public string UseItem(string input1, string input2)
        {
            string message = "";

            if ( (IsItemInstanceWithGivenNamePresent(input1, this.Player.Inventory) || IsItemInstanceWithGivenNamePresent(input1, this.Player.Location.Content)) 
                && (IsItemInstanceWithGivenNamePresent(input2, this.Player.Inventory) || IsItemInstanceWithGivenNamePresent(input2, this.Player.Location.Content)) )
            {
                Debug.Assert((IsItemInstanceWithGivenNamePresent(input1, this.Player.Inventory) || IsItemInstanceWithGivenNamePresent(input1, this.Player.Location.Content)));
                Debug.Assert((IsItemInstanceWithGivenNamePresent(input2, this.Player.Inventory) || IsItemInstanceWithGivenNamePresent(input2, this.Player.Location.Content)));
                Item item1;
                Item item2;

                if (IsItemInstanceWithGivenNamePresent(input1, Player.Inventory))
                    item1 = ReturnItemInstanceWithGivenName(input1, Player.Inventory);
                else
                    item1 = ReturnItemInstanceWithGivenName(input1, this.Player.Location.Content);
                if (IsItemInstanceWithGivenNamePresent(input2, Player.Inventory))
                    item2 = ReturnItemInstanceWithGivenName(input2, Player.Inventory);
                else
                    item2 = ReturnItemInstanceWithGivenName(input2, this.Player.Location.Content);

                Debug.Assert(this.Player.Location.Content.Contains(item1) || Player.Inventory.Contains(item1));
                Debug.Assert(this.Player.Location.Content.Contains(item2) || Player.Inventory.Contains(item2));

                Definition_UseItemWithItem definition = GetInstanceOfDefinitionOfUse(item1, item2);
                message = definition.messageOnUseAttempt;
                if (definition.hasBeenTriedBefore && !definition.actionCanBeRepeated)
                {
                    message = String.Format("You've already used {0} with {1}. Try something new.", item1.Name, item2.Name);
                }
                else if ((definition.canBeUsedTogether && definition.actionCanBeRepeated)
                    || (definition.canBeUsedTogether && !definition.hasBeenTriedBefore))
                {
                    Debug.Assert(definition.actionCanBeRepeated || !definition.hasBeenTriedBefore);

                    if (Player.Inventory.Contains(item1))
                        Player.LeaveChosenItem(item1);
                    else if (this.Player.Location.Content.Contains(item1))
                        this.Player.Location.TakeItemFromLocation(item1);
                    if (Player.Inventory.Contains(item2))
                        Player.LeaveChosenItem(item2);
                    else if (this.Player.Location.Content.Contains(item2))
                        this.Player.Location.TakeItemFromLocation(item2);

                    foreach (Item item in definition.outcomeItems)
                    {
                        this.Player.Location.AddItemToLocation(item);
                    }
                    definition.hasBeenTriedBefore = true;
                }
            }
            return message;
        }
        #endregion

        #region pick up single Item functionality
        /// <summary>
        /// This method manages all the logic related to PICKING UP ITEM game functionality.
        /// </summary>
        /// <returns>string message to be displayed to the user</returns>
        public string PickUpItem(string userInput)
        {
            string message = "";

            if ( this.IsItemInstanceWithGivenNamePresent(userInput, this.Player.Location.Content) )
            {
                Item itemInQuestion = this.ReturnItemInstanceWithGivenName(userInput, this.Player.Location.Content);

                if (itemInQuestion.CanItBeMooved)
                {
                    this.Player.Location.TakeItemFromLocation(itemInQuestion);
                    this.Player.PickUpItem(itemInQuestion);
                    message = string.Format("You picked up {0}.", itemInQuestion.Name);
                }
                else
                {
                    message = string.Format("You tried to pick up {0}, but it cannot be taken!", itemInQuestion.Name);
                }
            }
            return message;
        }
        #endregion

        #region leave single Item functionality
        /// <summary>
        /// This method manages all the logic related to LEAVING ITEM game functionality.
        /// </summary>
        /// <returns>string message to be displayed to the user</returns>
        public string LeaveItem(string userInput)
        {
            string message = "";

            if (this.IsItemInstanceWithGivenNamePresent(userInput, this.Player.Inventory))
            {
                Item itemInQuestion = this.ReturnItemInstanceWithGivenName(userInput, this.Player.Inventory);

                this.Player.LeaveChosenItem(itemInQuestion);
                this.Player.Location.AddItemToLocation(itemInQuestion);

                message = string.Format("You left {0} in {1}.", itemInQuestion.Name, this.Player.Location.Name);
            }
            return message;
        }
        #endregion

        #region examine single Item functionality
        /// <summary>
        /// This method manages all the logic related to LEAVING ITEM game functionality.
        /// </summary>
        /// <returns>string message to be displayed to the user</returns>
        public string ExamineItem(string userInput)
        {
            string message = "";

            if ( this.IsItemInstanceWithGivenNamePresent(userInput, this.Player.Inventory) 
                || this.IsItemInstanceWithGivenNamePresent(userInput, this.Player.Location.Content) )
            {
                Item itemInQuestion;
                if (this.IsItemInstanceWithGivenNamePresent(userInput, this.Player.Inventory))
                    itemInQuestion = this.ReturnItemInstanceWithGivenName(userInput, this.Player.Inventory);
                else 
                    itemInQuestion = this.ReturnItemInstanceWithGivenName(userInput, this.Player.Location.Content);

                message = string.Format("You examined the {0} closely. {1}", itemInQuestion.Name, itemInQuestion.Discription);
            }
            return message;
        }
        #endregion

        #region GoDue... functionality
        public string GoDue(Directions direction)
        {
            string message = "";

            int newRowPosition = Player.NorthSouth_RowPosition + MapOfLocations.changeOfRowIndex[direction];
            int newColumnPosition = Player.WestEast_ColumnPosition + MapOfLocations.changeOfColumnIndex[direction];
            
            if ( newRowPosition >= 0  &&  newColumnPosition >= 0  
                &&  newRowPosition < MapOfLocations.GridWithLocations.GetLength(0)
                &&  newColumnPosition < MapOfLocations.GridWithLocations.GetLength(1)
                &&  MapOfLocations.GridWithLocations[newRowPosition, newColumnPosition] != null )
            {
                this.Player.NorthSouth_RowPosition = newRowPosition;
                this.Player.WestEast_ColumnPosition = newColumnPosition;
                this.Player.Location = this.MapOfLocations.GridWithLocations[newRowPosition, newColumnPosition];
                message = String.Format("You went {0}. \n\n{1}", direction, this.Player.Location.Discription);
            }
            else 
            {
                message = String.Format("You can't go due {0} from here!", direction);
            }
            return message;
        }
        #endregion



        #region Looking for Item instances with a given name, Returning Item instances with a given name
        /// <summary>
        /// Return true if an instance of class Item, which Name matches theName, is present in a given list.
        /// </summary>
        private bool IsItemInstanceWithGivenNamePresent(string theName, List<Item> list)
        {
            foreach (Item item in list)
            {
                if (item.SynonymsForItemName.Contains(theName))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Return an actual instance of class Item, which Name matches given argument, from a given List of Items.  
        /// </summary>
        private Item ReturnItemInstanceWithGivenName(string theName, List<Item> list)
        {
            foreach (Item item in list)
            {
                if (item.SynonymsForItemName.Contains(theName))
                {
                    return item;
                }
            }
            return null;
        }
        #endregion

    }
}
