using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;

namespace HangmanPrototype
{
    /// <summary>
    /// Determines how to react to the events on the GameScene
    /// </summary>
    public class GameSceneEventHandler : MonoBehaviour
    {
        #region Variables
        public Text wordText;
        public Text directionsText;
        public Sprite[] hangmanSequence;
        public Image hangmanImage;
        public GameObject screenBlocker;
        GameSceneController _sceneController;
        
        #endregion

        #region Properties
        public static GameSceneEventHandler Instance { get; private set; }
        #endregion

        // Use this for initialization
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _sceneController = GameSceneController.Instance;
            }
        }

        void Start()
        {
            //request a new word
            _sceneController.Reset();
            SetUpHiddenWord(_sceneController.GetNewWord());
            //setup the spaces for the new word
        }

        /// <summary>
        /// Decides whether to update the hidden word because it was a valid letter or to update the hangman image because the letter was wrong.
        /// </summary>
        /// <param name="letter">Letter that was selected by the player</param>
        public void OnLetterPressed(string letter)
        {

            directionsText.gameObject.SetActive(false);
            List<int> letterIndexes;
            if (_sceneController.TryGetLetterIndexes(letter, out letterIndexes))
            {
                //if the letter was in the word
                UpdateHiddenWord(letter, letterIndexes);
            }
            else
            {
                //increase the number of failures
                TryUpdateHangmanImage();
            }
        }


        void UpdateHiddenWord(string letter, List<int> indexesInWord)
        {
            StringBuilder updatedWord = new StringBuilder( wordText.text);
            for (int i = 0; i < indexesInWord.Count; i++)
            {
                updatedWord.Replace(Constants.LETTER_PLACEHOLDER, letter, indexesInWord[i]*2, 1);// = "f";
            }

            wordText.text = updatedWord.ToString();

            if (!wordText.text.Contains(Constants.LETTER_PLACEHOLDER))
            {
                OnGameWon();
            }
        }

        void TryUpdateHangmanImage()
        {
            if (_sceneController.FailuresCount < GameSceneController.MAX_NUMBER_OF_FAILS)
            {
                hangmanImage.sprite = hangmanSequence[_sceneController.FailuresCount - 1];
            }
            else
            {
                hangmanImage.sprite = hangmanSequence[_sceneController.FailuresCount - 1];
                screenBlocker.SetActive(true);
                Invoke("OnGameFailed", 1f);
            }
        }

        void OnGameFailed()
        {
            _sceneController.EndGame(false);
        }

        void OnGameWon()
        {
            _sceneController.EndGame(true);
        }

        /// <summary>
        /// Based in a given word, it will update the text in the screen.
        /// </summary>
        /// <param name="word"></param>
        void SetUpHiddenWord(string word)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < word.Length; i++)
            {
                if (i == 0)
                {
                    stringBuilder.Append(Constants.LETTER_PLACEHOLDER);
                }
                else
                {
                    stringBuilder.Append(Constants.SPACED_LETTER_PLACEHOLDER);
                }
            }

            wordText.text = stringBuilder.ToString();
        }
    }
}

