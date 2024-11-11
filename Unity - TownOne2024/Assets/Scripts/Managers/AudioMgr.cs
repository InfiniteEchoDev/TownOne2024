//using UnityEngine;
//using UnityEngine.Audio;

using UnityEngine;
using UnityEngine.Audio;

public class AudioMgr : Singleton<AudioMgr>
{
    public enum MusicTypes
    {
        Login = 0,
        MainMenu = 1,
        RunGameplay = 2,
        LevelVictory=3,
        

    }
    
    public enum SoundTypes
    {
        ButtonSelect = 0, //buttons in button prefab
        ButtonHover = 1,
        SortingError = 2,  // on mother ship for incorect items
        GameOver = 3,     //whwen a player runs out of lives
        CorrectObject= 4,  // on mothership for correct items
        SpawnPerson=5,     //on spawn script for when people spawn
        PlayerLoseLife=6,  //in player ship after colison with astroids/obsticals
        PersonSaved=7,     // in mothership for person
        PersonPickedUp=8,  // in collection code 
        ButtonExit=9,     // in settings prefab/exit game
        PersonSortingError=10, //in mothership for wrong location
        DropItems=11,     // in player controls
        ItemSpawn=12,     // in spawner code 
        ItemPickup=13,     // in collection code
        PersonDespawn=14,   // In pickup despawn code
        PersonDying=15      // in pickup despawn code
    }
    
    [Header("Mixer")]
    [SerializeField] private AudioMixer _mixer;
    
    [Header("Sources")]
    [SerializeField] private AudioSource _musicSource;
    
    [SerializeField] private AudioSource _sfxSource;
    
    [Header("Reusable Clips")] [SerializeField]
    private AudioClip[] _reusableMusicClips;
    
    [SerializeField] private AudioClip[] _reusableSoundClips;
    
    private AudioSource MusicPlayer
    {
        get
        {
            if (_musicSource == null)
            {
                _musicSource = gameObject.AddComponent<AudioSource>();
                _musicSource.loop = true;
            }
    
            return _musicSource;
        }
    }
    
    private AudioSource SfxPlayer
    {
        get
        {
            if (_sfxSource == null) _sfxSource = gameObject.AddComponent<AudioSource>();
            return _sfxSource;
        }
    }
    
    /// Read and save from player prefs
    public float GlobalVolume
    {
        set => SaveUtil.SavedValues.GlobalVolume = value;
        get => SaveUtil.SavedValues.GlobalVolume;
    }
    
    public float MusicVolume
    {
        set => SaveUtil.SavedValues.MusicVolume = value;
        get => SaveUtil.SavedValues.MusicVolume;
    }
    
    public float SfxVolume
    {
        set => SaveUtil.SavedValues.SfxVolume = value;
        get => SaveUtil.SavedValues.SfxVolume;
    }
    
    private void Start()
    {
        DontDestroyOnLoad(this);
        SaveUtil.OnLoadCompleted += OnDataLoadComplete;
        SaveUtil.Load();
    }
    
    private void OnDataLoadComplete()
    {
        SaveUtil.OnLoadCompleted -= OnDataLoadComplete;
    
        UpdateVolumeFromSaveData();
    }
    
    public void UpdateVolumeFromSaveData()
    {
        _mixer.SetFloat("MasterVol", GlobalVolume);
        _mixer.SetFloat("MusicVol", MusicVolume);
        _mixer.SetFloat("SfxVol", SfxVolume);
    }
    
    public void PlayMusic(MusicTypes music, float volumeMod)
    {
        var index = (int) music;
        if (_reusableMusicClips.Length < index)
        {
            Debug.LogWarning($"Music type {music.ToString()} not found in music clips");
            return;
        }
    
        PlayMusic(_reusableMusicClips[(int) music], volumeMod);
    }
    
    public void PlayMusic(AudioClip clip, float volumeMod)
    {
        if (volumeMod <= 0f) return;
        MusicPlayer.clip = clip;
        MusicPlayer.volume = volumeMod;
        MusicPlayer.loop = true;
        MusicPlayer.Play();
    }
    
    public void PauseMusic()
    {
        MusicPlayer.Pause();
    }
    
    public void ResumeMusic()
    {
        MusicPlayer.Play();
    }
    
    public void PlayOneShotMusic(AudioClip clip, float volumeMod)
    {
        MusicPlayer.Stop();
        if (volumeMod <= 0f) return;
        MusicPlayer.PlayOneShot(clip, volumeMod);
    }
    
    public void PlaySoundNoOverlap(SoundTypes sound, float volumeMod)
    {
        if (!SfxPlayer.isPlaying)
            PlaySound(sound, volumeMod);
    }
    
    public void PlaySound(SoundTypes sound, float volumeMod = 1f)
    {
        var index = (int) sound;
        if (_reusableSoundClips.Length < index)
        {
            Debug.LogWarning($"Sound type {sound.ToString()} not found in sound clips");
            return;
        }
    
        PlaySound(_reusableSoundClips[(int) sound], volumeMod);
    }
    
    public void PlaySound(AudioClip clip, float volumeMod = 1f)
    {
        if (volumeMod <= 0f) return;
        if (clip != null) SfxPlayer.PlayOneShot(clip, volumeMod);
    }
}
