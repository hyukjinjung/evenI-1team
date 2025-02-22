using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public PlayerAnimationController playerAnimationController;

    private bool isGameOver = false;

    public GameObject StartPanel;
    public GameObject PlayingPanel;
    public GameObject GameOverPanel;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void StartGame()
    {
        //playerController.ResetTrigger("GameOver"); // ���� ���� �� Ʈ���� �ʱ�ȭ

        // ���� ����
        Debug.Log("���� ����");
        player.GetComponent<PlayerMovement>().enabled = true; // �÷��̾� �̵� Ȱ��ȭ
        player.GetComponent<PlayerAttackController>().enabled = true; // �÷��̾� ���� Ȱ��ȭ

        Time.timeScale = 1f; // ���� �帧 �ٽ� ����

        StartPanel.SetActive(false);
        PlayingPanel.SetActive(true);
    }

    public void GameOver()
    {
        if (isGameOver) return; // �̹� ���� ������ ��� ���� ����

        GameOverPanel.SetActive(true);

        isGameOver = true;
        Debug.Log("���� ����");

        player.GetComponent<PlayerMovement>().enabled = false; // �÷��̾� �̵� ��Ȱ��ȭ
        player.GetComponent<PlayerAttackController>().enabled = false; // �÷��̾� ���� ��Ȱ��ȭ

        playerAnimationController.PlayGameOverAnimation();
        player.GetComponentInChildren<Animator>().Play("Die");

        // �ִϸ��̼� �Ŀ� Time.timeScale�� 0���� ����
        StartCoroutine(FreezeGameAfterDelay());

    }    
        
    private IEnumerator FreezeGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f); // �ִϸ��̼� ������ ���� 0.5�� ���
        Time.timeScale = 0f; // ���� �Ͻ� ����
    }
}

