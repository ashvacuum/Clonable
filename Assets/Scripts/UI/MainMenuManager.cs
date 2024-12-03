using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Button startGameButton;
        [SerializeField] UnityEngine.UI.Button howToPlayButton;

        private void OnEnable()
        {
            startGameButton.onClick.AddListener(OnStartGameButtonPressed);
            howToPlayButton.onClick.AddListener(OnHowToPlayButtonPressed);
        }

        private void OnDisable()
        {
            startGameButton.onClick.RemoveListener(OnStartGameButtonPressed);
            howToPlayButton.onClick.RemoveListener(OnHowToPlayButtonPressed);
        }

        private void OnStartGameButtonPressed()
        {
            SceneChanger.instance.LoadGameScene();
        }

        private void OnHowToPlayButtonPressed()
        {
            SceneChanger.instance.LoadHowToPlayScene();
        }
    
    }
}
