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

    private GameManager gameManager;


    void Start()
    {
        gameManager = GameManager.Instance; // GameManager 가져오기

        if (gameManager == null)
        {
            return;
        }

        if (gameManager.uiManager == null)
        {
            return;
        }

        gameManager.uiManager.playerInputController = this;
        AssignButtons();



    }

    public void AssignButtons()
    {
        UIManager uIManager = GameManager.Instance.uiManager;
        if (uIManager == null) return;

        LeftButton = uIManager.LeftButton;
        RightButton = uIManager.RightButton;
        AttackButton = uIManager.AttackButton;


        if (LeftButton != null && RightButton != null && AttackButton != null)
        {
            LeftButton.onClick.AddListener(() => OnJumpButtonClick(true));
            RightButton.onClick.AddListener(() => OnJumpButtonClick(false));
            AttackButton.onClick.AddListener(() => OnAttackButtonClick(isLeft));
        }
    }


    public void AssignPlayerMovement()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

        if (playerMovement != null)
        {
            // 변신 후 기존 리스너를 제거하고 다시 등록
            OnJumpEvent -= playerMovement.Jump;
            OnJumpEvent += playerMovement.Jump;
        }
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
        CallJumpEvent(JumpLeft);
    }

    public void OnAttackButtonClick(bool AttackLeft)
    {
        CallAttackEvent(AttackLeft);
    }

}
