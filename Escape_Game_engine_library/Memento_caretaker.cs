using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Escape_Game_engine_library
{
    public class Memento_caretaker
    {
        private static string pathToSavegameFile = @"zork_savegame.json";

        public static string SaveGame(Game_Control_Center currentGame)
        {
            // instantiating a Memento object (a game saving object)
            Memento savedGame = new Memento();
            // storing the state of our current game in the Memento object
            savedGame.ItemsInGarden = currentGame.garden.Content;
            savedGame.ItemsInKitchen = currentGame.kitchen.Content;
            savedGame.ItemsInLibrary = currentGame.library.Content;
            savedGame.ItemsInSwimmingPoolArea = currentGame.swimmingPoolArea.Content;
            savedGame.ItemsInPlayersInventory = currentGame.Player.Inventory;
            savedGame.PlayersLocation = currentGame.Player.Location;
            savedGame.PlayersNorthSouth_RowPosition = currentGame.Player.NorthSouth_RowPosition;
            savedGame.PlayersWestEast_ColumnPosition = currentGame.Player.WestEast_ColumnPosition;

            // Serialization to a JSON file (2 phases):
            // phase 1: converting Memento object to JSON
            string jsonString = JsonConvert.SerializeObject(savedGame);
            // phase 2: writing to a .txt file
            using (StreamWriter myWriter = new StreamWriter(pathToSavegameFile))
            {
                myWriter.Write(jsonString);
            }
            return "# Game was successfully saved! #";
        }


        public static Game_Control_Center LoadGame()
        {
            string jsonString = "";
            // Deserialization from a JSON file (2 phases):
            // phase 1: reading from a .txt file
            try
            {
                using (StreamReader myReader = new StreamReader(pathToSavegameFile))
                {
                    jsonString = myReader.ReadToEnd();
                }
            }
            catch
            {
                return null;
            }
            if (jsonString == "")
                return null;
            else
            {
                Game_Control_Center restoredGame = new Game_Control_Center();

                // phase 2: converting JSON to a Memento object
                Memento savedGame = JsonConvert.DeserializeObject<Memento>(jsonString);

                // restoring the state of our previously saved game from the Memento object
                restoredGame.garden.Content = savedGame.ItemsInGarden;
                restoredGame.kitchen.Content = savedGame.ItemsInKitchen;
                restoredGame.library.Content = savedGame.ItemsInLibrary;
                restoredGame.swimmingPoolArea.Content = savedGame.ItemsInSwimmingPoolArea;
                restoredGame.Player.Inventory = savedGame.ItemsInPlayersInventory;
                restoredGame.Player.Location = savedGame.PlayersLocation;
                restoredGame.Player.NorthSouth_RowPosition = savedGame.PlayersNorthSouth_RowPosition;
                restoredGame.Player.WestEast_ColumnPosition = savedGame.PlayersWestEast_ColumnPosition;
                
                return restoredGame;
            }
        }

    }
}
