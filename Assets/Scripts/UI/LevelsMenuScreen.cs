using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    public class LevelsMenuScreen : UIScreen
    {
        [SerializeField] private MainMenuScreen _mainMenuScreen;
        
        private Button _backButton;

        public override void Initialize()
        {
            _backButton = _root.Q<Button>("BackButton");
            
            var levelButtons = _root.Query<Button>().Class("levels-button").Build();

            levelButtons.ForEach(button =>
            {
                button.RegisterCallback<ClickEvent>(OnLevelButtonClick);
            });
            
            _backButton.RegisterCallback<ClickEvent>(evt =>
            {
                GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
                _mainMenuScreen.ShowMenu();
            });
        }

        private void OnLevelButtonClick(ClickEvent evt)
        {
            var button = evt.target as Button;
            if (button == null) return;
            
            SceneManager.LoadScene(int.Parse(button.text));
        }
    }
}