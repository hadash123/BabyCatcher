using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundPanel : MonoBehaviour
{
    public Sprite music;
    public Sprite musicOff;

    public Sprite sound;
    public Sprite soundOff;

    public Image musicImage;
    public Image soundImage;

    public Transform controller;

    private void Start()
    {
        if (controller != null) 
        { 
            if (SoundController.GetSingleton == null)
            {
                Instantiate(controller, new Vector3(0, 0, 0), Quaternion.identity).parent = this.transform.parent;
            }
            else
            {
                if (SoundController.IsInitialized)
                {
                    musicImage.sprite = SoundController.GetSingleton.Music_Mute ? musicOff : music;
                    soundImage.sprite = SoundController.GetSingleton.Sound_Mute ? soundOff : sound;
                }
            }
        }
    }

    public void MusicChange()
    {
        musicImage.sprite = !SoundController.GetSingleton.Music_Mute ? musicOff: music;
        SoundController.GetSingleton.Music_Mute = !SoundController.GetSingleton.Music_Mute;

    }

    public void SoundChange()
    {
        soundImage.sprite = !SoundController.GetSingleton.Sound_Mute ? soundOff: sound;
        SoundController.GetSingleton.Sound_Mute = !SoundController.GetSingleton.Sound_Mute;
    }
}
