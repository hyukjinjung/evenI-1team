using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverSystem : MonoBehaviour
{
    public static FeverSystem Instance { get; private set; }

    [SerializeField] private bool isFeverActive = false;
    public bool IsFeverActive => isFeverActive;

    [Header("Fever Mode Settings")]
    [SerializeField] private float feverDuration = 10;
    [SerializeField] private float RemaningFeverTime = 0;

    [SerializeField] private int CurrentfeverScore = 0;
    public int FeverScore
    {
        get => CurrentfeverScore;
        set => CurrentfeverScore = Mathf.Max(0, value);
    }

    [SerializeField] private int feverScoreLimit = 150;

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

        gameManager.feverBackGroundManager.SetFeverMode(false);

        //StartCoroutine( PreloadFeverAudio());
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
            {FeverScoreType.TestCoin, 150 }
        };
    }


    void Update()
    {
        if (isFeverActive)
        {
            RemaningFeverTime -= Time.deltaTime;

            if (RemaningFeverTime <= 0)
            {
                EndFever();
            }
        }
    }


    public void StartFever()
    {
        if (isFeverActive) return;

        isFeverActive = true;
        RemaningFeverTime = feverDuration;
        CurrentfeverScore = 0;

        StartCoroutine(SmoothToFever());

        Debug.Log("피버 시작");
        OnFeverStart?.Invoke();

        SoundManager.Instance.ChangeBackGroundMusic(2);

    }


    public void EndFever()
    {
        if (!isFeverActive) return;

        isFeverActive = false;

        gameManager.PlayerAnimationController.SetFeverMode(false);
        gameManager.feverBackGroundManager.SetFeverMode(false);

        gameManager.PlayerMovement.feverFallCount = 0;

        Debug.Log("피버 종료");
        OnFeverEnd?.Invoke();

        SoundManager.Instance.ChangeBackGroundMusic(1);

    }


    public void AddFeverScore(FeverScoreType feverType)
    {
        if (isFeverActive) return;

        if (feverScoresValues.ContainsKey(feverType))
        {
            CurrentfeverScore += feverScoresValues[feverType];
        }



        Debug.Log($"현재 피버 점수 {CurrentfeverScore}");

        if (CurrentfeverScore >= feverScoreLimit && !isFeverActive)
        {
            StartFever();
            SoundManager.Instance.PlayClip(21);
        }
    }


    private IEnumerator SmoothToFever()
    {
        gameManager.feverBackGroundManager.SetFeverMode(true);
        yield return new WaitForSeconds(0.1f);

        gameManager.PlayerAnimationController.SetFeverMode(true);
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator PreloadFeverAudio()
    {
        yield return null;

        AudioSource temp = gameObject.AddComponent<AudioSource>();
        temp.clip = SoundManager.Instance.musicClip[2];
        temp.Play();

        yield return new WaitForSeconds(0.1f);

        Destroy(temp);
    }
}
