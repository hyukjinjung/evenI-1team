using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResultPanel : UIPanel
{
    public Button ResultScoreTwiceButton;
    public Button ResultReStartButton;
    public Button ResultMainHomeButton;
    public Button ResultCoinTwiceButton;

    public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);
        ResultCoinTwiceButton.onClick.AddListener(OnResultCoinTwiceButtonClicked);
        ResultScoreTwiceButton.onClick.AddListener(OnResultScoreTwiceButtonClicked);
        ResultReStartButton.onClick.AddListener(OnResultReStartButtonClicked);
        ResultMainHomeButton.onClick.AddListener(OnResultMainHomeButtonClicked);
    }
    
    void OnResultReStartButtonClicked()
    {
        uiManager.StartGame();
    }

    void OnResultMainHomeButtonClicked()
    {
        uiManager.OnResultMainHomeButtonClicked();
    }
    
    void OnResultCoinTwiceButtonClicked()
    {

    }
    void OnResultScoreTwiceButtonClicked()
    {

    }

}
