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

                if (_instance == null )
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

    public UIManager uiManager; // GameManager���� UIManager ����

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
            Destroy(gameObject); // �ߺ��� GameManager ����
        }
    }


    public void StartGame()
    { 

        // ���� ����
        Debug.Log("���� ����");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = true; // �÷��̾� �̵� Ȱ��ȭ
            player.GetComponent<PlayerAttackController>().enabled = true; // �÷��̾� ���� Ȱ��ȭ
        }

        Time.timeScale = 1f; // ���� �帧 �ٽ� ����

        StartPanel.SetActive(false);
        PlayingPanel.SetActive(true);
    }

    public void GameOver()
    {
        if (isGameOver) return; // �̹� ���� ������ ��� ���� ����

        GameOverPanel.SetActive(true);

        isGameOver = true;
        Debug.Log("[GameManager] ���� ����");

        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = false; // �÷��̾� �̵� ��Ȱ��ȭ
            player.GetComponent<PlayerAttackController>().enabled = false; // �÷��̾� ���� ��Ȱ��ȭ

            playerAnimationController.PlayGameOverAnimation();
            player.GetComponentInChildren<Animator>().Play("Die");

        }
   
        // �ִϸ��̼� �Ŀ� Time.timeScale�� 0���� ����
        StartCoroutine(FreezeGameAfterDelay());

    }    
        
    private IEnumerator FreezeGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f); // �ִϸ��̼� ������ ���� 0.5�� ���
        Time.timeScale = 0f; // ���� �Ͻ� ����
    }
}

