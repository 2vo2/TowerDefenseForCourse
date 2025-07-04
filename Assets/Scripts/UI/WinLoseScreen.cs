﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class WinLoseScreen : UIScreen
{
    [SerializeField] private GameScreen _gameScreen;
    
    private Label _winningLabel;
    private Button _menuButton;
    private Button _playButton;
    private Button _restartButton;

    public void ShowWinLoseScreen(string winningText)
    {
        ShowMenu();
        
        _winningLabel.text = $"YOU {winningText}";
        
        if (_winningLabel.text == "YOU WIN!")
        {
            _playButton.RegisterCallback<ClickEvent>(evt =>
            {
                LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            });
        }
        else
        {
            _playButton.enabledSelf = false;
        }
    }

    private void LoadScene(int sceneNumber)
    {
        SceneManager.LoadSceneAsync(sceneNumber);
    }
    
    public override void Initialize()
    {
        _winningLabel = _root.Q<Label>("WinningLabel");
        _menuButton = _root.Q<Button>("MenuButton");
        _playButton = _root.Q<Button>("PlayButton");
        _restartButton = _root.Q<Button>("RestartButton");

        _menuButton.RegisterCallback<ClickEvent>(evt => LoadScene(0));
        _restartButton.RegisterCallback<ClickEvent>(evt => LoadScene(1));
    }
}
