using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Game_engine_library
{
    public class Item
    {
        #region Constructors
        public Item(string theName, List<string> synonymsForItemName, string theDiscription, bool canItBeMooved = true)
        {
            this.name = theName;
            this.synonymsForItemName = synonymsForItemName;
            this.discription = theDiscription;
            this.canItBeMooved = canItBeMooved;
        }
        #endregion

        #region Fields
        private string name;
        private List<string> synonymsForItemName;
        private string discription;
        private bool canItBeMooved;
        #endregion

        #region Properties

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public List<string> SynonymsForItemName
        {
            get { return this.synonymsForItemName; }
            set { this.synonymsForItemName = value; }
        }

        public string Discription
        {
            get { return this.discription; }
            set { this.discription = value; }
        }

        public bool CanItBeMooved
        {
            get { return this.canItBeMooved; }
            set { this.canItBeMooved = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
