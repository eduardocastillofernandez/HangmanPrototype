using UnityEngine;

namespace HangmanPrototype
{
    /// <summary>
    /// Contains the information that will be passed from one scene to the other
    /// </summary>
    public class Session : MonoBehaviour
    {
        #region Properties
        /// <summary>
        /// Only instance of the class Session
        /// </summary>
        public static Session Instance { get; private set; }

        /// <summary>
        /// Word that the player has to guess
        /// </summary>
        public string CurrentWord{ get; set; }

        /// <summary>
        /// Indicates whether the player guessed the word or not
        /// </summary>
        public bool Victory { get; set; }
        #endregion

        #region Methods
        void Awake()
        {
            if (Instance == null)//Initializing the instance of this class if it is null.
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);  //destroy the object if another Session existed already
            }
        }
        #endregion
    }
}

