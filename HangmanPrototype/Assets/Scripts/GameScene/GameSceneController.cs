using UnityEngine;
using System.Collections.Generic;

namespace HangmanPrototype
{
    /// <summary>
    /// Contains the logic of the GameScene
    /// </summary>
    public class GameSceneController 
    {
        #region Variables and Constants
        static GameSceneController _instance; //only instance of this class
        string[] _wordsPool = { "Hangman","Success","Computer","Friendship"}; //this are a group of words that will be used to guess as an example
        public const int MAX_NUMBER_OF_FAILS = 6;
        #endregion

        #region Properties
        /// <summary>
        /// Returns the only instance of this class and initializes it if it is null.
        /// </summary>
        public static GameSceneController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameSceneController();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Current number of times the player has pressed the wrong letter
        /// </summary>
        public int FailuresCount { get; private set; }
        #endregion

        #region Constructors
        GameSceneController() { }
        #endregion

        #region Methods
        /// <summary>
        /// Returns whether a letter is in the current word or not. 
        /// If the letter is a valid letter, it returns all the indexes of that letter in the current word.
        /// </summary>
        /// <param name="letter">Letter that was pressed in the keyboard</param>
        /// <param name="letterIndexes">Indexes in the guessed word where the pressed letter is present</param>
        /// <returns>Returns true if the pressed letter is in the current word. Otherwise, it returns false.</returns>
        public bool TryGetLetterIndexes(string letter, out List<int> letterIndexes)
        {
            bool isValidLetter = false;
            letterIndexes = new List<int>();
            string currentWord = Session.Instance.CurrentWord.ToUpper(); //All letters in the keyboard are upper case, therefore, you need to convert the word to uppercase
            for (int i = 0; i < currentWord.Length; i++) 
            {
                if (currentWord[i].ToString().Equals(letter))
                {
                    letterIndexes.Add(i);
                    isValidLetter = true;
                }
            }

            if (!isValidLetter) //Updating the number of failures because it was the wrong letter
            {
                FailuresCount++;
            }

            return isValidLetter;
        }

        /// <summary>
        /// Returns the word to be guessed
        /// </summary>
        /// <returns>New word to be guessed</returns>
        public string GetNewWord()
        {
            string newWord = _wordsPool[Random.Range(0,_wordsPool.Length-1)];
            while (newWord.Equals(Session.Instance.CurrentWord)) //making sure that the same word is not selected
            {
                newWord = _wordsPool[Random.Range(0, _wordsPool.Length - 1)];
            }
            Session.Instance.CurrentWord = newWord; 
            return Session.Instance.CurrentWord;
        }

        /// <summary>
        /// Sets the victory value in session and jump to the result scene
        /// </summary>
        /// <param name="victory"></param>
        public void EndGame(bool victory)
        {
            Session.Instance.Victory = victory;
            UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
        }

        /// <summary>
        /// Resets the failures count to zero.
        /// </summary>
        public void Reset()
        {
            FailuresCount = 0;
        }
        #endregion
    }
}

