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

                if (_instance == null)
                {
                    Debug.Log("[GameManager] GameManager NULL");
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
            }

            return _instance;
        }
    }

    public UIManager uiManager;

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
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
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
        Debug.Log("[GameManager] ���� ����");

        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerAttackController>().enabled = false;

            playerAnimationController.PlayGameOverAnimation();
            player.GetComponentInChildren<Animator>().Play("Die");

        }
   
        
        StartCoroutine(FreezeGameAfterDelay());

        UpdateBestScore(score);
    }    
        
    private IEnumerator FreezeGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 0f;
    }

    public void ResultPanel()
    {
        Debug.Log("점수 합산 창으로 이동");
    }

    public void AddScore(int value)
    {
        if (isGameOver) return;

        score += value;
    }

    private void UpdateBestScore(int value)
    {
        if (bestScore < value)
        {
            bestScore = value;
        }
    }
}

