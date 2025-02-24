
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


    public Image buttonImage;    // �̹��� ������Ʈ (��ư�� �̹���)
    public Sprite PauseSoundEffectButtonOnSprite;  // Ȱ��ȭ�� ���� �̹���
    public Sprite PauseBGMButtonOffSprite; // ��Ȱ��ȭ�� ���� �̹���
    public Sprite SettingBGMButtonOnSprite;  // Ȱ��ȭ�� ���� �̹���
    public Sprite SettingSoundEffectButtonOffSprite;

    private bool isActive = true; // �ʱ� ���´� Ȱ��ȭ ����


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
        // ��ư Ŭ�� �� ToggleImage �Լ� ȣ�� */

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

        // �ִϸ��̼� ���̸�ŭ ���
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;
        yield return new WaitForSeconds(animationDuration);

       
    }


    //��ư Ŭ�� 

    void OnGameStartButtonClicked()
    {
        GameStartButton.interactable = false; // ��ư �ߺ� �Է� ����
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

        //������ ������ �� ���� �ջ� ȭ�� ��ȹ�� Ȯ���غ��� 
    }

    void OnCautionContineButtonClicked()
    {
        PlayingPanel.SetActive(false);
        CautionPanel.SetActive(false);
        PausePanel.SetActive(true);
    }

    //���ӿ��� �׾��� �� ���� ���� �г� 5�� �̻� ��ġ ���� �� ���â���� ȭ�� ��ȯ ??
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
        // ����� �ϳ� �ҽ��ϸ鼭 30�� Ÿ�̸� ���۰� ���ÿ� ���� ���� 
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
    

     // ��ư Ŭ�� �� �̹��� ��ȯ
     private void PauseSoundEffectImage()
     {
            // �̹��� ���� ���
            isActive = !isActive;

            if (isActive)
            {
                buttonImage.sprite = ButtonOnSprite;  // Ȱ��ȭ�� �̹����� ����
            }
            else
            {
                buttonImage.sprite = ButtonOffSprite;  // ��Ȱ��ȭ�� �̹����� ����
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


    // �Ҹ� ���¿� ���� �̹����� ������Ʈ�ϴ� �Լ�
    /* private void UpdateImage()
     {
         // �Ҹ� ���¿� ���� �̹��� ����
         if (isSoundActive)
         {
             ButtonImage.sprite = ButtonOnSprite;  // Ȱ��ȭ�� �̹���
         }
         else
         {
             ButtonImage.sprite = ButtonOffSprite;  // ��Ȱ��ȭ�� �̹���
         }
     }*/

    //���� �ջ� �̾ ¥�ߵ�
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
    public GameObject homePanel; // Ȩ ȭ�� �г�
    public GameObject gamePanel; // ���� ȭ�� �г�

    private void Start()
    {
        startButton.onClick.AddListener(OnGameStartClicked);

        // �ʱ� �г� ���� ���� (Ȩ ȭ��)
        homePanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    void OnGameStartClicked()
    {
        startButton.interactable = false; // ��ư �ߺ� �Է� ����
        StartCoroutine(PlayTurnAround());
    }

    IEnumerator PlayTurnAround()
    {
        playerAnimator.SetTrigger("GameStart");

        // �ִϸ��̼� ���̸�ŭ ���
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;
        yield return new WaitForSeconds(animationDuration);

        // �г� Ȱ��ȭ/ ��Ȱ��ȭ
        homePanel.SetActive(false);
        gamePanel.SetActive(true);
    }
}*/
}
