using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundManager : MonoBehaviour
{
    public AudioClip buttonClickSound;

    public Button startGameButton;

    public void PlayButtonSound()
    {
        AudioSource.PlayClipAtPoint(buttonClickSound, transform.position);
    }

    public void Start()
    {
        startGameButton.onClick.AddListener(OnClickStartGameButton);
    }

    void OnClickStartGameButton()
    {
        if (TestSoundManager.Instance != null)
        {
            TestSoundManager.Instance.PlayButtonClickGameStart();
        }
        GameManager.Instance.StartGame();
    }
}




