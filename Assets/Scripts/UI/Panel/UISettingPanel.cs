using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingPanel : UIPanel
{
    public Button SettingReturnButton;
    public Button SettingInstagramButton;
    public Button SettingCreditButton;
    public Button SettingSoundEffectButton;
    public Button SettingBGMButton;


    public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);
        SettingReturnButton.onClick.AddListener(OnClickedSettingReturnButton);
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
        SoundMnager.Instance.PlayClip(22);
    }
    void OnClickedSettingCreditButton()
    {
        uiManager.OnClickedSettingCreditButton();
        SoundMnager.Instance.PlayClip(22);
    } 
    void OnClickedSettingReturnButton()
    {
        uiManager.OnClickedSettingReturnButton();
        SoundMnager.Instance.PlayClip(22);
    }
   
}

    


