using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;


public class SoundManager : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float soundEffectVolum;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    private AudioSource musicAudioSource;
    public AudioClip[] musicClip;

    public AudioClip[] sfxSounds;

    ObjectPool objectPool;

    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

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

    public void PlayClip(int index, float volumeMultiplier = 1.0f)
    {
        GameObject go = objectPool.SpawnFromPool("SoundSource");
        go.SetActive(true);
        SoundSource soundSource = go.GetComponent<SoundSource>();
        soundSource.Play(sfxSounds[index], soundEffectVolum * volumeMultiplier, soundEffectPitchVariance);
    }


}
