using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    [SerializeField] private UIDocument _menu;

    private VisualElement _root;
    private Button _playButton;
    private Button _settingsButton;
    private Button _quitButton;

    private void Awake()
    {
        _root = _menu.rootVisualElement;

        _playButton = _root.Q<Button>("PlayButton");
        _settingsButton = _root.Q<Button>("SettingsButton");
        _quitButton = _root.Q<Button>("QuitButton");
        
        _playButton.RegisterCallback<ClickEvent>(OnPlayButtonClick);
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClick);
        _quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);
    }

    private void OnPlayButtonClick(ClickEvent evt) => SceneManager.LoadSceneAsync(sceneBuildIndex: 1);

    private void OnSettingsButtonClick(ClickEvent evt) => print("Load Settings");
    private void OnQuitButtonClick(ClickEvent evt) => Application.Quit();
}
