using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundSetting : MonoBehaviour
{
    public Image settingImage;
    public Sprite soundSettingImage;
    public Sprite muteSettingImage;

    private void Start()
    {
        ChangeSettingImage();
    }
    public void ChangeSound()
    {
        if(!AudioManager.Instance.isMuted)
        {
            AudioManager.Instance.MusicVolume(0);
            AudioManager.Instance.SFXVolume(0);
            AudioManager.Instance.isMuted = true;
            settingImage.sprite = muteSettingImage;
        }
        else
        {
            AudioManager.Instance.MusicVolume(1);
            AudioManager.Instance.SFXVolume(1);
            AudioManager.Instance.isMuted = false;
            settingImage.sprite = soundSettingImage;
        }
        PlayerPrefs.SetInt("SoundMuted", AudioManager.Instance.isMuted ? 1 : 0);
        PlayerPrefs.Save();
    } 
    
    private void ChangeSettingImage()
    {
        if (AudioManager.Instance.isMuted)
        {
            settingImage.sprite = muteSettingImage;
        }
        else
        {
            settingImage.sprite = soundSettingImage;
        }
    }    

}
