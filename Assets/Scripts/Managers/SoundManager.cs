using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public bool isSoundActive = true; // 초기 소리는 활성화
    //bool pressed = false; // 눌린 상태

    public UIManager uiManager;

    public Button SettingBGMButton;
    public Button SettingSoundEffectButton;
    public Button PauseBGMButton;
    public Button PauseSoundEffectButton;

    public Sprite SettingBGMButtonOnSprite;
    public Sprite SettingBGMButtonOffSprite;
    public Sprite SettingSoundEffectButtonnOnSprite;
    public Sprite SettingSoundEffectButtonOffSprite;
    public Sprite PauseBGMButtonOnSprite;
    public Sprite PauseBGMButtonOffSprite;
    public Sprite PauseSoundEffectButtonOnSprite;
    public Sprite PauseSoundEffectButtonOffSprite;

    public void Start()
    {
        SettingBGMButton.onClick.AddListener(SettingBGMButtonSoundState);
        SettingSoundEffectButton.onClick.AddListener(SettingSoundEffectButtonSoundState);
        PauseBGMButton.onClick.AddListener(PauseBGMButtonSoundState);
        PauseSoundEffectButton.onClick.AddListener(PauseSoundEffectButtonSoundState);

        uiManager = GetComponent<UIManager>();
    }

    public void SettingBGMButtonSoundState()
    {
        isSoundActive = !isSoundActive;  // 소리 상태를 반전

        // 소리 상태에 맞춰 소리 제어
        if (isSoundActive)
        {
            audioSource.Play();  // 소리 활성화
            Debug.Log("소리 활성화");

            SettingBGMButton.image.sprite = SettingBGMButtonOnSprite;
        }

        else
        {
            audioSource.Pause();  // 소리 비활성화
            Debug.Log("소리 비활성화");

            SettingBGMButton.image.sprite = SettingBGMButtonOffSprite;

        }


    }

    public void SettingSoundEffectButtonSoundState()
    {
        isSoundActive = !isSoundActive;  // 소리 상태를 반전

        // 소리 상태에 맞춰 소리 제어
        if (isSoundActive)
        {
            audioSource.Play();  // 소리 활성화
            Debug.Log("소리 활성화");

            SettingSoundEffectButton.image.sprite = SettingSoundEffectButtonnOnSprite;
        }

        else
        {
            audioSource.Pause();  // 소리 비활성화
            Debug.Log("소리 비활성화");

            SettingSoundEffectButton.image.sprite = SettingSoundEffectButtonOffSprite;

        }
    }


    public void PauseBGMButtonSoundState()
    {
        isSoundActive = !isSoundActive;  // 소리 상태를 반전

        // 소리 상태에 맞춰 소리 제어
        if (isSoundActive)
        {
            audioSource.Play();  // 소리 활성화
            Debug.Log("소리 활성화");

            PauseBGMButton.image.sprite = PauseBGMButtonOnSprite;
        }
        else
        {
            audioSource.Pause();  // 소리 비활성화
            Debug.Log("소리 비활성화");

            PauseBGMButton.image.sprite = PauseBGMButtonOffSprite;
        }


    }

    public void PauseSoundEffectButtonSoundState()
    {
        isSoundActive = !isSoundActive;  // 소리 상태를 반전

        // 소리 상태에 맞춰 소리 제어
        if (isSoundActive)
        {
            audioSource.Play();  // 소리 활성화
            Debug.Log("소리 활성화");

            PauseSoundEffectButton.image.sprite = PauseSoundEffectButtonOnSprite;
        }
        else
        {
            audioSource.Pause();  // 소리 비활성화
            Debug.Log("소리 비활성화");

            PauseSoundEffectButton.image.sprite = PauseSoundEffectButtonOffSprite;
        }
    }
}






