using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIGameOverPanel : UIPanel
{
    public Button AdscomebackButton;
    public Button SkipButton;

    public TextMeshProUGUI textMeshProUGUI;
    public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);
        AdscomebackButton.onClick.AddListener(OnAdscomebackButtonClicked);
        SkipButton.onClick.AddListener(OnSkipButtonClicked);
    }
    void OnAdscomebackButtonClicked()
    {
        
    }
    void OnSkipButtonClicked()
    {
        uiManager.OnSkipButtonClicked();
    }
}
