using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverSystem : MonoBehaviour
{
    public static FeverSystem Instance { get; private set; }

    public bool isFeverActive = false;
    public float feverDuration = 10;
    private float feverTimer = 0;
    public int feverScore = 0;

    public event Action OnFeverStart;
    public event Action OnFeverEnd;

    private Dictionary<FeverScoreType, int> feverScoresValues;
    [SerializeField] private int feverStartScore = 150; // 피버 시작 점수 설정

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
    }

    private void InitializeFeverScoreValues()
    {
        feverScoresValues = new Dictionary<FeverScoreType, int>
        {
            { FeverScoreType.Movement, 1 },
            { FeverScoreType.FeverCoin, 10 },
            { FeverScoreType.MonsterKill, 1 },
            { FeverScoreType.ItemUse, 5 },
            { FeverScoreType.FeverReductionCoin, -10 },
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

        GameManager.Instance.PlayerAnimationController.SetFeverMode(true);
        GameManager.Instance.feverBackGroundManager.SetFeverMode(true);

        Debug.Log("피버 시작");
        OnFeverStart?.Invoke();
    }

    public void EndFever()
    {
        if (!isFeverActive) return;

        isFeverActive = false;

        feverScore = 0;
        Debug.Log($"피버 지속시간 종료. 피버 점수 {feverScore}");

        GameManager.Instance.PlayerAnimationController.SetFeverMode(false);
        GameManager.Instance.feverBackGroundManager.SetFeverMode(false);

        Debug.Log("피버 종료");
        OnFeverEnd?.Invoke();
    }

    public void AddFeverScore(FeverScoreType feverType)
    {
        if (feverScoresValues.ContainsKey(feverType))
        {
            feverScore += feverScoresValues[feverType];
        }

        Debug.Log($"현재 피버 점수 {feverScore}");

        //피버 시작 점수 변수화
        if (feverScore >= feverStartScore && !isFeverActive)
        {
            StartFever();
        }
    }

    //public static FeverSystem Instance { get; private set; }

    //public bool isFeverActive = false;
    //public float feverDuration = 10;
    //private float feverTimer = 0;
    //public int feverScore = 0;

    //public event Action OnFeverStart;
    //public event Action OnFeverEnd;

    //private Dictionary<FeverScoreType, int> feverScoresValues;


    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }

    //    InitializeFeverScoreValues();
    //}

    //void Start()
    //{

    //}

    //private void InitializeFeverScoreValues()
    //{
    //    feverScoresValues = new Dictionary<FeverScoreType, int>
    //    {
    //        {FeverScoreType.Movement, 1 },
    //        {FeverScoreType.FeverCoin, 10 },
    //        {FeverScoreType.MonsterKill, 1 },
    //        {FeverScoreType.ItemUse, 5 },
    //        {FeverScoreType.FeverReductionCoin, -10 },
    //        { FeverScoreType.TestCoin, 150 }

    //    };
    //}


    //void Update()
    //{
    //    if (isFeverActive)
    //    {
    //        feverTimer -= Time.deltaTime;

    //        if (feverTimer <= 0)
    //        {
    //            EndFever();
    //        }
    //    }
    //}


    //public void StartFever()
    //{
    //    if (isFeverActive) return;

    //    isFeverActive = true;
    //    feverTimer = feverDuration;
    //    feverScore = 0;

    //    GameManager.Instance.PlayerAnimationController.SetFeverMode(true);
    //    GameManager.Instance.feverBackGroundManager.SetFeverMode(true);

    //    Debug.Log("피버 시작");
    //    OnFeverStart?.Invoke();
    //}


    //public void EndFever()
    //{
    //    if (!isFeverActive) return;

    //    isFeverActive = false;

    //    feverScore = 0;
    //    Debug.Log($"피버 지속시간 종료. 피버 점수 {feverScore}");

    //    GameManager.Instance.PlayerAnimationController.SetFeverMode(false);
    //    GameManager.Instance.feverBackGroundManager.SetFeverMode(false);

    //    Debug.Log("피버 종료");
    //    OnFeverEnd?.Invoke();
    //}


    //public void AddFeverScore(FeverScoreType feverType)
    //{
    //    if (feverScoresValues.ContainsKey(feverType))
    //    {
    //        feverScore += feverScoresValues[feverType];
    //    }

    //    Debug.Log($"현재 피버 점수 {feverScore}");

    //    if (feverScore >= 150 && !isFeverActive)
    //    {
    //        StartFever();
    //    }
    //}
}
