using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundUI : UIPanel
{

    
    
        public AudioSource bgmAudioSource; // BGM 오디오 소스
        public AudioSource soundEffectAudioSource; // 효과음 오디오 소스

        // 각각의 오디오 상태를 따로 관리하도록 수정
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
            isBGMActive = !isBGMActive;  // BGM 상태를 반전

            // BGM 상태에 맞춰 BGM 소리 제어
            if (isBGMActive)
            {
                bgmAudioSource.Play();  // BGM 활성화
                SettingBGMButton.image.sprite = SettingBGMButtonOnSprite;
                Debug.Log("BGM 활성화");
            }
            else
            {
                bgmAudioSource.Pause();  // BGM 비활성화
                SettingBGMButton.image.sprite = SettingBGMButtonOffSprite;
                Debug.Log("BGM 비활성화");
            }
        }

        public void SettingSoundEffectButtonSoundState()
        {
            isSFXActive = !isSFXActive;  // 효과음 상태를 반전

            // 효과음 상태에 맞춰 효과음 소리 제어
            if (isSFXActive)
            {
                soundEffectAudioSource.Play();  // 효과음 활성화
                SettingSoundEffectButton.image.sprite = SettingSoundEffectButtonOnSprite;
                Debug.Log("효과음 활성화");
            }
            else
            {
                soundEffectAudioSource.Pause();  // 효과음 비활성화
                SettingSoundEffectButton.image.sprite = SettingSoundEffectButtonOffSprite;
                Debug.Log("효과음 비활성화");
            }
        }

        public void PauseBGMButtonSoundState()
        {
            isBGMActive = !isBGMActive;  // BGM 상태를 반전

            // BGM 상태에 맞춰 BGM 소리 제어
            if (isBGMActive)
            {
                bgmAudioSource.Play();  // BGM 활성화
                PauseBGMButton.image.sprite = PauseBGMButtonOnSprite;
                Debug.Log("BGM 활성화");
            }
            else
            {
                bgmAudioSource.Pause();  // BGM 비활성화
                PauseBGMButton.image.sprite = PauseBGMButtonOffSprite;
                Debug.Log("BGM 비활성화");
            }
        }

        public void PauseSoundEffectButtonSoundState()
        {
            isSFXActive = !isSFXActive;  // 효과음 상태를 반전

            // 효과음 상태에 맞춰 효과음 소리 제어
            if (isSFXActive)
            {
                soundEffectAudioSource.Play();  // 효과음 활성화
                PauseSoundEffectButton.image.sprite = PauseSoundEffectButtonOnSprite;
                Debug.Log("효과음 활성화");
            }
            else
            {
                soundEffectAudioSource.Pause();  // 효과음 비활성화
                PauseSoundEffectButton.image.sprite = PauseSoundEffectButtonOffSprite;
                Debug.Log("효과음 비활성화");
            }
        }
    

    /* public AudioSource[] bgmAudioSources;
     public AudioSource[] soundEffectAudioSources;

     public AudioSource bgm1;  // 첫 번째 BGM
     public AudioSource bgm2;  // 두 번째 BGM
     public AudioSource bgm3;  // 두 번째 BGM
     public AudioSource bgm4;  // 두 번째 BGM

     public AudioSource soundEffect1;  // 첫 번째 SFX
     public AudioSource soundEffect2;  // 두 번째 SFX
     public AudioSource soundEffect3;  // 두 번째 SFX
     public AudioSource soundEffect4;  // 두 번째 SFX
     public AudioSource soundEffect5;  // 두 번째 SFX
     public AudioSource soundEffect6;  // 두 번째 SFX
     public AudioSource soundEffect7;  // 두 번째 SFX
     public AudioSource soundEffect8;  // 두 번째 SFX
     public AudioSource soundEffect9;  // 두 번째 SFX
     public AudioSource soundEffect10;  // 두 번째 SFX
     public AudioSource soundEffect11;  // 두 번째 SFX
     public AudioSource soundEffect12;  // 두 번째 SFX
     public AudioSource soundEffect13;  // 두 번째 SFX
     public AudioSource soundEffect14;  // 두 번째 SFX
     public AudioSource soundEffect15;  // 두 번째 SFX
     public AudioSource soundEffect16;  // 두 번째 SFX
     public AudioSource soundEffect17;  // 두 번째 SFX
     public AudioSource soundEffect18;  // 두 번째 SFX
     public AudioSource soundEffect19;  // 두 번째 SFX
     public AudioSource soundEffect20;  // 두 번째 SFX
     public AudioSource soundEffect21;  // 두 번째 SFX
     public AudioSource soundEffect22;  // 두 번째 SFX
     public AudioSource soundEffect23;  // 두 번째 SFX
     public AudioSource soundEffect24;  // 두 번째 SFX


     public bool isBGMActive = true;
     public bool isSFXActive = true;// 초기 소리는 활성화

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
         isBGMActive = !isBGMActive;  // 소리 상태를 반전

         // 소리 상태에 맞춰 소리 제어
         foreach (var audioSource in bgmAudioSources)
         {
             if (isBGMActive)
             {
                 audioSource.Play();
                 Debug.Log("BGM 활성화");

                 SettingBGMButton.image.sprite = SettingBGMButtonOnSprite;
             }
             else
             {
                 audioSource.Pause();  // 소리 비활성화
                 Debug.Log("BGM 비활성화");

                 SettingBGMButton.image.sprite = SettingBGMButtonOffSprite;
             }
         }




     }

     public void SettingSoundEffectButtonSoundState()
     {
         isSFXActive = !isSFXActive;  // 소리 상태를 반전

         // 소리 상태에 맞춰 소리 제어
         foreach (var audioSource in soundEffectAudioSources)
         {
             if (isSFXActive)
             {
                 soundEffectAudioSources.Play();  // 소리 활성화
                 Debug.Log("소리 활성화");

                 SettingSoundEffectButton.image.sprite = SettingSoundEffectButtonnOnSprite;
             }

             else
             {
                 soundEffectAudioSources.Pause();  // 소리 비활성화
                 Debug.Log("소리 비활성화");

                 SettingSoundEffectButton.image.sprite = SettingSoundEffectButtonOffSprite;

             }
         }
     }


     public void PauseBGMButtonSoundState()
     {
         isBGMActive = !isBGMActive;  // 소리 상태를 반전

         // 소리 상태에 맞춰 소리 제어
         foreach (var audioSource in bgmAudioSources)
         {
             if (isBGMActive)
             {
                 bgmAudioSources.Play();  // 소리 활성화
                 Debug.Log("BGM 활성화");

                 PauseBGMButton.image.sprite = PauseBGMButtonOnSprite;
             }
             else
             {
                 bgmAudioSources.Pause();  // 소리 비활성화
                 Debug.Log("소리 비활성화");

                 PauseBGMButton.image.sprite = PauseBGMButtonOffSprite;
             }

         }
     }

     public void PauseSoundEffectButtonSoundState()
     {
         isSFXActive = !isSFXActive;  // 소리 상태를 반전

         // 소리 상태에 맞춰 소리 제어
         foreach (var audioSource in bgmAudioSources)
         {
             if (isSFXActive)
             {
                 soundEffectAudioSources.Play();  // 소리 활성화
                 Debug.Log("소리 활성화");

                 PauseSoundEffectButton.image.sprite = PauseSoundEffectButtonOnSprite;
             }
             else
             {
                 soundEffectAudioSources.Pause();  // 소리 비활성화
                 Debug.Log("소리 비활성화");

                 PauseSoundEffectButton.image.sprite = PauseSoundEffectButtonOffSprite;
             }
         }
     }*/
}





