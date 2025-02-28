using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPausePanel : UIPanel
{
    public Button PauseMainHomeButton;
    public Button PauseContinueButton;
    public Button PauseBGMButton;
    public Button PauseSoundEffectButton;

   public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);
        PauseMainHomeButton.onClick.AddListener(OnClickedPauseMainHomeButton);
        PauseContinueButton.onClick.AddListener(OnClickedPauseContinueButton);
        PauseBGMButton.onClick.AddListener(OnClickedPauseBGMButton);
        PauseSoundEffectButton.onClick.AddListener(OnClickedPauseSoundEffectButton);
    }


    void OnClickedPauseMainHomeButton()
    {
        uiManager.OnPauseMainHomeButtonClicked();
    }
    void OnClickedPauseContinueButton()
    {
        uiManager.OnPauseContinueButtonClicked();
    }
    void OnClickedPauseBGMButton()
    {

    }
    void OnClickedPauseSoundEffectButton()
    {

    }
}
