using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Game_engine_library
{
    public class Main_character
    {
        #region Constructors
        
        public Main_character(string theName)
        {
            this.name = theName;
            this.inventory = new List<Item>();
        }

        public Main_character() : this("Anonymous") { }

        #endregion

        #region Fields
        private string name;
        private List<Item> inventory;
        private Location location; 
        private int northSouth_RowPosition; 
        private int westEast_ColumnPosition; 
        #endregion


        #region Properties

        public string Name
        {
            get { return this.name; }
        }

        public List<Item> Inventory
        {
            get { return this.inventory; }
            set { this.inventory = value; }
        }

        public Location Location
        {
            get { return this.location; }
            set { this.location = value; }
        }
        public int NorthSouth_RowPosition
        {
            get { return this.northSouth_RowPosition; }
            set { this.northSouth_RowPosition = value; }
        }
        public int WestEast_ColumnPosition
        {
            get { return this.westEast_ColumnPosition; }
            set { this.westEast_ColumnPosition = value; }
        }

        #endregion


        #region Methods

        public void PickUpItem(Item item)
        {
            this.inventory.Add(item);
        }

        public Item LeaveItemFromSlot(int slot_num)
        {
            Item i = this.inventory[slot_num];
            this.inventory.RemoveAt(slot_num);
            return i;
        }

        public void LeaveChosenItem(Item item)
        {
            this.inventory.Remove(item);
        }

        /// <summary>
        /// Prepares a string, containing the inventory of a the Main_character, suitable for displaying in GUI.
        /// </summary>
        public string GetCharactersInventoryAsString()
        {
            string result = "";
            foreach (Item item in this.inventory)
            {
                result = string.Format("{0} {1} \n", result, item.Name);
            }
            return result;
        }

        /// <summary>
        /// Prepares a list of string, containing names of all of the Items in the inventory of the Main_character.
        /// </summary>
        /// <returns></returns>
        public List<string> GetCharactersInventoryAsListOfString()
        {
            List<string> result = new List<string>();
            foreach (Item item in this.inventory)
            {
                result.Add(item.Name);
            }
            return result;
        }

        #endregion

    }
}
