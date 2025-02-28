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

    void Start()
    {
        gameManager = GameManager.Instance; // GameManager 가져오기
        if (gameManager == null) return;
        
        uiManager = gameManager.uiManager;
        if (uiManager == null) return;

        uiManager.playerInputController = this;
        
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
        OnJumpEvent?.Invoke(isLeft);
    }
    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }
    

}
