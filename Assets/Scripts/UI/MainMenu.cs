using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIDocument _menuUIDocument;
    [SerializeField] private VisualTreeAsset _mainMenu;
    [SerializeField] private VisualTreeAsset _settingsMenu;

    private VisualElement _root;

    private void Awake()
    {
        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        _menuUIDocument.visualTreeAsset = _mainMenu;
        _root = _menuUIDocument.rootVisualElement;

        var playButton = _root.Q<Button>("PlayButton");
        var settingsButton = _root.Q<Button>("SettingsButton");
        var quitButton = _root.Q<Button>("QuitButton");
        
        playButton.RegisterCallback<ClickEvent>(evt => SceneManager.LoadSceneAsync(1));
        settingsButton.RegisterCallback<ClickEvent>(evt => ShowSettingsMenu());
        quitButton.RegisterCallback<ClickEvent>(evt => Application.Quit());
    }

    private void ShowSettingsMenu()
    {
        _menuUIDocument.visualTreeAsset = _settingsMenu;
        _root = _menuUIDocument.rootVisualElement;
        
        var musicToggle = _root.Q<Toggle>("MusicToggle");
        var volumeSlider = _root.Q<Slider>("VolumeSlider");
        var backButton = _root.Q<Button>("BackButton");
        
        musicToggle.RegisterValueChangedCallback(evt => Debug.Log("Toggle: " + evt.newValue));
        volumeSlider.RegisterValueChangedCallback(evt => Debug.Log("Volume: " + evt.newValue));
        backButton.RegisterCallback<ClickEvent>(evt => ShowMainMenu());
    }
}
