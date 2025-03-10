using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class UIPlayingPanel : UIPanel
{
     [SerializeField] Button AttackButton;
     [SerializeField] Button LeftButton;
     [SerializeField] Button RightButton;
     [SerializeField] Button PauseButton;
    
     [SerializeField] TextMeshProUGUI scoreText;
     
     
    public event Action OnClickedAttackEvent;
    public event Action<bool> OnClickedMoveEvent;   // true -> Left

    SoundManager soundManager;

    public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);
        PauseButton.onClick.AddListener(OnPauseButtonClicked);
        AttackButton.onClick.AddListener(OnClickedAttackButton);
        LeftButton.onClick.AddListener(OnClickedLeftButton);
        RightButton.onClick.AddListener(OnClickedRightButton);
        soundManager = SoundManager.Instance;
    }

    

    void OnPauseButtonClicked()
    {
        uiManager.OnPauseButtonClicked();
        soundManager.PlayClip(22);
    }
    
    void OnClickedAttackButton()
    {
        OnClickedAttackEvent?.Invoke();
        soundManager.PlayClip(0);
    }
    
    void OnClickedLeftButton()
    {
        OnClickedMoveEvent?.Invoke(true);
        soundManager.PlayClip(1);
    }
    
    void OnClickedRightButton()
    {
        OnClickedMoveEvent?.Invoke(false);
        soundManager.PlayClip(1);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

}
