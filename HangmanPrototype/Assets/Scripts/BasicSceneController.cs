using UnityEngine;

namespace HangmanPrototype
{
    /// <summary>
    /// Contains the logic of a simple Scene that contains only one button
    /// </summary>
    public class BasicSceneController : MonoBehaviour
    {
        /// <summary>
        /// Determines what to do when the player presses the play button
        /// </summary>
        public virtual void Play()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
    }
}

