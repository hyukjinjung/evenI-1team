
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
    bool isGameOver = false; 


    UIStartPanel uiStartPanel;
    public UIStartPanel UIStartPanel    { get { return uiStartPanel; } }

    UIPlayingPanel uiPlayingPanel;
    public UIPlayingPanel UIPlayingPanel { get { return uiPlayingPanel; } }
    
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject CautionPanel;
    public GameObject ResultPanel;
    public GameObject StoryPanel;
    public GameObject SettingPanel;
    public GameObject SourcePanel;


    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI ResultScoreText;
    public TextMeshProUGUI ResultBestScoreText;
    public TextMeshProUGUI ResultPileUpScoreText;
    public TextMeshProUGUI ResultCoinScoreText;
    
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
    public Button StoryReTurnButton;
    public Button StoryButton;
    public Button SettingInstagrambutton;
    public Button SettingSourceButton;
    public Button SettingSoundEffectButton;
    public Button SettingBGMButton;
    public Button CreditsReTurnButton;

 
    private bool isActive = true; 


    private int score = 0;
    private int bestScore = 0;


    public PlayerAnimationController playerAnimationController;


    private void Awake()
    {
        uiStartPanel = GetComponentInChildren<UIStartPanel>(true);
        uiStartPanel.Initialize(this);
        
        uiPlayingPanel = GetComponentInChildren<UIPlayingPanel>(true);
        uiPlayingPanel.Initialize(this);
        
    }


    // Start is called before the first frame update
    void Start()
    {
        uiStartPanel.SetActive(true);
        uiPlayingPanel.SetActive(false);
        

        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        CautionPanel.SetActive(false);
        ResultPanel.SetActive(false);
        StoryPanel.SetActive(false);
        SettingPanel.SetActive(false);
        SourcePanel.SetActive(false);

        
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


    // Update is called once per frame
    void Update()
    {

    }
   
    public void StartGame()
    {
        isGameOver = false;
        playerAnimationController.PlayGameStartAnimation();

        
        uiStartPanel.SetActive(false);
        uiPlayingPanel.SetActive(true);
        
        SettingPanel.SetActive(false);
        GameOverPanel.SetActive(false);

    }

    public void GameOver()
    {
        isGameOver = true;

        GameOverPanel.SetActive(true);
        PausePanel.SetActive(false);
        CautionPanel.SetActive(false);
        playerAnimationController.PlayGameStartAnimation();
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void OnClickedSettingButton()
    {
        uiStartPanel.SetActive(false);
        SettingPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
     public void OnPauseButtonClicked()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f; //Gamepause
    }


    public void OnSourceReTurnButtonClicked()
    {
        SettingPanel.SetActive(true);
        SourcePanel.SetActive(false);
    }


    public void OnPauseMainHomeButtonClicked()
    {
        CautionPanel.SetActive(true);
    }

    public void OnPauseContinueButtonClicked()
    {
        uiPlayingPanel.SetActive(true);
        PausePanel.SetActive(false);
        Time.timeScale = 1f;

    }

    public void OnCautionMainHomeButtonClicked()
    {
        uiStartPanel.SetActive(true);
        uiPlayingPanel.SetActive(false);
        
        PausePanel.SetActive(false);
        CautionPanel.SetActive(false);

       
    }

    public void OnCautionContineButtonClicked()
    {
        uiPlayingPanel.SetActive(false);
        CautionPanel.SetActive(false);
        PausePanel.SetActive(true);
    }

    
    public void OnAdsComeBackButtonClicked()
    {
        uiPlayingPanel.SetActive(true);
    }
    

    public void OnSkipButtonClicked()
    {
        uiPlayingPanel.SetActive(false);
        ResultPanel.SetActive(true);
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
        ResultPanel.SetActive(false); 
       
    }

    public void OnResultMainHomeButtonClicked()
    {
        uiStartPanel.SetActive(true);
        ResultPanel.SetActive(false);

    }
    

    public void OnStoryReTurnButtonClicked()
    {
        ResultPanel.SetActive(true);
    }

   public void OnStoryButtonClicked()
    {

    }


   
    
       
        


}
