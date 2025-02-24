using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSoundUIManager : MonoBehaviour
{
    private bool isActive = true;
    private bool isSoundActive = true;

    public Button SettingBGMButton;
    public Button SettingSoundEffectButton;
    public Button PauseBGMButton;
    public Button PauseSoundEffectButton;


    public Sprite SettingBGMButtonOnSprite;
    public Sprite SettingBGMButtonOffSprite;

    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        SettingBGMButton.onClick.AddListener(SettingBGMButtonSoundState);
        SettingSoundEffectButton.onClick.AddListener(SettingSoundEffectButtonSoundState);
        PauseBGMButton.onClick.AddListener(PauseBGMButtonSoundState);
        PauseSoundEffectButton.onClick.AddListener(PauseSoundEffectButtonSoundState);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateImage()
    {
        if (isSoundActive)
        {
            SettingBGMButton.image.sprite = SettingBGMButtonOnSprite;
        }
        else
        {
            SettingBGMButton.image.sprite = SettingBGMButtonOffSprite;
        }
    }

    

    public void SettingBGMButtonSoundState()
    {
        isSoundActive = !isSoundActive;  // 家府 惑怕甫 馆傈

        // 家府 惑怕俊 嘎苗 家府 力绢
        if (isSoundActive)
        {
            audioSource.Play();  // 家府 劝己拳
            Debug.Log("家府 劝己拳");
        }
        else
        {
            audioSource.Pause();  // 家府 厚劝己拳
            Debug.Log("家府 厚劝己拳");
        }

        //if (uiManager != null)
            //uiManager.UpdateImage();
    }

    public void SettingSoundEffectButtonSoundState()
    {
        isSoundActive = !isSoundActive;  // 家府 惑怕甫 馆傈

        // 家府 惑怕俊 嘎苗 家府 力绢
        if (isSoundActive)
        {
            audioSource.Play();  // 家府 劝己拳
            Debug.Log("家府 劝己拳");
        }
        else
        {
            audioSource.Pause();  // 家府 厚劝己拳
            Debug.Log("家府 厚劝己拳");
        }

       // if (uiManager != null)
           // uiManager.UpdateImage();
    }
    public void PauseBGMButtonSoundState()
    {
        isSoundActive = !isSoundActive;  // 家府 惑怕甫 馆傈

        // 家府 惑怕俊 嘎苗 家府 力绢
        if (isSoundActive)
        {
            audioSource.Play();  // 家府 劝己拳
            Debug.Log("家府 劝己拳");
        }
        else
        {
            audioSource.Pause();  // 家府 厚劝己拳
            Debug.Log("家府 厚劝己拳");
        }

        //if (uiManager != null)
            //uiManager.UpdateImage();
    }

    public void PauseSoundEffectButtonSoundState()
    {
        isSoundActive = !isSoundActive;  // 家府 惑怕甫 馆傈

        // 家府 惑怕俊 嘎苗 家府 力绢
        if (isSoundActive)
        {
            audioSource.Play();  // 家府 劝己拳
            Debug.Log("家府 劝己拳");
        }
        else
        {
            audioSource.Pause();  // 家府 厚劝己拳
            Debug.Log("家府 厚劝己拳");
        }

        //if (uiManager != null)
           // uiManager.UpdateImage();
    }
}


