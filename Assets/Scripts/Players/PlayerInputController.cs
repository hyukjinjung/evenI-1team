using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputController : MonoBehaviour
{
    public event Action<bool> OnJumpEvent;
    public event Action<bool> OnAttackEvent;

    public Button LeftButton;
    public Button RightButton;
    public Button AttackButton;

    // Start is called before the first frame update
    void Start()
    {
        LeftButton.onClick.AddListener(() => OnLeftButtonClick());
        RightButton.onClick.AddListener(() => OnRightButtonClick());
        AttackButton.onClick.AddListener(() => OnAttackButtonClick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallJumpEvent(bool isLeft)
    {
        OnJumpEvent?.Invoke(isLeft);
    }
    public void CallAttackEvent(bool isLeft)
    {
        OnAttackEvent?.Invoke(isLeft);
    }



    public void OnLeftButtonClick()
    {
        //Debug.Log("left Clicked");
        CallJumpEvent(true); 
    }

    public void OnRightButtonClick()
    {
        //Debug.Log("right Clicked");
        CallJumpEvent(false);
    }

    public void OnAttackButtonClick()
    {
        CallAttackEvent(false);
    }

}
