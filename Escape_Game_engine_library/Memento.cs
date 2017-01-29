using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Escape_Game_engine_library
{
    /// <summary>
    /// Objects (instances) of Memento class store the state of the game,
    /// as it was at the time when a save game operation was performed, 
    /// so that it can be restored later.
    /// </summary>
    public class Memento
    {
        public List<Item> ItemsInSwimmingPoolArea { get; set; }
        public List<Item> ItemsInGarden { get; set; }
        public List<Item> ItemsInKitchen { get; set; }
        public List<Item> ItemsInLibrary { get; set; }
        public List<Item> ItemsInPlayersInventory { get; set; }
        public Location PlayersLocation { get; set; }
        public int PlayersNorthSouth_RowPosition { get; set; }
        public int PlayersWestEast_ColumnPosition { get; set; }
    }
}
