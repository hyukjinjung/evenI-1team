using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverSystem : MonoBehaviour
{
    public static FeverSystem Instance { get; private set; }

    [SerializeField] private bool isFeverActive = false;
    public bool IsFeverActive => isFeverActive;

    [SerializeField] private float feverDuration = 10;
    [SerializeField] private float feverTimer = 0;
    [SerializeField] private int feverScore = 0;
    public int FeverScore
    {
        get => feverScore;
        set => feverScore = Mathf.Max(0, value);
    }

    public event Action OnFeverStart;
    public event Action OnFeverEnd;

    private Dictionary<FeverScoreType, int> feverScoresValues;

    GameManager gameManager;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeFeverScoreValues();
    }

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void InitializeFeverScoreValues()
    {
        feverScoresValues = new Dictionary<FeverScoreType, int>
        {
            {FeverScoreType.Movement, 1 },
            {FeverScoreType.FeverCoin, 10 },
            {FeverScoreType.MonsterKill, 1 },
            {FeverScoreType.ItemUse, 5 },
            {FeverScoreType.FeverReductionCoin, -10 },
            { FeverScoreType.TestCoin, 150 }
        };
    }


    void Update()
    {
        if (isFeverActive)
        {
            feverTimer -= Time.deltaTime;

            if (feverTimer <= 0)
            {
                EndFever();
            }
        }
    }


    public void StartFever()
    {
        if (isFeverActive) return;

        isFeverActive = true;
        feverTimer = feverDuration;
        feverScore = 0;

        gameManager.PlayerAnimationController.SetFeverMode(true);
        gameManager.feverBackGroundManager.SetFeverMode(true);

        Debug.Log("피버 시작");
        OnFeverStart?.Invoke();

        SoundManager.Instance.ChangeBackGroundMusic(2);  
    }


    public void EndFever()
    {
        if (!isFeverActive) return;

        isFeverActive = false;
        //feverScore = 0;

        gameManager.PlayerAnimationController.SetFeverMode(false);
        gameManager.feverBackGroundManager.SetFeverMode(false);

        gameManager.PlayerMovement.feverFallCount = 0;

        Debug.Log("피버 종료");
        OnFeverEnd?.Invoke();

        SoundManager.Instance.ChangeBackGroundMusic(1);
    }


    public void AddFeverScore(FeverScoreType feverType)
    {
        if (feverScoresValues.ContainsKey(feverType))
        {
            feverScore += feverScoresValues[feverType];
        }

        Debug.Log($"현재 피버 점수 {feverScore}");

        if (feverScore >= 50 && !isFeverActive)
        {
            StartFever();
        }
    }
}
