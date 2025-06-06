using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuScreen : UIScreen
{
    [SerializeField] private SettingsScreen _settingsScreen;
    [SerializeField] private LevelsMenuScreen _levelsMenuScreen;
    
    private Button _playButton;
    private Button _settingsButton;
    private Button _quitButton;

    private void Awake()
    {
        ShowMenu();
        Initialize();
        
        GameAudio.Instance.PlayMusic(GameAudio.Instance.GameAudioData.Music);
        GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
    }

    public override void Initialize()
    {
        _playButton = _root.Q<Button>("PlayButton");
        _settingsButton = _root.Q<Button>("SettingsButton");
        _quitButton = _root.Q<Button>("QuitButton");
        
        _playButton.RegisterCallback<ClickEvent>(evt =>
        {
            GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
            _levelsMenuScreen.ShowMenu();
        });
        _settingsButton.RegisterCallback<ClickEvent>(evt =>
        {
            GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
            _settingsScreen.ShowMenu();
        });
        _quitButton.RegisterCallback<ClickEvent>(evt =>
        {
            GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
            Application.Quit();
        });
    }
}
