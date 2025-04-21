using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameInterface : MonoBehaviour
{
    [SerializeField] private UIDocument _gameInterface;
    [SerializeField] private EnemyBase _enemyBase;

    private VisualElement _root;
    private Label _moneyLabel;
    private Label _waveLabel;

    public Label MoneyLabel => _moneyLabel;

    public event UnityAction OnButtonClick;

    private void Awake()
    {
        _root = _gameInterface.rootVisualElement;

        _moneyLabel = _root.Q<Label>("MoneyLabel");
        _waveLabel = _root.Q<Label>("WaveLabel");
        var buttons = _root.Query<Button>().Class("unity-button").Build();

        buttons.ForEach(button =>
        {
            button.RegisterCallback<ClickEvent>(OnTowerButtonClick);
        });
    }

    private void OnEnable()
    {
        _enemyBase.WaveActivated += OnWaveActivated;
    }

    private void OnDisable()
    {
        _enemyBase.WaveActivated -= OnWaveActivated;
    }

    private void OnWaveActivated(int waveIndex, int waveCount)
    {
        _waveLabel.text = $"Wave: {waveIndex + 1}/{waveCount}";
    }
    
    private void OnTowerButtonClick(ClickEvent evt)
    {
        OnButtonClick?.Invoke();
    }
}
