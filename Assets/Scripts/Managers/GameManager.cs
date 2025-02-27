using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    public TestTileManager tileManager;
    public PlayerAnimationController playerAnimationController;

    public GameObject player;

    private bool isGameOver = false;

    public GameObject StartPanel;
    public GameObject PlayingPanel;
    public GameObject GameOverPanel;

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
    }


    public void StartGame()
    {
        Debug.Log("게임 시작");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerAttackController>().enabled = true;
        }

        Time.timeScale = 1f;

        StartPanel.SetActive(false);
        PlayingPanel.SetActive(true);
    }

    public void GameOver()
    {
        if (isGameOver) return;

        GameOverPanel.SetActive(true);

        isGameOver = true;


        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerAttackController>().enabled = false;

            playerAnimationController.PlayGameOverAnimation();
            player.GetComponentInChildren<Animator>().Play("Die");

        }
   
        
        StartCoroutine(FreezeGameAfterDelay());

    }    
        
    private IEnumerator FreezeGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 0f;
    }
}

