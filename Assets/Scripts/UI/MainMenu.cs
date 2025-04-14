using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using VContainer;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIDocument _menuUIDocument;
    [SerializeField] private VisualTreeAsset _mainMenu;
    [SerializeField] private VisualTreeAsset _settingsMenu;

    private VisualElement _root;

    private AudioSystem _audioSystem;

    [Inject]
    private void Construct(AudioSystem audioSystem)
    {
        _audioSystem = audioSystem;
        print("Register AudioSystem");
    }
    
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
        
        playButton.RegisterCallback<ClickEvent>(PlayButtonClick);
        settingsButton.RegisterCallback<ClickEvent>(SettingsButtonClick);
        quitButton.RegisterCallback<ClickEvent>(QuitButtonClick);
    }

    private void PlayButtonClick(ClickEvent evt)
    {
        _audioSystem.ButtonClickSound.Play();
        SceneManager.LoadSceneAsync(1);
    }

    private void SettingsButtonClick(ClickEvent evt)
    {
        _audioSystem.ButtonClickSound.Play();
        ShowSettingsMenu();
    }

    private void QuitButtonClick(ClickEvent evt)
    {
        _audioSystem.ButtonClickSound.Play();
        Application.Quit();
    }

    private void ShowSettingsMenu()
    {
        _menuUIDocument.visualTreeAsset = _settingsMenu;
        _root = _menuUIDocument.rootVisualElement;
        
        var musicToggle = _root.Q<Toggle>("MusicToggle");
        var volumeSlider = _root.Q<Slider>("VolumeSlider");
        var backButton = _root.Q<Button>("BackButton");
        
        musicToggle.RegisterValueChangedCallback(MusicToggleChanged);
        volumeSlider.RegisterValueChangedCallback(VolumeSliderValueChanged);
        backButton.RegisterCallback<ClickEvent>(BackButtonClick);

        musicToggle.value = !_audioSystem.BackgroundMusic.mute;
        volumeSlider.value = _audioSystem.BackgroundMusic.volume;
    }

    private void MusicToggleChanged(ChangeEvent<bool> evt)
    {
        _audioSystem.TurnMusic(evt.newValue);
    }

    private void VolumeSliderValueChanged(ChangeEvent<float> evt)
    {
        _audioSystem.ChangeBackgroundMusicVolume(evt.newValue);
        Debug.Log("Volume: " + evt.newValue);
    }

    private void BackButtonClick(ClickEvent evt)
    {
        _audioSystem.ButtonClickSound.Play();
        ShowMainMenu();
    }
}
