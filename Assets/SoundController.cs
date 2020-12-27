using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Drop,
    Baby,
    Trampoline,
    Success,
    Crash,
}

public class SoundController : MonoBehaviour
{
    static SoundController instance;
    public  bool Music_Mute = false;
    public  bool Sound_Mute = false;

    public AudioSource musicSource;
    

    [Header("Musik For BG Play :")]
    public AudioClip[] music = new AudioClip[2];

    [Header("Musik Drop Item Sounds :")]
    public AudioClip[] DropSound = new AudioClip[2];

    [Header("Musik Drop Item Sounds :")]
    public AudioClip[] BabySounds = new AudioClip[3];

    [Header("TrampolineSound Sounds :")]
    public AudioClip[] TrampolineSounds = new AudioClip[3];

    [Header("Success Sounds :")]
    public AudioClip successSounds;

    [Header("Crush Sounds :")]
    public AudioClip[] CrashSounds = new AudioClip[3];

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
        musicSource.clip = music[Random.Range(0, music.Length)];
        musicSource.Play();
    }


    public static bool IsInitialized
    {
        get
        {
            return instance != null;
        }
    }

    public static SoundController GetSingleton
    {
        get
        {
            return instance;
        }
    }

    public void PlaySound(SoundType type)
    {
        StartCoroutine(PlaySoundCorutine(type));
        
    }

    public void PlayFaster()
    {
        if (musicSource.pitch < 2.0f)
            musicSource.pitch += 0.005f;
    }

    public void NormalizeSound()
    {
        musicSource.pitch = 1.75f;
    }

    IEnumerator PlaySoundCorutine(SoundType type)
    {
        AudioSource soundSource = this.gameObject.AddComponent< AudioSource>();
        soundSource.mute = SoundController.GetSingleton.Sound_Mute;
        soundSource.volume = .4f;
        switch (type)
        {
            case SoundType.Drop:
                soundSource.clip = DropSound[Random.Range(0, DropSound.Length)];
                break;
            case SoundType.Baby:
                soundSource.clip = BabySounds[Random.Range(0, BabySounds.Length)];
                break;
            case SoundType.Trampoline:
                soundSource.clip = TrampolineSounds[Random.Range(0, TrampolineSounds.Length)];
                break;
            case SoundType.Success:
                soundSource.clip = successSounds;
                break;
            case SoundType.Crash:
                soundSource.clip = CrashSounds[Random.Range(0, CrashSounds.Length)];
                break;
        }
        soundSource.Play();
        yield return new WaitForSeconds(2);
        DestroyImmediate(soundSource);
    }

    // Update is called once per frame
    void Update()
    {
        musicSource.mute = SoundController.GetSingleton.Music_Mute;
        
    }
}
