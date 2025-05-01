using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelSettingsScreen : SettingsScreen
{
    private Button _quitButton;
    private Button _restartButton;
    
    public void SettingsInitialize()
    {
        Initialize();
        
        _quitButton = _root.Q<Button>("QuitButton");
        _restartButton = _root.Q<Button>("RestartButton");
        
        _quitButton.RegisterCallback<ClickEvent>(evt =>
        {
            GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
        });
        _restartButton.RegisterCallback<ClickEvent>(evt =>
        {
            GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        });
    }
}