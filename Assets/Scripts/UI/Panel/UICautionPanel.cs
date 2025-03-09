using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICautionPanel : UIPanel
{
    public Button CautionMainHomeButton;
    public Button CautionContinueButton;

    public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);
        CautionMainHomeButton.onClick.AddListener(OnClickedCautionMainHomeButton);
        CautionContinueButton.onClick.AddListener(OnClickedCautionContinueButon);
    }

    void OnClickedCautionMainHomeButton()
    {
        uiManager.OnCautionMainHomeButtonClicked();
        SoundMnager.Instance.PlayClip(22);
    }
    void OnClickedCautionContinueButon()
    {
        uiManager.OnAdsComeBackButtonClicked();
        SoundMnager.Instance.PlayClip(22);
    }
}
