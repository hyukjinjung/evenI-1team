using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputController : MonoBehaviour
{
    public event Action<bool> OnJumpEvent;

    public Button leftButton;
    public Button rightButton;

    // Start is called before the first frame update
    void Start()
    {
        leftButton.onClick.AddListener(() => OnLeftButtonClick());
        rightButton.onClick.AddListener(() => OnRightButtonClick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallJumpEvent(bool isleft)
    {
        OnJumpEvent?.Invoke(isleft);
    }

    public void OnLeftButtonClick()
    {

        CallJumpEvent(true); 
    }

    public void OnRightButtonClick()
    {
        CallJumpEvent(false);
    }
}
