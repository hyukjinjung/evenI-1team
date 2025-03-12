using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    
    [SerializeField] private float startCountdownTime = 3f;

    public Image image;
    

    void Start()
    {
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        float timeRemaining = startCountdownTime;
        
        while (timeRemaining > 0)
        {
            countdownText.text = Mathf.CeilToInt(timeRemaining).ToString();
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
        }
        
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        
        gameObject.SetActive(false);
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
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

        Debug.Log("�̾��ϱ� â Ȱ��ȭ");
    }

    private IEnumerator StartCountDown()
    {
        Invoke("activeFalse", 5.0f);

        isCounting = true; // ī��Ʈ�ٿ� ����
        float timeLeft = countdownTime; // ī��Ʈ�ٿ� �ð� ����

        while (timeLeft > 0)
        {
            countdownText.text = Mathf.CeilToInt(timeLeft).ToString(); // ���� �ð� ǥ��
            countdownImage.fillAmount = (countdownTime - timeLeft) / countdownTime; // ���� ä���
            yield return new WaitForSeconds(1f); // 1�� ���
            timeLeft--; // �ð� 1�� ����
        }

        // ī��Ʈ�ٿ��� ������ ��� �г� ǥ��
        ResultPanel();
    }

    private void ResultPanel()
    {
        // ���� ���� UI ��Ȱ��ȭ �� ��� ȭ�� ǥ��
        gameOverPanel.SetActive(false);
        GameManager.Instance.ResultPanel(); // ��� ȭ������ �̵�
    }

    /*public void GameOverPanelUI()
    {
        gameOverPanel.SetActive(true);
        StartCoroutine(StartCountDown());

        Debug.Log("�̾��ϱ� â Ȱ��ȭ");
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
        Debug.Log("���� ��� â Ȱ��ȭ ");

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
        GameManager.Instance.ResultPanel(); // 5�� �� ���� �ջ� â���� �̵�
    }

    //private void RevivePlayer()
    //{
    //    GameManager.Instance.RevivePlayer(); //  ���� ��û �� �÷��̾� ��Ȱ ó��
    //}*/




