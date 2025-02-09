using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public Animator playerAnimator;
    public Button startButton;
    public GameObject homePanel; // 홈 화면 패널
    public GameObject gamePanel; // 게임 화면 패널

    private void Start()
    {
        startButton.onClick.AddListener(OnGameStartClicked);

        // 초기 패널 상태 설정 (홈 화면)
        homePanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    void OnGameStartClicked()
    {
        startButton.interactable = false; // 버튼 중복 입력 방지
        StartCoroutine(PlayTurnAround());
    }

    IEnumerator PlayTurnAround()
    {
        playerAnimator.SetTrigger("GameStart");

        // 애니메이션 길이만큼 대기
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;
        yield return new WaitForSeconds(animationDuration);

        // 패널 활성화/ 비활성화
        homePanel.SetActive(false);
        gamePanel.SetActive(true);
    }
}
