using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_games_input_parser_library_v2
{
    public class input_parser
    {

        /// <summary>
        /// All characters on which we want to seperate are substrings from input.
        /// - and ' are not included, because they might be part of legitimate words.
        /// </summary>
        private string stringWithCharactersOnWhichToParse = @"!@#$%^&*()_+=[]\{}|;:"",./<>?`~ ";

        /// <summary>
        /// List of all words (in lowercase letters!) that we want to ignore while parsing our user input.
        /// </summary>
        private List<string> wordsToBeIgnored = new List<string>() { "i", "a", "an", "the", "to", "and"};


        #region turning string input into a list of words, that the input is composed of
        /// <summary>
        /// Returns a list of string, where each element is a word from the input. A word == a substring seperated from another substring with an empty space " " 
        /// as well as with one of those characters: !@#$%^&*()_+=[]\{}|;:"",./<>?`~
        /// The substrings containing - or ' are not seperated, because they might be part of a legitimate word.
        /// Empty "words" are ignored.
        /// This method also turnes every uppercase letter to lowercase letter.
        /// This method is based on the implementation of .Split() instance method on strings.
        /// </summary>
        internal List<string> string_to_listOfstring(string input)
        {
            List<string> result = new List<string>(){};
            char[] ignoreChars = this.getCharArrayFromAString(this.stringWithCharactersOnWhichToParse);
            result = input.ToLower().Split(ignoreChars, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            return result;
        }

        /// <summary>
        /// Returns a list of string, where each element is a word from the input. A word == a substring of input seperated from another substring with an empty space " " 
        /// as well as with one of those characters: !@#$%^&*()_+=[]\{}|;:"",./<>?`~
        /// The substrings containing - or ' are not seperated, because they might be part of a legitimate word.
        /// This method also turnes every uppercase letter to lowercase letter.
        /// Empty "words" are ignored.
        /// This method is based on MY OWN implementation.
        /// </summary>
        internal List<string> string_to_listOfstring2(string input)
        {
            input = input.ToLower();
            List<string> resultingList = new List<string>() { };
            char[] ignoreChars = this.getCharArrayFromAString(this.stringWithCharactersOnWhichToParse);
            int startingPosition = 0;
            string currentWord = "";
            for (int i = 0; i < input.Length + 1; i++)
            {
                if (i == input.Length || ignoreChars.Contains(input[i]))
                {
                    if (i == 0 || ignoreChars.Contains(input[i - 1]))
                    {
                        startingPosition = i + 1;
                    }
                    else
                    {
                        currentWord = input.Substring(startingPosition, i - startingPosition);
                        resultingList.Add(currentWord);
                        startingPosition = i + 1;
                    }
                }
            }
            return resultingList;
        }
        #endregion

        #region helpful tools
        private char[] getCharArrayFromAString(string singleString)
        {
            char[] result = new char[singleString.Length];
            int i = 0;
            foreach (char ch in singleString)
            {
                result[i] = ch;
                i++;
            }
            return result;
        }
        #endregion

        #region prepare the list of string to be parsed, remove unuseful words
        /// <summary>
        /// Returns a list of string, which is created by removing all the words lited in the filed: wordsToBeIgnored.
        /// </summary>
        internal List<string> RemoveUnusefulWords(List<string> allWords)
        {
            List<string> onlyUsefulWords = allWords;
            foreach (string word in wordsToBeIgnored)
            {
                while (onlyUsefulWords.Contains(word))
                    onlyUsefulWords.Remove(word);
            }
            return onlyUsefulWords;
        }
        #endregion



        public string ParseTextInput(ITextGameAction actualGameInstance, string inputAsString)
        {
            if (inputAsString == null)
                throw new ArgumentNullException();
            if (actualGameInstance == null)
                throw new ArgumentNullException();

            PhraseToBeParsed phrase = new PhraseToBeParsed(inputAsString);
            string entireMessage = "";
            string messageOnTake = actualGameInstance.TakeItems(phrase.ItemsCorrespondingToKeyWords["take"]);
            string messageOnDrop = actualGameInstance.DropItems(phrase.ItemsCorrespondingToKeyWords["drop"]);
            string messageOnExamine = actualGameInstance.ExamineItems(phrase.ItemsCorrespondingToKeyWords["examine"]);
            // different types of operating ONE ITEM:
            string messageOnOperate = actualGameInstance.OperateItems("operate", phrase.ItemsCorrespondingToKeyWords["operate"]);
            string messageOnMove = actualGameInstance.OperateItems("move", phrase.ItemsCorrespondingToKeyWords["move"]);
            string messageOnRead = actualGameInstance.OperateItems("read", phrase.ItemsCorrespondingToKeyWords["read"]);
            string messageOnOpen = actualGameInstance.OperateItems("open", phrase.ItemsCorrespondingToKeyWords["open"]);
            string messageOnClose = actualGameInstance.OperateItems("close", phrase.ItemsCorrespondingToKeyWords["close"]);
            // ...
            string messageOnUse = actualGameInstance.UseItems(phrase.ItemsCorrespondingToKeyWords["use"]);
            // The actions: GoTo and Look Around have to be called last, because GoTo changes the location with which to interact(!)
            // and look around is likely used after someone changed location. 
            string messageOnGoTo = actualGameInstance.GoTo(phrase.ItemsCorrespondingToKeyWords["go"]);
            if (messageOnTake != "")
                entireMessage = entireMessage + "\n" + messageOnTake;
            if (messageOnDrop != "")
                entireMessage = entireMessage + "\n" + messageOnDrop;
            if (messageOnExamine != "")
                entireMessage = entireMessage + "\n" + messageOnExamine;
            if (messageOnOperate != "")
                entireMessage = entireMessage + "\n" + messageOnOperate;
            if (messageOnMove != "")
                entireMessage = entireMessage + "\n" + messageOnMove;
            if (messageOnRead != "")
                entireMessage = entireMessage + "\n" + messageOnRead;
            if (messageOnOpen != "")
                entireMessage = entireMessage + "\n" + messageOnOpen;
            if (messageOnClose != "")
                entireMessage = entireMessage + "\n" + messageOnClose;
            if (messageOnUse != "")
                entireMessage = entireMessage + "\n" + messageOnUse;
            // messageOnGoTo and then messageOnLookAround have to be appended last!!
            if (messageOnGoTo != "")
                entireMessage = entireMessage + "\n\n" + messageOnGoTo;
            if (phrase.LookAroundHasBeenCalled)
            {
                string messageOnLookAround = actualGameInstance.LookAround();
                entireMessage = entireMessage + "\n\n" + messageOnLookAround;
            }
            return entireMessage;
        }
    }
}
