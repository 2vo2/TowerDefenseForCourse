using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class WinLoseScreen : MonoBehaviour
{
    [SerializeField] private UIDocument _gameUIDocument;
    [SerializeField] private GameInterface _gameInterface;
    [SerializeField] private VisualTreeAsset _winLoseScreen;

    private VisualElement _root;
    private Label _winningLabel;
    private Button _menuButton;
    private Button _playButton;
    private Button _restartButton;
    
    private void OnEnable()
    {
        _gameInterface.GameEnded += ShowWinLoseScreen;
    }

    private void OnDisable()
    {
        _gameInterface.GameEnded -= ShowWinLoseScreen;
    }

    private void ShowWinLoseScreen(string winningText)
    {
        _gameUIDocument.visualTreeAsset = _winLoseScreen; 
        _root = _gameUIDocument.rootVisualElement;
        
        _winningLabel = _root.Q<Label>("WinningLabel");
        _menuButton = _root.Q<Button>("MenuButton");
        _playButton = _root.Q<Button>("PlayButton");
        _restartButton = _root.Q<Button>("RestartButton");
        
        _winningLabel.text = $"YOU {winningText}";
        
        _menuButton.RegisterCallback<ClickEvent>(evt => LoadScene(0));
        _playButton.RegisterCallback<ClickEvent>(evt => LoadScene(0));
        _restartButton.RegisterCallback<ClickEvent>(evt => LoadScene(1));
    }

    private void LoadScene(int sceneNumber)
    {
        SceneManager.LoadSceneAsync(sceneNumber);
    }
}
