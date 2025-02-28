using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingPanel : UIPanel
{
    public Button SettingInstagramButton;
    public Button SettingCreditButton;
    public Button SettingSoundEffectButton;
    public Button SettingBGMButton;


    public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);
        SettingBGMButton.onClick.AddListener(OnClickedSettingBGMButton);
        SettingSoundEffectButton.onClick.AddListener(OnClikedSettingSoundEffectButton);
        SettingInstagramButton.onClick.AddListener(OnClikedSettingInstagramButton);
        SettingCreditButton.onClick.AddListener(OnClickedSettingCreditButton);
    }

    void OnClickedSettingBGMButton()
    {
        uiManager.OnClickedSettingBGMButton();
    }
    void OnClikedSettingSoundEffectButton()
    {
        uiManager.OnClickedSettingSoundEffectButton();
    }
    void OnClikedSettingInstagramButton()
    { 
        uiManager.OnClickedSettingInstaGramButton();
    }
    void OnClickedSettingCreditButton()
    {
        uiManager.OnClickedSettingCreditButton();
    } 

}
