//using UnityEngine;
//using UnityEngine.Audio;

public class AudioMgr : Singleton<AudioMgr>
{
    // public enum MusicTypes
    // {
    //     Login = 0,
    //     MainMenu = 1,
    //     RunGameplay = 2
    // }
    //
    // public enum SoundTypes
    // {
    //     ButtonSelect = 0,
    //     ButtonHover = 1,
    //     ButtonError = 2,
    // }
    //
    // [Header("Mixer")]
    // [SerializeField] private AudioMixer _mixer;
    //
    // [Header("Sources")]
    // [SerializeField] private AudioSource _musicSource;
    //
    // [SerializeField] private AudioSource _sfxSource;
    //
    // [Header("Reusable Clips")] [SerializeField]
    // private AudioClip[] _reusableMusicClips;
    //
    // [SerializeField] private AudioClip[] _reusableSoundClips;
    //
    // private AudioSource MusicPlayer
    // {
    //     get
    //     {
    //         if (_musicSource == null)
    //         {
    //             _musicSource = gameObject.AddComponent<AudioSource>();
    //             _musicSource.loop = true;
    //         }
    //
    //         return _musicSource;
    //     }
    // }
    //
    // private AudioSource SfxPlayer
    // {
    //     get
    //     {
    //         if (_sfxSource == null) _sfxSource = gameObject.AddComponent<AudioSource>();
    //         return _sfxSource;
    //     }
    // }
    //
    // /// Read and save from player prefs
    // public float GlobalVolume
    // {
    //     set => SaveDataHelper.ActiveSaveData.GlobalVolume = value;
    //     get => SaveDataHelper.ActiveSaveData.GlobalVolume;
    // }
    //
    // public float MusicVolume
    // {
    //     set => SaveDataHelper.ActiveSaveData.MusicVolume = value;
    //     get => SaveDataHelper.ActiveSaveData.MusicVolume;
    // }
    //
    // public float SfxVolume
    // {
    //     set => SaveDataHelper.ActiveSaveData.SfxVolume = value;
    //     get => SaveDataHelper.ActiveSaveData.SfxVolume;
    // }
    //
    // private void Start()
    // {
    //     DontDestroyOnLoad(this);
    //     SaveDataHelper.OnDataLoadComplete += OnDataLoadComplete;
    //     SaveDataHelper.LoadSaveData();
    // }
    //
    // private void OnDataLoadComplete()
    // {
    //     SaveDataHelper.OnDataLoadComplete -= OnDataLoadComplete;
    //
    //     UpdateVolumeFromSaveData();
    // }
    //
    // public void UpdateVolumeFromSaveData()
    // {
    //     _mixer.SetFloat("MasterVol", GlobalVolume);
    //     _mixer.SetFloat("MusicVol", MusicVolume);
    //     _mixer.SetFloat("SfxVol", SfxVolume);
    // }
    //
    // public void PlayMusic(MusicTypes music, float volumeMod)
    // {
    //     var index = (int) music;
    //     if (_reusableMusicClips.Length < index)
    //     {
    //         Debug.LogWarning($"Music type {music.ToString()} not found in music clips");
    //         return;
    //     }
    //
    //     PlayMusic(_reusableMusicClips[(int) music], volumeMod);
    // }
    //
    // public void PlayMusic(AudioClip clip, float volumeMod)
    // {
    //     if (volumeMod <= 0f) return;
    //     MusicPlayer.clip = clip;
    //     MusicPlayer.volume = volumeMod;
    //     MusicPlayer.loop = true;
    //     MusicPlayer.Play();
    // }
    //
    // public void PauseMusic()
    // {
    //     MusicPlayer.Pause();
    // }
    //
    // public void ResumeMusic()
    // {
    //     MusicPlayer.Play();
    // }
    //
    // public void PlayOneShotMusic(AudioClip clip, float volumeMod)
    // {
    //     MusicPlayer.Stop();
    //     if (volumeMod <= 0f) return;
    //     MusicPlayer.PlayOneShot(clip, volumeMod);
    // }
    //
    // public void PlaySoundNoOverlap(SoundTypes sound, float volumeMod)
    // {
    //     if (!SfxPlayer.isPlaying)
    //         PlaySound(sound, volumeMod);
    // }
    //
    // public void PlaySound(SoundTypes sound, float volumeMod)
    // {
    //     var index = (int) sound;
    //     if (_reusableSoundClips.Length < index)
    //     {
    //         Debug.LogWarning($"Sound type {sound.ToString()} not found in sound clips");
    //         return;
    //     }
    //
    //     PlaySound(_reusableSoundClips[(int) sound], volumeMod);
    // }
    //
    // public void PlaySound(AudioClip clip, float volumeMod)
    // {
    //     if (volumeMod <= 0f) return;
    //     if (clip != null) SfxPlayer.PlayOneShot(clip, volumeMod);
    // }
}
