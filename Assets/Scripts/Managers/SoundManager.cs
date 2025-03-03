using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public bool isSoundActive = true; // �ʱ� �Ҹ��� Ȱ��ȭ
    //bool pressed = false; // ���� ����

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
        isSoundActive = !isSoundActive;  // �Ҹ� ���¸� ����

        // �Ҹ� ���¿� ���� �Ҹ� ����
        if (isSoundActive)
        {
            audioSource.Play();  // �Ҹ� Ȱ��ȭ
            Debug.Log("�Ҹ� Ȱ��ȭ");

            SettingBGMButton.image.sprite = SettingBGMButtonOnSprite;
        }

        else
        {
            audioSource.Pause();  // �Ҹ� ��Ȱ��ȭ
            Debug.Log("�Ҹ� ��Ȱ��ȭ");

            SettingBGMButton.image.sprite = SettingBGMButtonOffSprite;

        }


    }

    public void SettingSoundEffectButtonSoundState()
    {
        isSoundActive = !isSoundActive;  // �Ҹ� ���¸� ����

        // �Ҹ� ���¿� ���� �Ҹ� ����
        if (isSoundActive)
        {
            audioSource.Play();  // �Ҹ� Ȱ��ȭ
            Debug.Log("�Ҹ� Ȱ��ȭ");

            SettingSoundEffectButton.image.sprite = SettingSoundEffectButtonnOnSprite;
        }

        else
        {
            audioSource.Pause();  // �Ҹ� ��Ȱ��ȭ
            Debug.Log("�Ҹ� ��Ȱ��ȭ");

            SettingSoundEffectButton.image.sprite = SettingSoundEffectButtonOffSprite;

        }
    }


    public void PauseBGMButtonSoundState()
    {
        isSoundActive = !isSoundActive;  // �Ҹ� ���¸� ����

        // �Ҹ� ���¿� ���� �Ҹ� ����
        if (isSoundActive)
        {
            audioSource.Play();  // �Ҹ� Ȱ��ȭ
            Debug.Log("�Ҹ� Ȱ��ȭ");

            PauseBGMButton.image.sprite = PauseBGMButtonOnSprite;
        }
        else
        {
            audioSource.Pause();  // �Ҹ� ��Ȱ��ȭ
            Debug.Log("�Ҹ� ��Ȱ��ȭ");

            PauseBGMButton.image.sprite = PauseBGMButtonOffSprite;
        }


    }

    public void PauseSoundEffectButtonSoundState()
    {
        isSoundActive = !isSoundActive;  // �Ҹ� ���¸� ����

        // �Ҹ� ���¿� ���� �Ҹ� ����
        if (isSoundActive)
        {
            audioSource.Play();  // �Ҹ� Ȱ��ȭ
            Debug.Log("�Ҹ� Ȱ��ȭ");

            PauseSoundEffectButton.image.sprite = PauseSoundEffectButtonOnSprite;
        }
        else
        {
            audioSource.Pause();  // �Ҹ� ��Ȱ��ȭ
            Debug.Log("�Ҹ� ��Ȱ��ȭ");

            PauseSoundEffectButton.image.sprite = PauseSoundEffectButtonOffSprite;
        }
    }
}






