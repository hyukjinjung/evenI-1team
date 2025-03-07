using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundManager : MonoBehaviour
{
    TestSoundManager testSoundManager;

    public AudioClip buttonClickSound;

    public Button startGameButton;

    public void PlayButtonSound()
    {
        AudioSource.PlayClipAtPoint(buttonClickSound, transform.position);
    }

    public void Start()
    {
        startGameButton.onClick.AddListener(OnClickStartGameButton);

        Button[] buttons = GetComponentsInChildren<Button>();

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayButtonSound); 
        }
    }

    void OnClickStartGameButton()
    {
        TestSoundManager.Instance.PlayButtonClickGameStart();  
    }
}




