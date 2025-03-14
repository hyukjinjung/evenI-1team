using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIModePanel : MonoBehaviour
{
    [Header("모드 버튼")]
    [SerializeField] private Button infiniteModeButton;
    [SerializeField] private Button storyModeButton;
    [SerializeField] private Button challengeModeButton;

    [Header("도전 모드 버튼")]
    [SerializeField] private GameObject challengeSubPanel;
    [SerializeField] private Button speedModeButton;
    [SerializeField] private Button onOffModeButton;
    [SerializeField] private Button monsterModeButton;

    [Header("기타 버튼")]
    [SerializeField] private Button backButton;

    private UIManager uiManager;
    private GameModeManager gameModeManager;

    public void Initialize(UIManager manager)
    {
        uiManager = manager;
        gameModeManager = GameModeManager.Instance;

        // 버튼 이벤트 등록
        infiniteModeButton.onClick.AddListener(OnInfiniteModeSelected);
        storyModeButton.onClick.AddListener(OnStoryModeSelected);
        challengeModeButton.onClick.AddListener(OnChallengeModeSelected);

        speedModeButton.onClick.AddListener(() => OnChallengeModeTypeSelected(ChallengeType.Speed));
        onOffModeButton.onClick.AddListener(() => OnChallengeModeTypeSelected(ChallengeType.OnOff));
        monsterModeButton.onClick.AddListener(() => OnChallengeModeTypeSelected(ChallengeType.Monster));

        backButton.onClick.AddListener(OnBackButtonClicked);

        // 초기 설정
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
        // 도전 모드 서브 패널 표시
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
            // 도전 모드 서브 패널이 열려있으면 닫기
            challengeSubPanel.SetActive(false);
        }
        else
        {
            // 메인 화면으로 돌아가기
            uiManager.UIStartPanel.SetActive(true);
            SetActive(false);
        }
    }
}