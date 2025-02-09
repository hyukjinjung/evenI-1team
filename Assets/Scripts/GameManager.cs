using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player; // 플레이어 캐릭터

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
        animator.ResetTrigger("GameOver"); // 게임 시작 시 트리거 초기화

        // 게임 시작
        Debug.Log("게임 시작");
        player.GetComponent<PlayerMovement>().enabled = true; // 플레이어 무브먼트 활성화
        player.GetComponent<PlayerAttackController>().enabled = true; // 플레이어 어택컨트롤 활성화

        Time.timeScale = 1f; // 게임 흐름 다시 시작
    }

    public void GameOver()
    {
        Debug.Log("게임 오버");
        player.GetComponent<PlayerMovement>().enabled = false; // 플레이어 무브먼트 비활성화
        player.GetComponent<PlayerAttackController>().enabled = false; // 플레이어 어택컨트롤 비활성화

        // 애니메이션 후에 Time.timeScale을 0으로 설정
        StartCoroutine(FreezeGameAfterDelay());

    }    
        
    private IEnumerator FreezeGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f); // 애니메이션 실행을 위해 1초 대기
        Time.timeScale = 0f; // 게임 일시 정지
    }
}

