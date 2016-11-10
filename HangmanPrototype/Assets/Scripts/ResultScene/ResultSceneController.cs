using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace HangmanPrototype
{
    /// <summary>
    /// Updates the values of the ResultScene based on the information in Session 
    /// and Controls what happens in the ResultScene.
    /// </summary>
    public class ResultSceneController : BasicSceneController
    {
        #region Variables
        public Text wordText;
        public Text FeedbackText;
        #endregion

        #region Methods
        void Start()
        {
            //Initializing values
            wordText.text = Session.Instance.CurrentWord;
            if (!Session.Instance.Victory)
            {
                FeedbackText.text = "Try Again";
            }
        }
        #endregion
    }
}

