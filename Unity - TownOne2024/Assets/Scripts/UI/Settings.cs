using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Settings : MenuBase
{
    [FormerlySerializedAs("_backButton")] [SerializeField] private Button BackButton;
    
    [FormerlySerializedAs("_sliderMasterVolume")]
    [Header("Audio")] 
    [SerializeField] private Slider SliderMasterVolume;
    [FormerlySerializedAs("_sliderMusicVolume")] [SerializeField] private Slider SliderMusicVolume;
    [FormerlySerializedAs("_sliderSoundsVolume")] [SerializeField] private Slider SliderSoundsVolume;

    public override GameMenus MenuType()
    {
        return GameMenus.SettingsMenu;
    }

    private void Awake()
    {
        SliderMasterVolume.onValueChanged.AddListener(delegate { SliderValueChanged(); });
        SliderMusicVolume.onValueChanged.AddListener(delegate { SliderValueChanged(); });
        SliderSoundsVolume.onValueChanged.AddListener(delegate { SliderValueChanged(); });
    }

    public void Close()
    {
        UIMgr.Instance.HideMenu(GameMenus.SettingsMenu);
    }

    private void OnEnable()
    {
        BackButton.Select();
        UpdateAudioDisplay();
    }

    #region Audio

    public void SliderValueChanged()
    {
        AudioMgr.Instance.GlobalVolume = SliderMasterVolume.value;
        AudioMgr.Instance.MusicVolume = SliderMusicVolume.value;
        AudioMgr.Instance.SfxVolume = SliderSoundsVolume.value;
            
        SaveUtil.Save();
            
        AudioMgr.Instance.UpdateVolumeFromSaveData();
            
        UpdateAudioDisplay();
    }

    private void UpdateAudioDisplay()
    {
        SliderMasterVolume.SetValueWithoutNotify(SaveUtil.SavedValues.GlobalVolume);
        SliderMusicVolume.SetValueWithoutNotify(SaveUtil.SavedValues.MusicVolume);
        SliderSoundsVolume.SetValueWithoutNotify(SaveUtil.SavedValues.SfxVolume);
    }
    #endregion
}
