using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartPanel : UIPanel
{
    public Button GameStartButton;
    public Button SettingButton;

    public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);
        GameStartButton.onClick.AddListener(OnGameStartButtonClicked);
        SettingButton.onClick.AddListener(OnSettingButtonClicked);
    }
    
    void OnGameStartButtonClicked()
    {
        GameStartButton.interactable = false; 
        uiManager.StartGame();
    }
    
    public void OnSettingButtonClicked()
    {
        uiManager.OnClickedSettingButton();
    }
}
