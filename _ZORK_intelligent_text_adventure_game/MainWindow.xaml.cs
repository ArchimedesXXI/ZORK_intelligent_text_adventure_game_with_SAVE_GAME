using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Escape_Game_engine_library;
using Text_games_input_parser_library_v2;

namespace _ZORK_intelligent_text_adventure_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new_game.FillLocationAndCharacterWithInitialItemsOnGameStartup();
            this.currentMessageToBeDisplayed = new_game.Player.Location.Discription;
            UpdateGUIview();
        }


        // Instantiating a new game instance 
        // (an instance of Game_Control_Center class in the Escape_Game_engine_library)
        private Game_Control_Center new_game = new Game_Control_Center();

        // Instantiating a new input parser for parsing player input
        private input_parser new_input_parser = new input_parser();

        // A variable (field) holding the current game message to be displayed to the player.
        // The content of this field is updated by event-handlers methods.
        private string currentMessageToBeDisplayed;


        #region Update GUI view
        // update the appearance of the Graphical User Interface (GUI) 
        public void UpdateGUIview()
        {
            this.current_location_message.Text = new_game.Player.Location.Name;
            this.location_content_display.Text = new_game.Player.Location.GetLocationContentAsString();
            this.characters_inventory_display.Text = new_game.Player.GetCharactersInventoryAsString();
            this.user_input_box.Text = "";
            this.game_message.Text = this.currentMessageToBeDisplayed;
        }
        #endregion

      
        #region  Event handler for when the player hits "enter"
        private void user_input_box_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Return)
                return;
            else if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                e.Handled = true;
                string outputMessage = this.new_input_parser.ParseTextInput(new_game, this.user_input_box.Text);
                if (outputMessage != "")
                    this.currentMessageToBeDisplayed = outputMessage;
                else
                    this.currentMessageToBeDisplayed = "Sorry, what did you mean to do?";
                this.UpdateGUIview();
            }
        }
        #endregion

        #region Event handlers for SaveGame, LoadGame, RestartGame buttons' clicks
        private void OnRestartGameButton_Click(object sender, RoutedEventArgs e)
        {
            this.new_game = new Game_Control_Center();
            new_game.FillLocationAndCharacterWithInitialItemsOnGameStartup();
            this.currentMessageToBeDisplayed = "# Game was restarted! #";
            this.UpdateGUIview();
        }

        private void OnSaveGameButton_Click(object sender, RoutedEventArgs e)
        {
            this.currentMessageToBeDisplayed = Memento_caretaker.SaveGame(this.new_game);
            this.UpdateGUIview();
        }

        private void OnLoadGameButton_Click(object sender, RoutedEventArgs e)
        {
            Game_Control_Center restoredGame = Memento_caretaker.LoadGame();
            if (restoredGame == null)
                this.currentMessageToBeDisplayed = "# Sorry, cannot load game! #";
            else
            {
                this.new_game = restoredGame;
                this.currentMessageToBeDisplayed = "# Game loaded successfully! #";
            }
            this.UpdateGUIview();
        }
        #endregion
    }
}
