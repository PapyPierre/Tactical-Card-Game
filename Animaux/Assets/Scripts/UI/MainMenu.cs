using UnityEngine;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private GameManager _gameManager;
        [SerializeField] private GameObject mainMenu;

        private void Start()
        {
            _gameManager = GameManager.instance;
        }

        // Called from UI
        public void Play1v1()
        {
            mainMenu.SetActive(false);
            _gameManager.StartGame();
        }

        // Called from UI
        public void Quit()
        {
            Application.Quit();
        }
    }
}
