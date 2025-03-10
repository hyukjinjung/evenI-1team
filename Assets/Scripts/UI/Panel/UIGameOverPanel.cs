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

    [SerializeField] TextMeshProUGUI continueScoreText;


        
    public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);
        AdscomebackButton.onClick.AddListener(OnAdscomebackButtonClicked);
        SkipButton.onClick.AddListener(OnSkipButtonClicked);
    }
    void OnAdscomebackButtonClicked()
    {
        SoundManager.Instance.PlayClip(2);
    }
    void OnSkipButtonClicked()
    {
        uiManager.OnSkipButtonClicked();
        SoundManager.Instance.PlayClip(22);
    }

    public void UpdateScore(int score)
    {
        continueScoreText.text = score.ToString();

    }

   
}
