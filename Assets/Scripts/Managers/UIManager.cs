
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
    
    public GameObject StartPanel;
    public GameObject PlayingPanel;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject CautionPanel;

  

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;


    bool isGameOver = false;


    public Button PauseButton;
    public Button PauseMainHomeButton;
    public Button PauseContinueButton;
    public Button CautionMainHomeButton;
    public Button CautionContinueButton;


    private int score = 0;
    private int bestScore = 0;



     


    // Start is called before the first frame update
    void Start()
    {
        StartPanel.SetActive(true);
        PlayingPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        CautionPanel.SetActive(false);

        PauseButton.onClick.AddListener(OnPauseButtonClicked);
        PauseMainHomeButton.onClick.AddListener(OnPauseMainHomeButtonClicked);
        PauseContinueButton.onClick.AddListener(OnPauseContinueButtonClicked);
        CautionMainHomeButton.onClick.AddListener(OnCautionMainHomeButtonClicked);
        CautionContinueButton.onClick.AddListener(OnCautionContineButtonClicked);
    }



    // Update is called once per frame
    void Update()
    {

    }
   
    public void StartGame()
    {
        isGameOver = false;

        StartPanel.SetActive(false);
        PlayingPanel.SetActive(true);
       

    }

    public void GameOver()
    {
        isGameOver = true;


        GameOverPanel.SetActive(true);

        PausePanel.SetActive(false);
        CautionPanel.SetActive(false);


        UpdateBestScore(score);
    }


    public void RestartGame()// 
    {
        //SceneManager.LoadScene(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    //버튼 클릭 
    void OnPauseButtonClicked()
    {
        PausePanel.SetActive(true);

        Time.timeScale = 0f; //Gamepause
    }

    void OnPauseMainHomeButtonClicked()
    {
       // SceneManager.LoadScene("CautionPanel");

       // PausePanel.SetActive(true);
        CautionPanel.SetActive(true);
    }

    void OnPauseContinueButtonClicked()
    {
        // SceneManager.LoadScene("");

        PlayingPanel.SetActive(true);
        PausePanel.SetActive(false);
        // SceneManager.LoadScene(SceneManager.GetActiveScene())
        Time.timeScale = 1f;
     
    }

    void OnCautionMainHomeButtonClicked()
    {
        StartPanel.SetActive(true);
        PlayingPanel.SetActive(false);
        PausePanel.SetActive(false);
        CautionPanel.SetActive(false);

        //게임을 나갔을 때 점수 합산 화면 기획서 확인해보기 
    }

    void OnCautionContineButtonClicked()
    {
        PlayingPanel.SetActive(false);
        CautionPanel.SetActive(false);
        PausePanel.SetActive(true);
    }



    //점수 합산 이어서 짜야됨
    public void AddScore(int vlaue)
    {
        if (isGameOver) return;

        score += vlaue;
    }

    private void UpdateBestScore(int value)
    {
        if (bestScore < value)
        {
            bestScore = value;
        }
    }
}
