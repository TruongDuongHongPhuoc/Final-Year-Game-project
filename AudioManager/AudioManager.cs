using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set;}
    [SerializeField] List<Sound> sounds= new List<Sound>();
    [HideInInspector]private Sound themesound;
    private void Awake() {
          if(instance != null && instance != this){
        Destroy(this.gameObject);
        }else{
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        foreach (var sound in sounds) {
            AudioSource src = gameObject.AddComponent<AudioSource>();
            sound.source = src;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.isLoop;
            sound.source.clip = sound.audioClip;
        }
    }
    private void Start() {
        PlayAudioHaveName("Theme");
    }
    public void PlayAudioHaveName(string name){
        foreach (var sound in sounds){
            if(sound.soundName.Equals(name)){
                sound.source.Play();
                return;
            }
        }
        Debug.LogError("sound:" + name + "is not exits");
    }
    public void PlayThemeMusic(string name){
         foreach (var sound in sounds){
            if(sound.soundName.Equals(name)){
                if(themesound != null){
                themesound.source.Stop();
                themesound = sound;
                themesound.source.Play();
                }
                return;
            }
        }
    }
    public void StopAudioHaveName(string name){
         foreach (var sound in sounds){
            if(sound.soundName.Equals(name)){
                sound.source.Stop();
                return;
            }
        }
    }
}
[Serializable]
public class Sound{
    public string soundName;
    public bool isLoop;
    public AudioClip audioClip;
    [Range(0,1f)]
    public float volume;
    [HideInInspector]
    public AudioSource source;
}
