using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public Animator playerAnimator;
    public Button startButton;
    public GameObject homePanel; // Ȩ ȭ�� �г�
    public GameObject gamePanel; // ���� ȭ�� �г�

    private void Start()
    {
        startButton.onClick.AddListener(OnGameStartClicked);

        // �ʱ� �г� ���� ���� (Ȩ ȭ��)
        homePanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    void OnGameStartClicked()
    {
        startButton.interactable = false; // ��ư �ߺ� �Է� ����
        StartCoroutine(PlayTurnAround());
    }

    IEnumerator PlayTurnAround()
    {
        playerAnimator.SetTrigger("GameStart");

        // �ִϸ��̼� ���̸�ŭ ���
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;
        yield return new WaitForSeconds(animationDuration);

        // �г� Ȱ��ȭ/ ��Ȱ��ȭ
        homePanel.SetActive(false);
        gamePanel.SetActive(true);
    }
}
