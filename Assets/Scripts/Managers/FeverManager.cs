using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Unity.VisualScripting;

public class FeverManager : MonoBehaviour
{
    public static FeverManager Instance;

    [Header("Fever Settings")]

    public float feverDuration = 5f;
    public int feverScore = 0;
    private bool isFeverActive = false;


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
    }


    void Start()
    {
        Variables.Application.Set("FeverActive", false);
        Variables.Application.Set("FeverScore", 0);
    }

    void Update()
    {
        
    }


    public void AddFeverScore(int amount)
    {
        feverScore += amount;
        Variables.Application.Set("FeverScore", feverScore);

        Debug.Log($"현재 피버 점수 {feverScore}");

        if (feverScore >= 100 && !isFeverActive)
        {
            ActivateFever();
        }
    }


    public void ActivateFever()
    {
        if (!isFeverActive)
        {
            isFeverActive = true;

            Variables.Application.Set("FeverActive", true);

            CustomEvent.Trigger(gameObject, "ActivateFever");

            StartCoroutine(EndFeverAfterDuration());
        }
    }


    private IEnumerator EndFeverAfterDuration()
    {
        yield return new WaitForSeconds(feverDuration);

        isFeverActive = false ;
        Variables.Application.Set("FeverActive", false );

        CustomEvent.Trigger(gameObject, "EndFever");

    }


    public bool IsFeverActive()
    {
        return isFeverActive;
    }


    public int GetFeverScore()
    {
        return feverScore;
    }
}
