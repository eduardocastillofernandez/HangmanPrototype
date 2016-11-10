using UnityEngine;

namespace HangmanPrototype
{
    /// <summary>
    /// Handles the behaviour of a button in the keyboard
    /// </summary>
    public class LetterButtonHandler : MonoBehaviour
    {
        /// <summary>
        /// Deactivates the button of the pressed letter and 
        /// informs the Scene Event Handler to take further actions
        /// </summary>
        /// <param name="letter">Letter pressed by the player</param>
        public void OnButtonPressed(string letter)
        {
            GameSceneEventHandler.Instance.OnLetterPressed(letter);
            gameObject.SetActive(false);
        }
    }
}
