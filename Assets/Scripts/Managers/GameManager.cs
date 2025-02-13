using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public Animator animator;

    private bool isGameOver = false;

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
        animator.ResetTrigger("GameOver"); // ���� ���� �� Ʈ���� �ʱ�ȭ

        // ���� ����
        Debug.Log("���� ����");
        player.GetComponent<PlayerMovement>().enabled = true; // �÷��̾� �̵� Ȱ��ȭ
        player.GetComponent<PlayerAttackController>().enabled = true; // �÷��̾� ���� Ȱ��ȭ

        Time.timeScale = 1f; // ���� �帧 �ٽ� ����
    }

    public void GameOver()
    {
        if (isGameOver) return; // �̹� ���� ������ ��� ���� ����

        isGameOver = true;
        Debug.Log("���� ����");

        player.GetComponent<PlayerMovement>().enabled = false; // �÷��̾� �̵� ��Ȱ��ȭ
        player.GetComponent<PlayerAttackController>().enabled = false; // �÷��̾� ���� ��Ȱ��ȭ

        animator.SetTrigger("GameOver");
        player.GetComponent<Animator>().Play("Die");

        // �ִϸ��̼� �Ŀ� Time.timeScale�� 0���� ����
        StartCoroutine(FreezeGameAfterDelay());

    }    
        
    private IEnumerator FreezeGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f); // �ִϸ��̼� ������ ���� 0.5�� ���
        Time.timeScale = 0f; // ���� �Ͻ� ����
    }
}

