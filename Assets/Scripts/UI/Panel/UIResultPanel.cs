using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResultPanel : UIPanel
{
    public Button ResultScoreTwiceButton;
    public Button ResultReStartButton;
    public Button ResultMainHomeButton;
    public Button ResultCoinTwiceButton;

    [SerializeField] TextMeshProUGUI resultScoreText;
    [SerializeField] TextMeshProUGUI resultBestScoreText;
    //[SerializeField] TextMashProUGUI resultPileUpScoreText;
    //[SerializeField] TextMeshProUGUI resultCoinscoreText;
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

    public void UpdateResultScore(int score)
    {
        resultBestScoreText.text = score.ToString();
    }
    public void UpdateResultBestScore(int score)
    { 
        resultBestScoreText.text =score.ToString();
    }
}
