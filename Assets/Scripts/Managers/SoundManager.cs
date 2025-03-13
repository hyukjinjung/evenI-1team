using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class SoundManager : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float soundEffectVolum;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    public AudioSource musicAudioSource;

    public AudioClip[] musicClip;
    public AudioClip[] sfxSounds;

    ObjectPool objectPool;

    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    private bool isSFXEnabled = true;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        objectPool = GetComponent<ObjectPool>();
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
    }

    // Update is called once per frame
    private void Start()
    {
        ChangeBackGroundMusic(0);
    }

    public void ChangeBackGroundMusic(int index)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = musicClip[index];
        musicAudioSource.Play();
    }

    public void PlayClip(int index, float pitch = 1.0f, float volumeMultiplier = 1.0f)
    {
        if (!isSFXEnabled) return;

        GameObject go = objectPool.SpawnFromPool("SoundSource");
        go.SetActive(true);
        SoundSource soundSource = go.GetComponent<SoundSource>();


        //soundSource.SetPitch(pitch);
        soundSource.Play(sfxSounds[index], soundEffectVolum * volumeMultiplier, soundEffectPitchVariance);

        soundSource.enabled = false;
    }

    public void ToggleBGM()
    {
        musicAudioSource.mute = !musicAudioSource.mute;
        //    if (musicAudioSource.isPlaying)
        //    {
        //        musicAudioSource.Pause(); 
        //    }
        //    else
        //    {
        //        musicAudioSource.Play(); 
        //    }
    }
    public void ToggleSoundEffect()
    {
        isSFXEnabled = !isSFXEnabled;
        //if (musicAudioSource.isPlaying)
        //{
        //    musicAudioSource.Pause();
        //}
        //else
        //{
        //    SoundManager.Instance.PlayClip()

        //}
    }
    public bool IsBGMPlaying()
    {
        return musicAudioSource.isPlaying;
    }

    public bool IsSoundEffectPlaying()
    {
        return musicAudioSource.isPlaying;
    }
}
