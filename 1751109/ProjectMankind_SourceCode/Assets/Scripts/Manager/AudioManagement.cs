using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    public static AudioManagement instance;
    public AudioSource audioSource;
    public AudioSource oneClipSource;
    public AudioClip universeMusic;
    public AudioClip battleMusic;
    private void Awake() {
        instance = this;
    }

    public void ChangeMusic(AudioClip music) {
        audioSource.clip = music;
        audioSource.Play();
    }

    public void BattleMusic() {
        if (audioSource.clip != battleMusic) {
            audioSource.clip = battleMusic;
            audioSource.volume = 0.3f;
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void UniverseMusic() {
        
        if (audioSource.clip != universeMusic) {
            audioSource.volume = 0.1f;
            audioSource.clip = universeMusic;
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlayOneShot(AudioClip clip) {
        Debug.Log(clip);
       oneClipSource.PlayOneShot(clip);
    }

    public void StopOneShoot(AudioClip clip) {

        oneClipSource.Stop();
    }





}
