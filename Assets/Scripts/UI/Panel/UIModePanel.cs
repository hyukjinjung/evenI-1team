using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIModePanel : MonoBehaviour
{
    [Header("��� ��ư")]
    [SerializeField] private Button infiniteModeButton;
    [SerializeField] private Button storyModeButton;
    [SerializeField] private Button challengeModeButton;

    [Header("���� ��� ��ư")]
    [SerializeField] private GameObject challengeSubPanel;
    [SerializeField] private Button speedModeButton;
    [SerializeField] private Button onOffModeButton;
    [SerializeField] private Button monsterModeButton;

    [Header("��Ÿ ��ư")]
    [SerializeField] private Button backButton;

    private UIManager uiManager;
    private GameModeManager gameModeManager;

    public void Initialize(UIManager manager)
    {
        uiManager = manager;
        gameModeManager = GameModeManager.Instance;

        // ��ư �̺�Ʈ ���
        infiniteModeButton.onClick.AddListener(OnInfiniteModeSelected);
        storyModeButton.onClick.AddListener(OnStoryModeSelected);
        challengeModeButton.onClick.AddListener(OnChallengeModeSelected);

        speedModeButton.onClick.AddListener(() => OnChallengeModeTypeSelected(ChallengeType.Speed));
        onOffModeButton.onClick.AddListener(() => OnChallengeModeTypeSelected(ChallengeType.OnOff));
        monsterModeButton.onClick.AddListener(() => OnChallengeModeTypeSelected(ChallengeType.Monster));

        backButton.onClick.AddListener(OnBackButtonClicked);

        // �ʱ� ����
        challengeSubPanel.SetActive(false);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    private void OnInfiniteModeSelected()
    {
        gameModeManager.SetGameMode(GameMode.Infinite);
        uiManager.StartGame();
    }

    private void OnStoryModeSelected()
    {
        gameModeManager.SetGameMode(GameMode.Story);
        uiManager.StartGame();
    }

    private void OnChallengeModeSelected()
    {
        // ���� ��� ���� �г� ǥ��
        challengeSubPanel.SetActive(true);
    }

    private void OnChallengeModeTypeSelected(ChallengeType type)
    {
        gameModeManager.SetGameMode(GameMode.Challenge, type);
        uiManager.StartGame();
    }

    private void OnBackButtonClicked()
    {
        if (challengeSubPanel.activeSelf)
        {
            // ���� ��� ���� �г��� ���������� �ݱ�
            challengeSubPanel.SetActive(false);
        }
        else
        {
            // ���� ȭ������ ���ư���
            uiManager.UIStartPanel.SetActive(true);
            SetActive(false);
        }
    }
}