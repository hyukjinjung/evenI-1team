using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player; // �÷��̾� ĳ����

    public Animator animator;

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
        player.GetComponent<PlayerMovement>().enabled = true; // �÷��̾� �����Ʈ Ȱ��ȭ
        player.GetComponent<PlayerAttackController>().enabled = true; // �÷��̾� ������Ʈ�� Ȱ��ȭ

        Time.timeScale = 1f; // ���� �帧 �ٽ� ����
    }

    public void GameOver()
    {
        Debug.Log("���� ����");
        player.GetComponent<PlayerMovement>().enabled = false; // �÷��̾� �����Ʈ ��Ȱ��ȭ
        player.GetComponent<PlayerAttackController>().enabled = false; // �÷��̾� ������Ʈ�� ��Ȱ��ȭ

        // �ִϸ��̼� �Ŀ� Time.timeScale�� 0���� ����
        StartCoroutine(FreezeGameAfterDelay());

    }    
        
    private IEnumerator FreezeGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f); // �ִϸ��̼� ������ ���� 1�� ���
        Time.timeScale = 0f; // ���� �Ͻ� ����
    }
}

