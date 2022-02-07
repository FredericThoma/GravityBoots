using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{

    public enum Sound
    {
        PlayerMove,
        PlayerJump,
        GravChange,
        CoinPickUp,
        CheckPoint,
        Finish,
        PlayerDie,
        PlayerSpawn,
        Saw,
        CrusherFall,
        CrusherRise,
        DestPlatformCrumble,
        DestPlatformDestroy,

    }

    public static void Initialize()
    {
        soundTimeDictionary = new Dictionary<Sound, float>();
        soundTimeDictionary[Sound.PlayerMove] = 0f;
        soundTimeDictionary[Sound.PlayerJump] = 0f;
        
    }
    


   public static void PlaySound(Sound sound)
    {

        GameObject audioLib = GameObject.FindGameObjectWithTag("AudioLib");
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.volume = GetAudioClipVolume(sound);
            audioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    private static Dictionary<Sound, float> soundTimeDictionary;

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.PlayerMove:
                if (soundTimeDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimeDictionary[sound];
                    float playerMoveTimerMax = 0.135f;
                    if(lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimeDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
                
        }
    }

    private static float  GetAudioClipVolume(Sound sound)
    {
        GameObject audioLib = GameObject.FindGameObjectWithTag("AudioLib");
        AudioClipsScript audioClipsScript = audioLib.GetComponent<AudioClipsScript>();




        foreach (AudioClipsScript.SoundAudioClip soundAudioClip in audioClipsScript.AudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {

                return soundAudioClip.volume;
            }
        }
        Debug.LogError("Sound not found");
        return 0f;
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        GameObject audioLib = GameObject.FindGameObjectWithTag("AudioLib");
        AudioClipsScript audioClipsScript = audioLib.GetComponent<AudioClipsScript>();

        


        foreach (AudioClipsScript.SoundAudioClip soundAudioClip in audioClipsScript.AudioClipArray)
        {
            if(soundAudioClip.sound == sound)
            {
                
                return soundAudioClip.audioClip;
                }
        }
        Debug.LogError("Sound not found");
        return null;
    }
    
}
