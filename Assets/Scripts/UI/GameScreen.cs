using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class GameScreen : UIScreen
{
    [SerializeField] private PlayerBase _playerBase;
    [SerializeField] private EnemyBase _enemyBase;
    [SerializeField] private WinLoseScreen _winLoseScreen;
    [SerializeField] private LevelSettingsScreen _settingsScreen;
    
    private Label _moneyLabel;
    private Label _waveLabel;
    private Label _enemiesCountLabel;
    private Label _pauseAfterWaveLabel;

    public Label MoneyLabel => _moneyLabel;

    public event UnityAction<int> OnButtonClick;
    public event UnityAction<string> GameEnded;

    private void Awake()
    {
        Initialize();
    }

    public override void Initialize()
    {
        _menuUIDocument.visualTreeAsset = _menuVisualTreeAsset; 
        _root = _menuUIDocument.rootVisualElement;

        _moneyLabel = _root.Q<Label>("MoneyLabel");
        _waveLabel = _root.Q<Label>("WaveLabel");
        _enemiesCountLabel = _root.Q<Label>("EnemiesCountLabel");
        _pauseAfterWaveLabel = _root.Q<Label>("PauseAfterWaveLabel");
        
        var buttons = _root.Query<Button>().Class("tower-button").Build();

        buttons.ForEach(button =>
        {
            button.RegisterCallback<ClickEvent>(OnTowerButtonClick);
        });
        
        var settingsButton = _root.Q<Button>("SettingsButton");

        settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClick);
    }

    private void OnTowerButtonClick(ClickEvent evt)
    {
        PlayButtonSfx();
        
        var button = evt.target as Button;
        if (button == null) return;

        var name = button.name;
        if (name.StartsWith("TowerButton-"))
        {
            var indexStr = name.Replace("TowerButton-", "");
            if (int.TryParse(indexStr, out var index))
            {
                OnButtonClick?.Invoke(index - 1);
            }
        }
    }

    private void PlayButtonSfx()
    {
        GameAudio.Instance.PlaySfx(GameAudio.Instance.GameAudioData.ClickSfx);
    }

    private void OnSettingsButtonClick(ClickEvent evt)
    {
        _settingsScreen.ShowMenu();
        _settingsScreen.SettingsInitialize();
    }

    private void OnEnable()
    {
        _enemyBase.WaveActivated += OnWaveActivated;
        _enemyBase.EnemyLeft += OnEnemyLeft;
        _enemyBase.PauseAfterWave += OnPauseAfterWave;
        _enemyBase.WavesEnded += OnGameEnded;
        _playerBase.Destroyed += OnGameEnded;
    }

    private void OnDisable()
    {
        _enemyBase.WaveActivated -= OnWaveActivated;
        _enemyBase.EnemyLeft -= OnEnemyLeft;
        _enemyBase.PauseAfterWave -= OnPauseAfterWave;
        _enemyBase.WavesEnded -= OnGameEnded;
        _playerBase.Destroyed -= OnGameEnded;
    }

    private void OnWaveActivated(int waveIndex, int waveCount)
    {
        _waveLabel.text = $"Wave: {waveIndex + 1}/{waveCount}";
    }

    private void OnEnemyLeft(int enemiesLeft)
    {
        _enemiesCountLabel.text = $"Enemies: {enemiesLeft:F0}";
    }

    private void OnPauseAfterWave(float pauseDuration, bool pause)
    {
        _pauseAfterWaveLabel.visible = pause;
        _pauseAfterWaveLabel.text = $"Pause: {pauseDuration:F0}";
    }

    private void OnGameEnded(string winningText)
    {
        _winLoseScreen.ShowMenu();
    }
}
