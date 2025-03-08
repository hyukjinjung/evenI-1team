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

    //[SerializeField] private bool isPlayKeyboard;

    void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null) return;
        
        uiManager = gameManager.uiManager;
        if (uiManager == null) return;
        
        AssignButtons();
    }

    public void Update()
    {
        //if (isPlayKeyboard)
        //{
        //    if(Input.GetKeyDown(KeyCode.A)) CallJumpEvent(true);
        //    if(Input.GetKeyDown(KeyCode.D)) CallJumpEvent(false);
        //}
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
        Debug.Log($"입력 상태 변경 {(active ? "활성화" : "비활성화")}");
    }


    public bool IsInputActive()
    { 
        return isInputActive;
    }


}
