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
    private Toggle _audioToggle;
    private Slider _musicSlider;
    private Slider _sfxSlider;
    private Button _backButton;

    private void Awake()
    {
        ShowMainMenu();
        
        GameAudio.Instance.PlayMusic(GameAudio.Instance.GameAudioData.Music);
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
        
        _audioToggle = _root.Q<Toggle>("MusicToggle");
        _musicSlider = _root.Q<Slider>("VolumeSlider");
        _sfxSlider = _root.Q<Slider>("SfxSlider");
        _backButton = _root.Q<Button>("BackButton");
        
        _audioToggle.value = GameAudio.Instance.GetAudioToggle();
        
        _musicSlider.value = GameAudio.Instance.GetAudioVolume("MusicVolume");
        _sfxSlider.value = GameAudio.Instance.GetAudioVolume("SfxVolume");
        
        _audioToggle.RegisterValueChangedCallback(evt =>
        {
            GameAudio.Instance.AudioToggle("MusicToggle", evt.newValue);
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
