using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartPanel : UIPanel
{
    public Button GameStartButton;
    public Button SettingButton;
    public Button RankingButton;
    public Button SkinButton;
    public Button EncyButton;
    public Button AchieveButton;
    public Button AdsButton;
    public Button ModeButton;
    public Button EventButton;

    public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);
        GameStartButton.onClick.AddListener(OnGameStartButtonClicked);
        SettingButton.onClick.AddListener(OnSettingButtonClicked);
        //RankingButton.onClick.AddListener(OnClickedRankingButton);
        SkinButton.onClick.AddListener(OnClickedSkinButton);
        EncyButton.onClick.AddListener(OnClickedEncyButton);
        AchieveButton.onClick.AddListener(OnClickedAchieveButton);
        AdsButton.onClick.AddListener(OnClickedAdsButton);
        ModeButton.onClick.AddListener(OnClickedModeButton);
        EventButton.onClick.AddListener(OnClickedEventButton);
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

    public void OnClickedRankingButton()
    {
        uiManager.OnClickedRankingButton();
    }

    public void OnClickedSkinButton()
    {
        //uiManager.StartGame();
    }
    public void OnClickedEncyButton()
    {
        uiManager.OnClickedSkinButton();
    }

    public void OnClickedAchieveButton()
    {
        uiManager.OnClickedAchieveButton();
    }

    public void OnClickedAdsButton()
    {
        uiManager.OnClickedAdsButton();
    }
    public void OnClickedModeButton()
    {
        uiManager.OnClickedModeButton();
    }
    public void OnClickedEventButton()
    {
        uiManager.OnClickedEventButton();
    }

}
