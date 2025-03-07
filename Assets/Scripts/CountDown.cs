using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    private float countdownTime = 5f;
    private bool isCounting = false;

    public Image image;
    

    void Start()
    {
        StartCoroutine(TestCoroutine());  // 코루틴 활성화
        Invoke("ActiveFalse", 5.0f);  // 3초후 비활성화
    }

    IEnumerator TestCoroutine()
    {
        while (true)
        {
            yield return null;

            Debug.Log("활성화");
        }
    }

    void ActiveFalse()
    {
        this.gameObject.SetActive(false);
    }



}


    /*public GameObject gameOverPanel;
    public Image countdownImage;
    public TextMeshProUGUI countdownText;
    public Button adsComeBackButton;
    public Button skipButton;

    private float countdownTime = 5f;
    private bool isCounting = false;

 
    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        StartCoroutine(StartCountDown());

        Debug.Log("이어하기 창 활성화");
    }

    private IEnumerator StartCountDown()
    {
        Invoke("activeFalse", 5.0f);

        isCounting = true; // 카운트다운 시작
        float timeLeft = countdownTime; // 카운트다운 시간 설정

        while (timeLeft > 0)
        {
            countdownText.text = Mathf.CeilToInt(timeLeft).ToString(); // 남은 시간 표시
            countdownImage.fillAmount = (countdownTime - timeLeft) / countdownTime; // 색상 채우기
            yield return new WaitForSeconds(1f); // 1초 대기
            timeLeft--; // 시간 1초 감소
        }

        // 카운트다운이 끝나면 결과 패널 표시
        ResultPanel();
    }

    private void ResultPanel()
    {
        // 게임 오버 UI 비활성화 후 결과 화면 표시
        gameOverPanel.SetActive(false);
        GameManager.Instance.ResultPanel(); // 결과 화면으로 이동
    }

    /*public void GameOverPanelUI()
    {
        gameOverPanel.SetActive(true);
        StartCoroutine(StartCountDown());

        Debug.Log("이어하기 창 활성화");
    }


    private IEnumerator StartCountDown()
    {
        isCounting = true;
        float timeleft = countdowntime;

        while (timeleft > 0)
        {
            countdownText.text = Mathf.CeilToInt(timeleft).ToString();

            countdownImage.fillAmount = (5f - timeleft) / 5f;
            yield return new WaitForSeconds(1f);
            timeleft--;
        }

        ResultPanel();
        Debug.Log("점수 결과 창 활성화 ");

    }


    public void WatchAdtoRevive()
    {
        if (!isCounting)
        {
            isCounting = false;
            StopAllCoroutines();
            gameOverPanel.SetActive(false);
            //RevivePlayer();
        }
    }

    private void ResultPanel()
    {
        gameOverPanel.SetActive(false);
        GameManager.Instance.ResultPanel(); // 5초 후 점수 합산 창으로 이동
    }

    //private void RevivePlayer()
    //{
    //    GameManager.Instance.RevivePlayer(); //  광고 시청 후 플레이어 부활 처리
    //}*/




