using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartPanel : UIPanel
{
    public Button GameStartButton;
    public Button SettingButton;

    public BackGroundScroller backgroundScroller;



    public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);
        GameStartButton.onClick.AddListener(OnGameStartButtonClicked);
        SettingButton.onClick.AddListener(OnSettingButtonClicked);
    }
    
    public void ReStartGame()
    {
        uiManager.StartGame();
    }

    public void OnGameStartButtonClicked()
    {
        GameStartButton.interactable = false; 
        uiManager.StartGame();
        backgroundScroller.isGameStarted = true;

    }
    
    public void OnSettingButtonClicked()
    {
        uiManager.OnClickedSettingButton();
    }
}
