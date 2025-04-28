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
        GameAudio.Instance.PlayMusic(GameAudio.Instance.GameAudioData.Music);
    }

    private void ShowMainMenu()
    {
        _menuUIDocument.visualTreeAsset = _mainMenu;
        _root = _menuUIDocument.rootVisualElement;

        var playButton = _root.Q<Button>("PlayButton");
        var settingsButton = _root.Q<Button>("SettingsButton");
        var quitButton = _root.Q<Button>("QuitButton");
        
        playButton.RegisterCallback<ClickEvent>(evt =>
        {
            GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
            SceneManager.LoadSceneAsync(1);
        });
        settingsButton.RegisterCallback<ClickEvent>(evt =>
        {
            GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
            ShowSettingsMenu();
        });
        quitButton.RegisterCallback<ClickEvent>(evt =>
        {
            GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
            Application.Quit();
        });
    }

    private void ShowSettingsMenu()
    {
        _menuUIDocument.visualTreeAsset = _settingsMenu;
        _root = _menuUIDocument.rootVisualElement;
        
        var musicToggle = _root.Q<Toggle>("MusicToggle");
        var volumeSlider = _root.Q<Slider>("VolumeSlider");
        var backButton = _root.Q<Button>("BackButton");
        
        musicToggle.value = GameAudio.Instance.GetAudioToggle();
        volumeSlider.value = GameAudio.Instance.GetAudioVolume();
        
        musicToggle.RegisterValueChangedCallback(evt =>
        {
            GameAudio.Instance.AudioToggle(evt.newValue);
        });
        volumeSlider.RegisterValueChangedCallback(evt =>
        {
            GameAudio.Instance.AudioVolumeChanged(evt.newValue);
        });
        backButton.RegisterCallback<ClickEvent>(evt =>
        {
            GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
            ShowMainMenu();
        });
    }
}
