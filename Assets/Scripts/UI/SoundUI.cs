using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundUI : UIPanel
{

    
    
        public AudioSource bgmAudioSource; // BGM ����� �ҽ�
        public AudioSource soundEffectAudioSource; // ȿ���� ����� �ҽ�

        // ������ ����� ���¸� ���� �����ϵ��� ����
        private bool isBGMActive = true;
        private bool isSFXActive = true;

        public Image SettingBGMButtonImage;
        public Image SettingSoundEffectButtonImage;
        public Image PauseBGMButtonImage;
        public Image PauseSoundEffectButtonImage;

        public Button SettingBGMButton;
        public Button SettingSoundEffectButton;
        public Button PauseBGMButton;
        public Button PauseSoundEffectButton;

        public Sprite SettingBGMButtonOnSprite;
        public Sprite SettingBGMButtonOffSprite;
        public Sprite SettingSoundEffectButtonOnSprite;
        public Sprite SettingSoundEffectButtonOffSprite;
        public Sprite PauseBGMButtonOnSprite;
        public Sprite PauseBGMButtonOffSprite;
        public Sprite PauseSoundEffectButtonOnSprite;
        public Sprite PauseSoundEffectButtonOffSprite;

        public override void Initialize(UIManager manager)
        {
            base.Initialize(manager);
            SettingBGMButton.onClick.AddListener(SettingBGMButtonSoundState);
            SettingSoundEffectButton.onClick.AddListener(SettingSoundEffectButtonSoundState);
            PauseBGMButton.onClick.AddListener(PauseBGMButtonSoundState);
            PauseSoundEffectButton.onClick.AddListener(PauseSoundEffectButtonSoundState);
        }

        public void SettingBGMButtonSoundState()
        {
            isBGMActive = !isBGMActive;  // BGM ���¸� ����

            // BGM ���¿� ���� BGM �Ҹ� ����
            if (isBGMActive)
            {
                bgmAudioSource.Play();  // BGM Ȱ��ȭ
                SettingBGMButton.image.sprite = SettingBGMButtonOnSprite;
                Debug.Log("BGM Ȱ��ȭ");
            }
            else
            {
                bgmAudioSource.Pause();  // BGM ��Ȱ��ȭ
                SettingBGMButton.image.sprite = SettingBGMButtonOffSprite;
                Debug.Log("BGM ��Ȱ��ȭ");
            }
        }

        public void SettingSoundEffectButtonSoundState()
        {
            isSFXActive = !isSFXActive;  // ȿ���� ���¸� ����

            // ȿ���� ���¿� ���� ȿ���� �Ҹ� ����
            if (isSFXActive)
            {
                soundEffectAudioSource.Play();  // ȿ���� Ȱ��ȭ
                SettingSoundEffectButton.image.sprite = SettingSoundEffectButtonOnSprite;
                Debug.Log("ȿ���� Ȱ��ȭ");
            }
            else
            {
                soundEffectAudioSource.Pause();  // ȿ���� ��Ȱ��ȭ
                SettingSoundEffectButton.image.sprite = SettingSoundEffectButtonOffSprite;
                Debug.Log("ȿ���� ��Ȱ��ȭ");
            }
        }

        public void PauseBGMButtonSoundState()
        {
            isBGMActive = !isBGMActive;  // BGM ���¸� ����

            // BGM ���¿� ���� BGM �Ҹ� ����
            if (isBGMActive)
            {
                bgmAudioSource.Play();  // BGM Ȱ��ȭ
                PauseBGMButton.image.sprite = PauseBGMButtonOnSprite;
                Debug.Log("BGM Ȱ��ȭ");
            }
            else
            {
                bgmAudioSource.Pause();  // BGM ��Ȱ��ȭ
                PauseBGMButton.image.sprite = PauseBGMButtonOffSprite;
                Debug.Log("BGM ��Ȱ��ȭ");
            }
        }

        public void PauseSoundEffectButtonSoundState()
        {
            isSFXActive = !isSFXActive;  // ȿ���� ���¸� ����

            // ȿ���� ���¿� ���� ȿ���� �Ҹ� ����
            if (isSFXActive)
            {
                soundEffectAudioSource.Play();  // ȿ���� Ȱ��ȭ
                PauseSoundEffectButton.image.sprite = PauseSoundEffectButtonOnSprite;
                Debug.Log("ȿ���� Ȱ��ȭ");
            }
            else
            {
                soundEffectAudioSource.Pause();  // ȿ���� ��Ȱ��ȭ
                PauseSoundEffectButton.image.sprite = PauseSoundEffectButtonOffSprite;
                Debug.Log("ȿ���� ��Ȱ��ȭ");
            }
        }
    

    /* public AudioSource[] bgmAudioSources;
     public AudioSource[] soundEffectAudioSources;

     public AudioSource bgm1;  // ù ��° BGM
     public AudioSource bgm2;  // �� ��° BGM
     public AudioSource bgm3;  // �� ��° BGM
     public AudioSource bgm4;  // �� ��° BGM

     public AudioSource soundEffect1;  // ù ��° SFX
     public AudioSource soundEffect2;  // �� ��° SFX
     public AudioSource soundEffect3;  // �� ��° SFX
     public AudioSource soundEffect4;  // �� ��° SFX
     public AudioSource soundEffect5;  // �� ��° SFX
     public AudioSource soundEffect6;  // �� ��° SFX
     public AudioSource soundEffect7;  // �� ��° SFX
     public AudioSource soundEffect8;  // �� ��° SFX
     public AudioSource soundEffect9;  // �� ��° SFX
     public AudioSource soundEffect10;  // �� ��° SFX
     public AudioSource soundEffect11;  // �� ��° SFX
     public AudioSource soundEffect12;  // �� ��° SFX
     public AudioSource soundEffect13;  // �� ��° SFX
     public AudioSource soundEffect14;  // �� ��° SFX
     public AudioSource soundEffect15;  // �� ��° SFX
     public AudioSource soundEffect16;  // �� ��° SFX
     public AudioSource soundEffect17;  // �� ��° SFX
     public AudioSource soundEffect18;  // �� ��° SFX
     public AudioSource soundEffect19;  // �� ��° SFX
     public AudioSource soundEffect20;  // �� ��° SFX
     public AudioSource soundEffect21;  // �� ��° SFX
     public AudioSource soundEffect22;  // �� ��° SFX
     public AudioSource soundEffect23;  // �� ��° SFX
     public AudioSource soundEffect24;  // �� ��° SFX


     public bool isBGMActive = true;
     public bool isSFXActive = true;// �ʱ� �Ҹ��� Ȱ��ȭ

     public Button SettingBGMButton;
     public Button SettingSoundEffectButton;
     public Button PauseBGMButton;
     public Button PauseSoundEffectButton;

     public Image SettingBGMButtonsoundImage;
     public Image SettingSoundEffectButtonImage;
     public Image PauseBGMButtonImage;
     public Image PauseSoundEffectButtonImage;


     public Sprite SettingBGMButtonOnSprite;
     public Sprite SettingBGMButtonOffSprite;
     public Sprite SettingSoundEffectButtonnOnSprite;
     public Sprite SettingSoundEffectButtonOffSprite;
     public Sprite PauseBGMButtonOnSprite;
     public Sprite PauseBGMButtonOffSprite;
     public Sprite PauseSoundEffectButtonOnSprite;
     public Sprite PauseSoundEffectButtonOffSprite;

     private void Awake()
     {
         bgmAudioSources = new AudioSource[4];
         soundEffectAudioSources = new AudioSource[23];

         bgmAudioSources[0] = bgm1;
         bgmAudioSources[1] = bgm2;
         bgmAudioSources[2] = bgm3;
         bgmAudioSources[3] = bgm4;

         soundEffectAudioSources[0] = soundEffect1;
         soundEffectAudioSources[1] = soundEffect2;
         soundEffectAudioSources[2] = soundEffect3;
         soundEffectAudioSources[3] = soundEffect4;
         soundEffectAudioSources[4] = soundEffect5;
         soundEffectAudioSources[5] = soundEffect6;
         soundEffectAudioSources[6] = soundEffect7;
         soundEffectAudioSources[7] = soundEffect8;
         soundEffectAudioSources[8] = soundEffect9;
         soundEffectAudioSources[9] = soundEffect10;
         soundEffectAudioSources[9] = soundEffect11;
         soundEffectAudioSources[9] = soundEffect12;
         soundEffectAudioSources[9] = soundEffect13;
         soundEffectAudioSources[9] = soundEffect14;
         soundEffectAudioSources[9] = soundEffect15;
         soundEffectAudioSources[9] = soundEffect16;
         soundEffectAudioSources[9] = soundEffect17;
         soundEffectAudioSources[9] = soundEffect18;
         soundEffectAudioSources[9] = soundEffect19;
         soundEffectAudioSources[9] = soundEffect20;
         soundEffectAudioSources[9] = soundEffect21;
         soundEffectAudioSources[9] = soundEffect22;
         soundEffectAudioSources[9] = soundEffect23;

     }
     public override void Initialize(UIManager manager)
     {
         base.Initialize(manager);
         SettingBGMButton.onClick.AddListener(SettingBGMButtonSoundState);
         SettingSoundEffectButton.onClick.AddListener(SettingSoundEffectButtonSoundState);
         PauseBGMButton.onClick.AddListener(PauseBGMButtonSoundState);
         PauseSoundEffectButton.onClick.AddListener(PauseSoundEffectButtonSoundState);


     }

     public void SettingBGMButtonSoundState()
     {
         isBGMActive = !isBGMActive;  // �Ҹ� ���¸� ����

         // �Ҹ� ���¿� ���� �Ҹ� ����
         foreach (var audioSource in bgmAudioSources)
         {
             if (isBGMActive)
             {
                 audioSource.Play();
                 Debug.Log("BGM Ȱ��ȭ");

                 SettingBGMButton.image.sprite = SettingBGMButtonOnSprite;
             }
             else
             {
                 audioSource.Pause();  // �Ҹ� ��Ȱ��ȭ
                 Debug.Log("BGM ��Ȱ��ȭ");

                 SettingBGMButton.image.sprite = SettingBGMButtonOffSprite;
             }
         }




     }

     public void SettingSoundEffectButtonSoundState()
     {
         isSFXActive = !isSFXActive;  // �Ҹ� ���¸� ����

         // �Ҹ� ���¿� ���� �Ҹ� ����
         foreach (var audioSource in soundEffectAudioSources)
         {
             if (isSFXActive)
             {
                 soundEffectAudioSources.Play();  // �Ҹ� Ȱ��ȭ
                 Debug.Log("�Ҹ� Ȱ��ȭ");

                 SettingSoundEffectButton.image.sprite = SettingSoundEffectButtonnOnSprite;
             }

             else
             {
                 soundEffectAudioSources.Pause();  // �Ҹ� ��Ȱ��ȭ
                 Debug.Log("�Ҹ� ��Ȱ��ȭ");

                 SettingSoundEffectButton.image.sprite = SettingSoundEffectButtonOffSprite;

             }
         }
     }


     public void PauseBGMButtonSoundState()
     {
         isBGMActive = !isBGMActive;  // �Ҹ� ���¸� ����

         // �Ҹ� ���¿� ���� �Ҹ� ����
         foreach (var audioSource in bgmAudioSources)
         {
             if (isBGMActive)
             {
                 bgmAudioSources.Play();  // �Ҹ� Ȱ��ȭ
                 Debug.Log("BGM Ȱ��ȭ");

                 PauseBGMButton.image.sprite = PauseBGMButtonOnSprite;
             }
             else
             {
                 bgmAudioSources.Pause();  // �Ҹ� ��Ȱ��ȭ
                 Debug.Log("�Ҹ� ��Ȱ��ȭ");

                 PauseBGMButton.image.sprite = PauseBGMButtonOffSprite;
             }

         }
     }

     public void PauseSoundEffectButtonSoundState()
     {
         isSFXActive = !isSFXActive;  // �Ҹ� ���¸� ����

         // �Ҹ� ���¿� ���� �Ҹ� ����
         foreach (var audioSource in bgmAudioSources)
         {
             if (isSFXActive)
             {
                 soundEffectAudioSources.Play();  // �Ҹ� Ȱ��ȭ
                 Debug.Log("�Ҹ� Ȱ��ȭ");

                 PauseSoundEffectButton.image.sprite = PauseSoundEffectButtonOnSprite;
             }
             else
             {
                 soundEffectAudioSources.Pause();  // �Ҹ� ��Ȱ��ȭ
                 Debug.Log("�Ҹ� ��Ȱ��ȭ");

                 PauseSoundEffectButton.image.sprite = PauseSoundEffectButtonOffSprite;
             }
         }
     }*/
}





