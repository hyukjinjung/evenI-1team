using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResultPanel : UIPanel
{
    private int score = 0;
    private int bestScore = 0;

    public int Score { get { return score; } }
    public int BestScore { get { return bestScore; } }


    public Button ResultScoreTwiceButton;
    public Button ResultReStartButton;
    public Button ResultMainHomeButton;
    public Button ResultCoinTwiceButton;

    [SerializeField] TextMeshProUGUI resultScoreText;
    [SerializeField] TextMeshProUGUI resultBestScoreText;
    //[SerializeField] TextMashProUGUI resultPileUpScoreText;
    [SerializeField] TextMeshProUGUI resultCoinscoreText;
    public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);
        ResultCoinTwiceButton.onClick.AddListener(OnResultCoinTwiceButtonClicked);
        ResultScoreTwiceButton.onClick.AddListener(OnResultScoreTwiceButtonClicked);
        ResultReStartButton.onClick.AddListener(OnClickedResultReStartButton);
        ResultMainHomeButton.onClick.AddListener(OnClickedResultMainHomeButton);
    }
    
    void OnClickedResultReStartButton()
    {
        uiManager.OnResultReStartButtonClicked();
    }

    void OnClickedResultMainHomeButton()
    {
        uiManager.OnResultMainHomeButtonClicked();
    }
    
    void OnResultCoinTwiceButtonClicked()
    {

    }
    void OnResultScoreTwiceButtonClicked()
    {

    }

    public void UpdateScore(int score)
    {
        resultScoreText.text = score.ToString();
    }

    //public void UpdateBestScore(int score)
    //{
    //    if (bestScore < score)
    //    {
    //        bestScore = score;
    //    }
    //    resultBestScoreText.text = score.ToString();

    //}

}
