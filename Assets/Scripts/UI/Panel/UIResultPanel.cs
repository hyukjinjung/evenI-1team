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
        SoundMnager.Instance.PlayClip(1);
    }

    void OnClickedResultMainHomeButton()
    {
        uiManager.OnResultMainHomeButtonClicked();
        SoundMnager.Instance.PlayClip(22);
    }

    void OnResultCoinTwiceButtonClicked()
    {
        SoundMnager.Instance.PlayClip(22);
    }
    void OnResultScoreTwiceButtonClicked()
    {
        SoundMnager.Instance.PlayClip(22);
    }

    public void UpdateScore(int score)
    {
        resultScoreText.text = score.ToString();
       
    }

    public void UpdateBestScore(int score)
    {
        resultBestScoreText.text = score.ToString();
        SoundMnager.Instance.PlayClip(22);

    }

}
