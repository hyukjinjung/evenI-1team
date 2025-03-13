using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
    UIStartPanel uiStartPanel;
    public UIStartPanel UIStartPanel { get { return uiStartPanel; } }

    UIPlayingPanel uiPlayingPanel;
    public UIPlayingPanel UIPlayingPanel { get { return uiPlayingPanel; } }

    UIGameOverPanel uiGameOverPanel;
    public UIGameOverPanel UIGameOverPanel { get { return uiGameOverPanel; } }

    UIResultPanel uiResultPanel;
    public UIResultPanel UIResultPanel { get { return uiResultPanel; } }

    UISettingPanel uiSettingPanel;
    public UISettingPanel UISettingPanel { get { return uiSettingPanel; } }

    UIPausePanel uiPausePanel;
    public UIPausePanel UIPausePanel { get { return uiPausePanel; } }

    UICautionPanel uiCautionPanel;
    public UICautionPanel UICautionPanel { get { return uiCautionPanel; } }

    UIModePanel uiModePanel;
    public UIModePanel UIModePanel { get { return uiModePanel; } }


    //public GameObject StoryPanel;
    //public GameObject CreditPanel;

    //SoundUI soundUI;
    //public SoundUI SoundUI { get { return SoundUI; } }





    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI feverScoreText;
    public TextMeshProUGUI ResultScoreText;
    public TextMeshProUGUI ResultBestScoreText;
    public TextMeshProUGUI ResultPileUpScoreText;
    public TextMeshProUGUI ResultCoinScoreText;

    //out Game
    public Button StoryReTurnButton;
    public Button StoryButton;
    public Button SettingInstagrambutton;
    public Button SettingSourceButton;
    public Button SettingSoundEffectButton;
    public Button SettingBGMButton;
    public Button SettingReturnButton;
    public Button SettingCreditButton;
    public Button CreditsReTurnButton;

    public Button RankingButton;
    public Button SkinButton;
    public Button EncyButton;
    public Button AchieveButton;
    public Button AdsButton;
    public Button ModeButton;
    public Button EventButton;

    //in Game
    public Button PauseMainHomeButton;
    public Button PauseContinueButton;
    public Button PauseBGMButton;
    public Button PauseSoundEffectButton;
    public Button CautionMainHomeButton;
    public Button CautionContinueButton;
    public Button AdsComeBackButton;
    public Button SkipButton;
    public Button ResultScoreTwiceButton;
    public Button ResultReStartButton;
    public Button ResultMainHomeButton;
    public Button ResultCoinTwiceButton;
   

 
    public PlayerAnimationController playerAnimationController;


    private void Awake()
    {
        uiStartPanel = GetComponentInChildren<UIStartPanel>(true);
        uiStartPanel.Initialize(this);
        
        uiPlayingPanel = GetComponentInChildren<UIPlayingPanel>(true);
        uiPlayingPanel.Initialize(this);

        uiGameOverPanel = GetComponentInChildren<UIGameOverPanel>(true);
        uiGameOverPanel.Initialize(this);

        uiResultPanel = GetComponentInChildren<UIResultPanel>(true);
        uiResultPanel.Initialize(this);

        uiSettingPanel = GetComponentInChildren<UISettingPanel>(true);
        uiSettingPanel.Initialize(this);    

        uiPausePanel = GetComponentInChildren<UIPausePanel>(true);
        uiPausePanel.Initialize(this);
        
        uiCautionPanel = GetComponentInChildren<UICautionPanel>(true);
        uiCautionPanel.Initialize(this);

        uiModePanel = GetComponentInChildren<UIModePanel>(true);
        uiModePanel.Initialize(this);

        //soundUI = GetComponentInChildren<SoundUI>(true);
        //soundUI.Initialize(this);
      
    }


    // Start is called before the first frame update
    void Start()
    {
        uiStartPanel.SetActive(true);
        uiPlayingPanel.SetActive(false);
        uiGameOverPanel.SetActive(false);
        uiResultPanel.SetActive(false);
        uiSettingPanel.SetActive(false);
        uiPausePanel.SetActive(false);
        uiCautionPanel.SetActive(false);
        uiModePanel.SetActive(false);

        //StoryPanel.SetActive(false);

        RankingButton.onClick.AddListener(OnClickedRankingButton);
        SkinButton.onClick.AddListener(OnClickedSkinButton);
        EncyButton.onClick.AddListener(OnClickedEncyButton);
        AchieveButton.onClick.AddListener(OnClickedAchieveButton);
        AdsButton.onClick.AddListener(OnClickedAdsButton);
        ModeButton.onClick.AddListener(OnClickedModeButton);
        EventButton.onClick.AddListener(OnClickedEventButton);

        PauseMainHomeButton.onClick.AddListener(OnPauseMainHomeButtonClicked);
        PauseContinueButton.onClick.AddListener(OnPauseContinueButtonClicked);

        CautionMainHomeButton.onClick.AddListener(OnCautionMainHomeButtonClicked);
        CautionContinueButton.onClick.AddListener(OnCautionContineButtonClicked);

        AdsComeBackButton.onClick.AddListener(OnAdsComeBackButtonClicked);
        SkipButton.onClick.AddListener(OnSkipButtonClicked);

        ResultCoinTwiceButton.onClick.AddListener(OnResultCoinTwiceButtonClicked);
        ResultScoreTwiceButton.onClick.AddListener(OnResultScoreTwiceButtonClicked);
        ResultReStartButton.onClick.AddListener(OnResultReStartButtonClicked);
        ResultMainHomeButton.onClick.AddListener(OnResultMainHomeButtonClicked);

        StoryReTurnButton.onClick.AddListener(OnStoryReTurnButtonClicked);
        StoryButton.onClick.AddListener(OnStoryButtonClicked);

        CreditsReTurnButton.onClick.AddListener(OnSourceReTurnButtonClicked);
        
        playerAnimationController = FindObjectOfType<PlayerAnimationController>();
        
    }

    public void StartGame()
    {
        playerAnimationController.PlayGameStartAnimation();

        uiStartPanel.SetActive(false);
        uiPlayingPanel.SetActive(true);
        uiGameOverPanel.SetActive(false);
        uiSettingPanel.SetActive(false);
    }
    public void GameOver()
    {
        uiGameOverPanel.SetActive(true);
        uiPausePanel.SetActive(false);
        uiCautionPanel.SetActive(false);
        
        playerAnimationController.PlayGameStartAnimation();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnClickedSettingButton()
    {
        uiStartPanel.SetActive(false);
        uiSettingPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void OnClickedSettingInstaGramButton()
    {

    }
    public void OnClickedSettingCreditButton()
    {

    }
    public void OnClickedSettingSoundEffectButton()
    {
        //soundUI.SettingSoundEffectButtonSoundState();
    }
    public void OnClickedSettingBGMButton()
    {
        //soundUI.SettingBGMButtonSoundState();
    }
    public void OnClickedSettingReturnButton()
    {
        uiStartPanel.SetActive(true);
        uiSettingPanel.SetActive(false);
    }
    public void OnPauseButtonClicked()
    {
        uiPausePanel.SetActive(true);
        Time.timeScale = 0f; //Gamepause
    }
    public void OnSourceReTurnButtonClicked()
    {
        uiSettingPanel.SetActive(true);
        //CreditPanel.SetActive(false);
    }
    public void OnPauseMainHomeButtonClicked()
    {
        uiCautionPanel.SetActive(true);
    }
    public void OnPauseContinueButtonClicked()
    {
        uiPlayingPanel.SetActive(true);
        uiPausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OnClickedPauseBGMButton()
    {
        //soundUI.PauseBGMButtonSoundState();
    }
    public void OnClickedPauseSoundEffectButtonSoundState()
    {
        //soundUI.PauseSoundEffectButtonSoundState();
    }
    public void OnCautionMainHomeButtonClicked()
    {
        RestartGame();
    }

    public void OnCautionContineButtonClicked()
    {
        uiPlayingPanel.SetActive(false);
        uiCautionPanel.SetActive(false);
        uiPausePanel.SetActive(true);

        StartGame();
    }

    
    public void OnAdsComeBackButtonClicked()
    {
        uiPlayingPanel.SetActive(true);
    }


    public void OnSkipButtonClicked()
    {
        uiPlayingPanel.SetActive(false);
        uiGameOverPanel.SetActive(false);
        uiResultPanel.SetActive(true);
        SoundManager.Instance.PlayClip(3);
    }
    void OnResultCoinTwiceButtonClicked()
    {

    }

    void OnResultScoreTwiceButtonClicked()
    {

    }

    public void OnResultReStartButtonClicked()
    {
        uiPlayingPanel.SetActive(true);
        uiGameOverPanel.SetActive(false);
        uiResultPanel.SetActive(false);

        RestartGame();
    }

    public void OnResultMainHomeButtonClicked()
    {
        uiResultPanel.SetActive(false);

        RestartGame();
    }

   
    public void OnStoryButtonClicked()
    {

    }
    public void OnStoryReTurnButtonClicked()
    {
        uiResultPanel.SetActive(true);
    }

    public void OnClickedRankingButton()
    {

    }

    public void OnClickedSkinButton()
    {

    }
    public void OnClickedEncyButton()
    {

    }

    public void OnClickedAchieveButton()
    {

    }

    public void OnClickedAdsButton()
    {

    }
    public void OnClickedModeButton()
    {
        uiStartPanel.SetActive(false);
        uiModePanel.SetActive(true);
    }
    public void OnClickedEventButton()
    {

    }
}
