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

    public bool isLeft = true;



    void Start()
    {

        // 왼쪽 점프를 클릭하면, 콘솔에 점프 버튼: 왼쪽과 점프 버튼: 오른쪽이 같이 출력됨(오류)
        LeftButton.onClick.AddListener(() => OnJumpButtonClick(true));
        RightButton.onClick.AddListener(() => OnJumpButtonClick(false));
        AttackButton.onClick.AddListener(() => OnAttackButtonClick(isLeft));
    }



    public void CallJumpEvent(bool isLeft)
    {
        OnJumpEvent?.Invoke(isLeft);
    }
    public void CallAttackEvent(bool isLeft)
    {
        OnAttackEvent?.Invoke(isLeft);
    }


    public void OnJumpButtonClick(bool JumpLeft)
    {
        //Debug.Log($"점프 버튼 {(JumpLeft ? "왼쪽" : "오른쪽")}"); // 중복 호출되는지 확인

        CallJumpEvent(JumpLeft);
    }

    public void OnAttackButtonClick(bool AttackLeft)
    {
        CallAttackEvent(AttackLeft);
    }

}
