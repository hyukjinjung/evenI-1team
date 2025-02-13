using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputController : MonoBehaviour
{
    public event Action<Vector2> OnJumpEvent;

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

    public void CallJumpEvent(Vector2 direction)
    {
        OnJumpEvent?.Invoke(direction);
    }

    public void OnLeftButtonClick()
    {

        CallJumpEvent(new Vector2(-1f, 0.5f)); 
    }

    public void OnRightButtonClick()
    {
        CallJumpEvent(new Vector2(1f, 0.5f));
    }
}
