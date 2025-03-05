using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverSystem : MonoBehaviour
{
    public bool isFeverActive = false;
    public float feverDuration = 10;
    private float feverTimer = 0f;

    public event Action OnFeverStart;
    public event Action OnFeverEnd;



    void Start()
    {
        
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
        isFeverActive = true;
        feverTimer = feverDuration;

        Debug.Log("피버 시작");
        OnFeverStart?.Invoke();
    }


    public void EndFever()
    {
        isFeverActive = false;

        Debug.Log("피버 종료");
        OnFeverEnd?.Invoke();
    }
}
