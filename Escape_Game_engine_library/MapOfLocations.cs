using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_Game_engine_library
{
    public class MapOfLocations
    {
        #region Static constant fields with a "map" (Dictionary) of how to change position indices depending on the direction
        public static readonly Dictionary<Directions, int> changeOfRowIndex = new Dictionary<Directions, int>() 
        { 
            { Directions.north, -1 },
            { Directions.south, 1 },
            { Directions.west, 0 },
            { Directions.east, 0 },
            { Directions.northeast, -1 },
            { Directions.northwest, -1 },
            { Directions.southeast, 1 },
            { Directions.southwest, 1 },
        };
        public static readonly Dictionary<Directions, int> changeOfColumnIndex = new Dictionary<Directions, int>() 
        { 
            { Directions.north, 0 },
            { Directions.south, 0 },
            { Directions.west, -1 },
            { Directions.east, 1 },
            { Directions.northeast, 1 },
            { Directions.northwest, -1 },
            { Directions.southeast, 1 },
            { Directions.southwest, -1 },
        };
        #endregion


        #region Constructors
        public MapOfLocations(Location[,] gridWithLocations)
        {
            this.gridWithLocations = gridWithLocations;
        }
        #endregion

        #region Fields
        private Location[,] gridWithLocations;
        #endregion

        #region Properties
        public Location[,] GridWithLocations 
        {
            get { return gridWithLocations; }
        }
        #endregion

        


    }
}
