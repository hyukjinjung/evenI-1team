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

    SoundMnager soundMnager;

    public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);
        PauseButton.onClick.AddListener(OnPauseButtonClicked);
        AttackButton.onClick.AddListener(OnClickedAttackButton);
        LeftButton.onClick.AddListener(OnClickedLeftButton);
        RightButton.onClick.AddListener(OnClickedRightButton);
        soundMnager = SoundMnager.Instance;
    }

    

    void OnPauseButtonClicked()
    {
        uiManager.OnPauseButtonClicked();
        soundMnager.PlayClip(1);
    }
    
    void OnClickedAttackButton()
    {
        OnClickedAttackEvent?.Invoke();
        soundMnager.PlayClip(2);
    }
    
    void OnClickedLeftButton()
    {
        OnClickedMoveEvent?.Invoke(true);
        soundMnager.PlayClip(3);
    }
    
    void OnClickedRightButton()
    {
        OnClickedMoveEvent?.Invoke(false);
        soundMnager.PlayClip(4);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

}
