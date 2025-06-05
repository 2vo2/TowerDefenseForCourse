using UnityEngine;
using UnityEngine.UIElements;

public class SettingsScreen : UIScreen
{
    [SerializeField] protected UIScreen _mainMenuScreen;
    
    private Toggle _musicToggle;
    private Toggle _sfxToggle;
    private Slider _musicSlider;
    private Slider _sfxSlider;
    private Button _backButton;

    public override void Initialize()
    {
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
            _mainMenuScreen.ShowMenu();
        });
    }
}