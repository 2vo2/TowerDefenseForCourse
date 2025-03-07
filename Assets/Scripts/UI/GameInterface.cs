using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameInterface : MonoBehaviour
{
    [SerializeField] private UIDocument _gameInterface;

    private VisualElement _root;
    private Label _moneyLabel;

    public Label MoneyLabel => _moneyLabel;

    public event UnityAction OnButtonClick;

    private void Awake()
    {
        _root = _gameInterface.rootVisualElement;

        _moneyLabel = _root.Q<Label>("MoneyLabel");
        var buttons = _root.Query<Button>().Class("unity-button").Build();

        buttons.ForEach(button =>
        {
            button.RegisterCallback<ClickEvent>(OnTowerButtonClick);
        });
    }

    private void OnTowerButtonClick(ClickEvent evt)
    {
        OnButtonClick?.Invoke();
    }
}
