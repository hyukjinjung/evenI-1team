
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
    bool isGameOver = false; 


    public GameObject StartPanel;
    public GameObject PlayingPanel;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject CautionPanel;
    public GameObject ResultPanel;
    public GameObject StoryPanel;
    public GameObject SettingPanel;
    public GameObject SourcePanel;


    public Animator playerAnimator;


    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI ResultScoreText;
    public TextMeshProUGUI ResultBestScoreText;
    public TextMeshProUGUI ResultPileUpScoreText;
    public TextMeshProUGUI ResultCoinScoreText;


    public Button GameStartButton;
    public Button SettingButton;
    public Button AttackButton;
    public Button LeftButton;
    public Button RightButton;
    public Button PauseButton;
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
    public Button SourceReTurnButton;

   /* PauseSoundEffectButton.onClick.AddListener(PauseSoundEffectImage);
        PauseBGMButton.onClick.AddListener(PauseBGMImage);
        SettingBGMButton.onClick.AddListener(SettingBGMImage);
        SettingSoundEffectButton.onClick.AddListener(SettingSoundEffectImage);*/


    public Image buttonImage;    // 이미지 컴포넌트 (버튼의 이미지)
    public Sprite PauseSoundEffectButtonOnSprite;  // 활성화된 상태 이미지
    public Sprite PauseBGMButtonOffSprite; // 비활성화된 상태 이미지
    public Sprite SettingBGMButtonOnSprite;  // 활성화된 상태 이미지
    public Sprite SettingSoundEffectButtonOffSprite;

    private bool isActive = true; // 초기 상태는 활성화 상태


    private int score = 0;
    private int bestScore = 0;

    Sprite ButtonOnSprite;
    Sprite ButtonOffSprite;

    public PlayerInputController playerInputController;



    // Start is called before the first frame update
    void Start()
    {
        StartPanel.SetActive(true);
        PlayingPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        CautionPanel.SetActive(false);
        ResultPanel.SetActive(false);
        StoryPanel.SetActive(false);
        SettingPanel.SetActive(false);
        SourcePanel.SetActive(false);

        GameStartButton.onClick.AddListener(OnGameStartButtonClicked);
        PauseButton.onClick.AddListener(OnPauseButtonClicked);
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
        SettingButton.onClick.AddListener(OnSettingButtonClicked);
        SourceReTurnButton.onClick.AddListener(OnSourceReTurnButtonClicked);

        /*public Button BGMButton;
        public Button SoundEffectButton;
        public Button SettingSoundEffectButton;
        public Button SourceReTurnButton;
        // 버튼 클릭 시 ToggleImage 함수 호출 */

        PauseSoundEffectButton.onClick.AddListener(PauseSoundEffectImage);
        PauseBGMButton.onClick.AddListener(PauseBGMImage);
        SettingBGMButton.onClick.AddListener(SettingBGMImage);
        SettingSoundEffectButton.onClick.AddListener(SettingSoundEffectImage);

    }


    // Update is called once per frame
    void Update()
    {

    }
   
    public void StartGame()
    {
        isGameOver = false;

        PlayTurnAround();

        StartPanel.SetActive(false);
        PlayingPanel.SetActive(true);
        SettingPanel.SetActive(false);

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

    IEnumerator PlayTurnAround()
    {
        playerAnimator.SetTrigger("GameStart");

        // 애니메이션 길이만큼 대기
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;
        yield return new WaitForSeconds(animationDuration);

       
    }


    //버튼 클릭 

    void OnGameStartButtonClicked()
    {
        GameStartButton.interactable = false; // 버튼 중복 입력 방지
        StartCoroutine(PlayTurnAround());
    }

    void OnSettingButtonClicked()
    {
        SettingPanel.SetActive(true);
        StartPanel.SetActive(false);
    }

    public void OnSourceReTurnButtonClicked()
    {
        SettingPanel.SetActive(true);
        SourcePanel.SetActive(false);
    }

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

    //게임에서 죽었을 때 게임 오버 패널 5초 이상 터치 없을 시 결과창으로 화면 전환 ??
    void OnAdsComeBackButtonClicked()
    {
        PlayingPanel.SetActive(true);

    }
    

    void OnSkipButtonClicked()
    {
        ResultPanel.SetActive(true);
        PlayingPanel.SetActive(false);
    }

    void OnResultCoinTwiceButtonClicked()
    {

    }

    void OnResultScoreTwiceButtonClicked()
    {

    }

    void OnResultReStartButtonClicked()
    {
        // 입장권 하나 소실하면서 30분 타이머 시작과 동시에 게임 시작 
        PlayingPanel.SetActive(true);
    }

    void OnResultMainHomeButtonClicked()
    {
        StartPanel.SetActive(true);
    }
    

    void OnStoryReTurnButtonClicked()
    {
        ResultPanel.SetActive(true);
    }

   void OnStoryButtonClicked()
    {

    }


   
    
       
        

     /*PauseSoundEffectButton.onClick.AddListener(PauseSoundEffectImage);
     PauseBGMButton.onClick.AddListener(PauseBGMImage);
     SettingBGMButton.onClick.AddListener(SettingBGMImage);
     SettingSoundEffectButton.onClick.AddListener(SettingSoundEffectImage);*/
    

     // 버튼 클릭 시 이미지 전환
     private void PauseSoundEffectImage()
     {
            // 이미지 상태 토글
            isActive = !isActive;

            if (isActive)
            {
                buttonImage.sprite = ButtonOnSprite;  // 활성화된 이미지로 설정
            }
            else
            {
                buttonImage.sprite = ButtonOffSprite;  // 비활성화된 이미지로 설정
            }
     }

   

    private void PauseBGMImage()
    {
        isActive = !isActive;

        if (isActive)
        {
            buttonImage.sprite = ButtonOnSprite; 
        }
        else
        {
            buttonImage.sprite = ButtonOffSprite;
        }
    }

    private void SettingBGMImage()
    {
        isActive = !isActive;

        if (isActive)
        {
            buttonImage.sprite = ButtonOnSprite;
        }
        else
        {
            buttonImage.sprite = ButtonOffSprite;
        }
    }

    private void SettingSoundEffectImage()
    {
        isActive = !isActive;

        if (isActive)
        {
            buttonImage.sprite = ButtonOnSprite;
        }
        else
        {
            buttonImage.sprite = ButtonOffSprite;
        }
    }


    // 소리 상태에 맞춰 이미지를 업데이트하는 함수
    /* private void UpdateImage()
     {
         // 소리 상태에 따라 이미지 변경
         if (isSoundActive)
         {
             ButtonImage.sprite = ButtonOnSprite;  // 활성화된 이미지
         }
         else
         {
             ButtonImage.sprite = ButtonOffSprite;  // 비활성화된 이미지
         }
     }*/

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

    /*public class PanelManager : MonoBehaviour
{
    public Animator playerAnimator;
    public Button startButton;
    public GameObject homePanel; // 홈 화면 패널
    public GameObject gamePanel; // 게임 화면 패널

    private void Start()
    {
        startButton.onClick.AddListener(OnGameStartClicked);

        // 초기 패널 상태 설정 (홈 화면)
        homePanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    void OnGameStartClicked()
    {
        startButton.interactable = false; // 버튼 중복 입력 방지
        StartCoroutine(PlayTurnAround());
    }

    IEnumerator PlayTurnAround()
    {
        playerAnimator.SetTrigger("GameStart");

        // 애니메이션 길이만큼 대기
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;
        yield return new WaitForSeconds(animationDuration);

        // 패널 활성화/ 비활성화
        homePanel.SetActive(false);
        gamePanel.SetActive(true);
    }
}*/
}
