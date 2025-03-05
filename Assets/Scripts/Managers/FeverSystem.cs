using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverSystem : MonoBehaviour
{
    public bool isFeverActive = false;
    public float feverDuration = 10;
    private float feverTimer = 0;
    private int feverScore = 0;

    public event Action OnFeverStart;
    public event Action OnFeverEnd;

    private Dictionary<FeverScoreType, int> feverScoresValues;


    private void Awake()
    {
        InitializeFeverScoreValues();
    }

    void Start()
    {

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
            { FeverScoreType.TestCoin, 300 }

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

        Debug.Log("피버 시작");
        OnFeverStart?.Invoke();
    }


    public void EndFever()
    {
        if (!isFeverActive) return;

        isFeverActive = false;

        GameManager.Instance.PlayerAnimationController.SetFeverMode(false);


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

        if (feverScore >= 300 && !isFeverActive)
        {
            StartFever();
        }
    }
}
