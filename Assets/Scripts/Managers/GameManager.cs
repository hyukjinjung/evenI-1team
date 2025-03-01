using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isGodMode = false;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                Debug.Log("GameManager NULL");
            }

            return _instance;
        }
    }


    public UIManager uiManager;

    UIPlayingPanel uiPlayingPanel;
    UIResultPanel uiResultPanel;
    UIGameOverPanel uiGameOverPanel;

    public TestTileManager tileManager;

    public GameObject StartPanel;
    public GameObject PlayingPanel;
    public GameObject GameOverPanel;

    // -------------------------- player
    public GameObject player;
    private PlayerMovement playerMovement;
    private PlayerInputController playerInputController;
    private PlayerAttackController playerAttackController;
    private PlayerAnimationController playerAnimationController;
    
    public PlayerMovement PlayerMovement{get{return playerMovement;}}
    public PlayerAttackController PlayerAttackController { get { return playerAttackController; } }
    public PlayerInputController PlayerInputController{get{return playerInputController;}}
    public PlayerAnimationController PlayerAnimationController{get{return playerAnimationController;}}

    private int score = 0;
    private int bestScore = 0;
    private int resultScore = 0;
    private int resultBestScore = 0;
    
    public int Score {get{return score;}}
    public int BestScore {get{return bestScore;}}
    public int ResultScore {get{return resultScore;}}
    public int ResultBestScore {get{return resultBestScore;}}
    
    
    
    
    private PlayerScoreTracker playerScoreTracker;

    private bool isGameOver = false;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        playerScoreTracker = FindObjectOfType<PlayerScoreTracker>();
        uiManager = FindObjectOfType<UIManager>();
        
        if (player == null)
        {
            Debug.Log("unassigned player");
            return;
        }

        playerMovement = player.GetComponent<PlayerMovement>();
        playerAttackController = player.GetComponent<PlayerAttackController>();
        playerInputController = player.GetComponent<PlayerInputController>();
        playerAnimationController = player.GetComponent<PlayerAnimationController>();
        
    }

    private void Start()
    {
        uiGameOverPanel = uiManager.UIGameOverPanel;
        uiPlayingPanel = uiManager.UIPlayingPanel;
        uiResultPanel = uiManager.UIResultPanel;
    }

    public void StartGame()
    {
        Debug.Log("게임 시작");
        playerMovement.enabled = true;
        playerInputController.enabled = true;

        Time.timeScale = 1f;

        StartPanel.SetActive(false);
        PlayingPanel.SetActive(true);

        isGameOver = false;

        AddScore(0);
       //uiResultPanel.UpdateBestScore(0);

       // uiResultPanel.UpdateBestScore(bestScore);
    }


    public void GameOver()
    {
        Debug.Log("게임 오버 발생!");

        if (isGodMode) return;
        if (isGameOver) return;

        GameOverPanel.SetActive(true);

        isGameOver = true;


        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerAttackController>().enabled = false;

            PlayerAnimationController.PlayGameOverAnimation();
            player.GetComponentInChildren<Animator>().Play("Die");

        }
   
        StartCoroutine(FreezeGameAfterDelay());

        AddScore(score);

        //uiResultPanel.UpdateBestScore(score);

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private IEnumerator FreezeGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 0f;
    }
    public void AddScore(int value)
    {
        if (isGameOver) return;

        score += value;
        
        uiPlayingPanel.UpdateScore(score);
        uiGameOverPanel.UpdateScore(score);
        uiResultPanel.UpdateScore(score);

        Debug.Log(Score);
    }
    
    //public void UpdateBestScore(int value)
    //{ 
    //    if (bestScore < value)
    //    {
    //        bestScore = value;
    //    }
    //}
}