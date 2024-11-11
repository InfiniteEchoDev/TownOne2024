using UnityEngine;
using UnityEngine.UI;

public class Settings : MenuBase
{
    [SerializeField] private Button _backButton;
    
    [Header("Audio")] 
    [SerializeField] private Slider _sliderMasterVolume;
    [SerializeField] private Slider _sliderMusicVolume;
    [SerializeField] private Slider _sliderSoundsVolume;

    public override GameMenus MenuType()
    {
        return GameMenus.SettingsMenu;
    }

    public void Close()
    {
        UIMgr.Instance.HideMenu(GameMenus.SettingsMenu);
    }

    private void OnEnable()
    {
        _backButton.Select();
        UpdateAudioDisplay();
    }

    #region Audio

    public void SliderValueChanged()
    {
        AudioMgr.Instance.GlobalVolume = _sliderMasterVolume.value;
        AudioMgr.Instance.MusicVolume = _sliderMusicVolume.value;
        AudioMgr.Instance.SfxVolume = _sliderSoundsVolume.value;
            
        SaveUtil.Save();
            
        AudioMgr.Instance.UpdateVolumeFromSaveData();
            
        UpdateAudioDisplay();
    }

    private void UpdateAudioDisplay()
    {
        _sliderMasterVolume.SetValueWithoutNotify(SaveUtil.SavedValues.GlobalVolume);
        _sliderMusicVolume.SetValueWithoutNotify(SaveUtil.SavedValues.MusicVolume);
        _sliderSoundsVolume.SetValueWithoutNotify(SaveUtil.SavedValues.SfxVolume);
    }
    #endregion
}
