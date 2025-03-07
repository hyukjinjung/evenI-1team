using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;


/*public class SoundMnager : MonoBehaviour
{
   [SerializeField][Range(0f, 1f)] private float soundEffectVolum;
   [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
   [SerializeField][Range(0f, 1f)] private float musicVolume;


        private AudioSource musicAudioSource;
        public AudioClip musicClip;

        // Start is called before the first frame update
        void Awake()
        {
            musicAudioSource = GetComponent<AudioSource>();
            musicAudioSource.volume = musicVolume;
            musicAudioSource.loop = true;

        }

        // Update is called once per frame
        private void Start()  
        {
            ChangeBackGroundMusic(musicClip);
        }

        private static void ChangeBackGroundMusic(AudioClip musicClip)
        {
            Instance.musicAudioSource.Stop();
            Instance.musicAudioSource.clip = musicClip;
            Instance.musicAudioSource.Play();
        }

        public static void PlayClip(AudioClip clip, float volumeMultiplier = 1.0f)
        {
            GameObject go = Inatance.objectPool.SpawnFromPool("SoundSource");
            go.SetActive(true);
            SoundSource soundSource = go.GetComponent<SoundSource>();
            SoundSource.Play(clip, Instance.soundEffectVolume* volumeMultiplier, Instance.soundEffectPitchVariance);
        }     

    
}
*/