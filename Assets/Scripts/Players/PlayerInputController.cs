using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputController : MonoBehaviour
{
    public event Action<bool> OnJumpEvent;
    public event Action OnAttackEvent;
    
    private GameManager gameManager;
    private UIManager uiManager;

    private bool isInputActive = true;


    void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null) return;
        
        uiManager = gameManager.uiManager;
        if (uiManager == null) return;
        
        AssignButtons();
    }

    public void AssignButtons()
    {
        UIPlayingPanel uiPlayingPanel = uiManager.UIPlayingPanel;
        uiPlayingPanel.OnClickedMoveEvent += CallJumpEvent;
        uiPlayingPanel.OnClickedAttackEvent += CallAttackEvent;
    }

    public void CallJumpEvent(bool isLeft)
    {
        if (!isInputActive) return;

        OnJumpEvent?.Invoke(isLeft);
    }

    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }

    public void SetInputActive(bool active)
    {
        isInputActive = active;
        Debug.Log($"�Է� ���� ���� {(active ? "Ȱ��ȭ" : "��Ȱ��ȭ")}");
    }


    public bool IsInputActive()
    { 
        return isInputActive;
    }


}
