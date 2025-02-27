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
        isSoundActive = !isSoundActive;  // �Ҹ� ���¸� ����

        // �Ҹ� ���¿� ���� �Ҹ� ����
        if (isSoundActive)
        {
            audioSource.Play();  // �Ҹ� Ȱ��ȭ
            Debug.Log("�Ҹ� Ȱ��ȭ");
        }
        else
        {
            audioSource.Pause();  // �Ҹ� ��Ȱ��ȭ
            Debug.Log("�Ҹ� ��Ȱ��ȭ");
        }

        //if (uiManager != null)
            //uiManager.UpdateImage();
    }

    public void SettingSoundEffectButtonSoundState()
    {
        isSoundActive = !isSoundActive;  // �Ҹ� ���¸� ����

        // �Ҹ� ���¿� ���� �Ҹ� ����
        if (isSoundActive)
        {
            audioSource.Play();  // �Ҹ� Ȱ��ȭ
            Debug.Log("�Ҹ� Ȱ��ȭ");
        }
        else
        {
            audioSource.Pause();  // �Ҹ� ��Ȱ��ȭ
            Debug.Log("�Ҹ� ��Ȱ��ȭ");
        }

       // if (uiManager != null)
           // uiManager.UpdateImage();
    }
    public void PauseBGMButtonSoundState()
    {
        isSoundActive = !isSoundActive;  // �Ҹ� ���¸� ����

        // �Ҹ� ���¿� ���� �Ҹ� ����
        if (isSoundActive)
        {
            audioSource.Play();  // �Ҹ� Ȱ��ȭ
            Debug.Log("�Ҹ� Ȱ��ȭ");
        }
        else
        {
            audioSource.Pause();  // �Ҹ� ��Ȱ��ȭ
            Debug.Log("�Ҹ� ��Ȱ��ȭ");
        }

        //if (uiManager != null)
            //uiManager.UpdateImage();
    }

    public void PauseSoundEffectButtonSoundState()
    {
        isSoundActive = !isSoundActive;  // �Ҹ� ���¸� ����

        // �Ҹ� ���¿� ���� �Ҹ� ����
        if (isSoundActive)
        {
            audioSource.Play();  // �Ҹ� Ȱ��ȭ
            Debug.Log("�Ҹ� Ȱ��ȭ");
        }
        else
        {
            audioSource.Pause();  // �Ҹ� ��Ȱ��ȭ
            Debug.Log("�Ҹ� ��Ȱ��ȭ");
        }

        //if (uiManager != null)
           // uiManager.UpdateImage();
    }
}


