using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField]private AudioSource MusicSource;

    [SerializeField]private AudioSource SFXSource;

    [SerializeField]private AudioClip Music;

    private void Awake(){

        if(instance == null){
            instance = this;
        }
        else if(instance != this){
            Destroy(this);
        }
    }

    void Start()
    {
        PlayMusic(Music);
    }

    public void SetMaxVolume(float volume){
        MusicSource.maxDistance = volume;
        SFXSource.maxDistance = volume;
    }

    public void SetVolume(float volume){
        MusicSource.volume = volume;
        SFXSource.volume = volume;
    }

    public void PlaySound(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip){
        MusicSource.PlayOneShot(clip);
    }


}
