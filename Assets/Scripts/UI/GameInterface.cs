using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameInterface : MonoBehaviour
{
    [SerializeField] private UIDocument _gameUIDocument;
    [SerializeField] private VisualTreeAsset _gameInterface;
    [SerializeField] private PlayerBase _playerBase;
    [SerializeField] private EnemyBase _enemyBase;

    private VisualElement _root;
    private Label _moneyLabel;
    private Label _waveLabel;
    private Label _enemiesCountLabel;
    private Label _pauseAfterWaveLabel;

    public Label MoneyLabel => _moneyLabel;

    public event UnityAction<int> OnButtonClick;
    public event UnityAction<string> GameEnded;

    private void Awake()
    {
        _gameUIDocument.visualTreeAsset = _gameInterface; 
        _root = _gameUIDocument.rootVisualElement;

        _moneyLabel = _root.Q<Label>("MoneyLabel");
        _waveLabel = _root.Q<Label>("WaveLabel");
        _enemiesCountLabel = _root.Q<Label>("EnemiesCountLabel");
        _pauseAfterWaveLabel = _root.Q<Label>("PauseAfterWaveLabel");
        
        var buttons = _root.Query<Button>().Class("unity-button").Build();

        buttons.ForEach(button =>
        {
            button.RegisterCallback<ClickEvent>(OnTowerButtonClick);
        });
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
    
    private void OnTowerButtonClick(ClickEvent evt)
    {
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

    private void OnGameEnded(string winningText)
    {
        GameEnded?.Invoke(winningText);
    }
}
