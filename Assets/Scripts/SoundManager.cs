using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound {
        towerShoot,
        playerHit,
    }
    public static void PlaySound(Sound sound){
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound){
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClips){
            if(soundAudioClip.sound == sound){
                return soundAudioClip.audioClip;
            }
        }
        return null;
    }
}