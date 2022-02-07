using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipsScript : MonoBehaviour
{
    public SoundAudioClip[] AudioClipArray;
  
    

    public void Awake()
    {
        SoundManager.Initialize();
       
        
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
        public float volume = 1f;
       
    }
}
