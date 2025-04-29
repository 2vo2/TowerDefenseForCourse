using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIDocument _menuUIDocument;
    [SerializeField] private VisualTreeAsset _mainMenu;
    [SerializeField] private VisualTreeAsset _settingsMenu;

    private VisualElement _root;
    private Button _playButton;
    private Button _settingsButton;
    private Button _quitButton;
    private Toggle _musicToggle;
    private Toggle _sfxToggle;
    private Slider _musicSlider;
    private Slider _sfxSlider;
    private Button _backButton;

    private void Awake()
    {
        ShowMainMenu();
        
        GameAudio.Instance.PlayMusic(GameAudio.Instance.GameAudioData.Music);
        GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
    }

    private void ShowMainMenu()
    {
        _menuUIDocument.visualTreeAsset = _mainMenu;
        _root = _menuUIDocument.rootVisualElement;

        _playButton = _root.Q<Button>("PlayButton");
        _settingsButton = _root.Q<Button>("SettingsButton");
        _quitButton = _root.Q<Button>("QuitButton");
        
        _playButton.RegisterCallback<ClickEvent>(evt =>
        {
            GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
            SceneManager.LoadSceneAsync(1);
        });
        _settingsButton.RegisterCallback<ClickEvent>(evt =>
        {
            GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
            ShowSettingsMenu();
        });
        _quitButton.RegisterCallback<ClickEvent>(evt =>
        {
            GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
            Application.Quit();
        });
    }

    private void ShowSettingsMenu()
    {
        _menuUIDocument.visualTreeAsset = _settingsMenu;
        _root = _menuUIDocument.rootVisualElement;
        
        _musicToggle = _root.Q<Toggle>("MusicToggle");
        _sfxToggle = _root.Q<Toggle>("SfxToggle");
        _musicSlider = _root.Q<Slider>("VolumeSlider");
        _sfxSlider = _root.Q<Slider>("SfxSlider");
        _backButton = _root.Q<Button>("BackButton");
        
        _musicToggle.value = GameAudio.Instance.GetAudioToggle("MusicToggle");
        _sfxToggle.value = GameAudio.Instance.GetAudioToggle("SfxToggle");
        
        _musicSlider.value = GameAudio.Instance.GetAudioVolume("MusicVolume");
        _sfxSlider.value = GameAudio.Instance.GetAudioVolume("SfxVolume");
        
        _musicToggle.RegisterValueChangedCallback(evt =>
        {
            GameAudio.Instance.SetMusicToggle(evt.newValue);
        });
        _sfxToggle.RegisterValueChangedCallback(evt =>
        {
            GameAudio.Instance.SetSfxToggle(evt.newValue);
        });
        _musicSlider.RegisterValueChangedCallback(evt =>
        {
            GameAudio.Instance.SetMusicVolume(evt.newValue);
        });
        _sfxSlider.RegisterValueChangedCallback(evt =>
        {
            GameAudio.Instance.SetSfxVolume(evt.newValue);
            
            GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
        });
        _backButton.RegisterCallback<ClickEvent>(evt =>
        {
            GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
            ShowMainMenu();
        });
    }
}
