using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Escape_Game_engine_library
{
    public class Location
    {
        #region Constructors

        public Location()
        {
            this.name = "unnamed_location";
            this.content = new List<Item>(0);
            this.discription = "No description available for this location.";
        }

        public Location(string theName)
        {
            this.name = theName;
            this.content = new List<Item>(0);
            this.discription = "No description available for this location.";
        }

        public Location(string theName, List<Item> theContent)
        {
            this.name = theName;
            this.content.AddRange(theContent);
            this.discription = "No description available for this location.";
        }
        
        public Location(string theName, string discriptionDisplayedToUser)
        {
            this.name = theName;
            this.content = new List<Item>(0);
            this.discription = discriptionDisplayedToUser;
        }

        public Location(string theName, List<Item> theContent, string discriptionDisplayedToUser)
        {
            this.name = theName;
            this.content.AddRange(theContent);
            this.discription = discriptionDisplayedToUser;
        }

        #endregion


        #region Fields
        private string name;
        private List<Item> content;
        private string discription;
        #endregion


        #region Properties

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public List<Item> Content
        {
            get { return this.content; }
            set { this.content = value; }
        }

        public string Discription
        {
            get { return this.discription; }
            set { this.discription = value; }
        }

        #endregion


        #region Methods

        public void AddItemToLocation(Item item)
        {
            this.content.Add(item);
        }

        public void TakeItemFromLocation(Item item)
        {
            this.content.Remove(item);
        }

        /// <summary>
        /// Prepares a string, containing the content of a this location, suitable for displaying in GUI.
        /// </summary>
        public string GetLocationContentAsString()
        {
            string result = "";
            foreach (Item item in this.content)
            {
                result = string.Format("{0} {1} \n", result, item.Name);   
            }
            return result;
        }

        /// <summary>
        /// Prepares a list of string, containing names of all of the Items in the content of this location.
        /// </summary>
        /// <returns></returns>
        public List<string> GetLocationContentAsListOfString()
        {
            List<string> result = new List<string>();
            foreach (Item item in this.content)
            {
                result.Add(item.Name);
            }
            return result;
        }

        #endregion

    }
}
