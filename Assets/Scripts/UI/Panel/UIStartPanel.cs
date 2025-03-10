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
    
    public void ReStartGame()
    {
        uiManager.StartGame();

    }

    void OnGameStartButtonClicked()
    {
        GameStartButton.interactable = false; 
        uiManager.StartGame();
        SoundManager.Instance.ChangeBackGroundMusic(1);

    }
    
    public void OnSettingButtonClicked()
    {
        uiManager.OnClickedSettingButton();
        SoundManager.Instance.PlayClip(22);
    }
}
