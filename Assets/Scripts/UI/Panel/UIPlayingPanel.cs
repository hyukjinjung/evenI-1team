using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIPlayingPanel : UIPanel
{
     [SerializeField] Button AttackButton;
     [SerializeField] Button LeftButton;
     [SerializeField] Button RightButton;
     [SerializeField] Button PauseButton;
    
    public event Action OnClickedAttackEvent;
    public event Action<bool> OnClickedMoveEvent;   // true -> Left
    
    public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);
        PauseButton.onClick.AddListener(OnPauseButtonClicked);
        AttackButton.onClick.AddListener(OnClickedAttackButton);
        LeftButton.onClick.AddListener(OnClickedLeftButton);
        RightButton.onClick.AddListener(OnClickedRightButton);
    }

    void OnPauseButtonClicked()
    {
        uiManager.OnPauseButtonClicked();
    }
    
    void OnClickedAttackButton()
    {
        OnClickedAttackEvent?.Invoke();
    }
    
    void OnClickedLeftButton()
    {
        OnClickedMoveEvent?.Invoke(true);
    }
    
    void OnClickedRightButton()
    {
        OnClickedMoveEvent?.Invoke(false);
    }
    
}
