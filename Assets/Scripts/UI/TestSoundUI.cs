using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSoundUI : UIPanel
{
    private bool isBGMActive = true;
    private bool isSFXActive = true;

    [SerializeField] private Button SettingBGMButton;
    [SerializeField] private Button SettingSoundEffectButton;
    [SerializeField] private Button PauseBGMButton;
    [SerializeField] private Button PauseSoundEffectButton;

    [SerializeField] private Sprite BGMButtonOnSprite;
    [SerializeField] private Sprite BGMButtonOffSprite;
    [SerializeField] private Sprite SoundEffectButtonOnSprite;
    [SerializeField] private Sprite SoundEffectButtonOffSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Initialize(UIManager manager)
    {
        base.Initialize(manager);


        SettingBGMButton.onClick.AddListener(BGMButtonSoundState);
        SettingSoundEffectButton.onClick.AddListener(SoundEffectButtonSoundState);
        PauseBGMButton.onClick.AddListener(BGMButtonSoundState);
        PauseSoundEffectButton.onClick.AddListener(SoundEffectButtonSoundState);


    }

    public void BGMButtonSoundState()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ToggleBGM();
            UpDateBGMButtonSprit();
        }
    }

    public void SoundEffectButtonSoundState()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ToggleSoundEffect();
            UpDateSoundEffectButtonSprit();
        }
    }    
    
    private void UpDateBGMButtonSprit()
    {
        if (SoundManager.Instance.musicAudioSource.isPlaying)
        {
            SettingBGMButton.image.sprite = BGMButtonOnSprite;
        }
        else
        {
            SettingBGMButton.image.sprite = BGMButtonOffSprite;
        }
    }

    private void UpDateSoundEffectButtonSprit()
    {
        if (SoundManager.Instance.musicAudioSource.isPlaying)
        {
            SettingSoundEffectButton.image.sprite = SoundEffectButtonOnSprite;
        }
        else
        {
            SettingSoundEffectButton.image.sprite = SoundEffectButtonOffSprite;
        }
    }

}
