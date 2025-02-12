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
        animator.ResetTrigger("GameOver"); // 게임 시작 시 트리거 초기화

        // 게임 시작
        Debug.Log("게임 시작");
        player.GetComponent<PlayerMovement>().enabled = true; // 플레이어 이동 활성화
        player.GetComponent<PlayerAttackController>().enabled = true; // 플레이어 공격 활성화

        Time.timeScale = 1f; // 게임 흐름 다시 시작
    }

    public void GameOver()
    {
        if (isGameOver) return; // 이미 게임 오버된 경우 실행 방지

        isGameOver = true;
        Debug.Log("게임 오버");

        player.GetComponent<PlayerMovement>().enabled = false; // 플레이어 이동 비활성화
        player.GetComponent<PlayerAttackController>().enabled = false; // 플레이어 공격 비활성화

        animator.SetTrigger("GameOver");
        player.GetComponent<Animator>().Play("Die");

        // 애니메이션 후에 Time.timeScale을 0으로 설정
        StartCoroutine(FreezeGameAfterDelay());

    }    
        
    private IEnumerator FreezeGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f); // 애니메이션 실행을 위해 0.5초 대기
        Time.timeScale = 0f; // 게임 일시 정지
    }
}

