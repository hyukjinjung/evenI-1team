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

        // ���� ������ Ŭ���ϸ�, �ֿܼ� ���� ��ư: ���ʰ� ���� ��ư: �������� ���� ��µ�(����)
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
        //Debug.Log($"���� ��ư {(JumpLeft ? "����" : "������")}"); // �ߺ� ȣ��Ǵ��� Ȯ��

        CallJumpEvent(JumpLeft);
    }

    public void OnAttackButtonClick(bool AttackLeft)
    {
        CallAttackEvent(AttackLeft);
    }

}
