//using UnityEngine;
//using UnityEngine.Audio;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

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
    
    [FormerlySerializedAs("_mixer")]
    [Header("Mixer")]
    [SerializeField] private AudioMixer Mixer;
    
    [FormerlySerializedAs("_musicSource")]
    [Header("Sources")]
    [SerializeField] private AudioSource MusicSource;
    
    [FormerlySerializedAs("_sfxSource")] [SerializeField] private AudioSource SfxSource;
    
    [FormerlySerializedAs("_reusableMusicClips")] [Header("Reusable Clips")] [SerializeField]
    private AudioClip[] ReusableMusicClips;
    
    [FormerlySerializedAs("_reusableSoundClips")] [SerializeField] private AudioClip[] ReusableSoundClips;
    
    private AudioSource MusicPlayer
    {
        get
        {
            if (MusicSource == null)
            {
                MusicSource = gameObject.AddComponent<AudioSource>();
                MusicSource.loop = true;
            }
    
            return MusicSource;
        }
    }
    
    private AudioSource SfxPlayer
    {
        get
        {
            if (SfxSource == null) SfxSource = gameObject.AddComponent<AudioSource>();
            return SfxSource;
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
        Mixer.SetFloat("MasterVol", DbFromNormalized(GlobalVolume));
        Mixer.SetFloat("MusicVol", DbFromNormalized(MusicVolume));
        Mixer.SetFloat("SfxVol", DbFromNormalized(SfxVolume));
    }

    private float DbFromNormalized(float volumeNormalized)
    {
        var dbVolume = Mathf.Log10(volumeNormalized) * 20;
        if (volumeNormalized == 0.0f)
        {
            dbVolume = -80.0f;
        }
        return dbVolume;
    }
    
    public void PlayMusic(MusicTypes music, float volumeMod)
    {
        var index = (int) music;
        if (ReusableMusicClips.Length < index)
        {
            Debug.LogWarning($"Music type {music.ToString()} not found in music clips");
            return;
        }
    
        PlayMusic(ReusableMusicClips[(int) music], volumeMod);
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
        if (ReusableSoundClips.Length < index)
        {
            Debug.LogWarning($"Sound type {sound.ToString()} not found in sound clips");
            return;
        }
    
        PlaySound(ReusableSoundClips[(int) sound], volumeMod);
    }
    
    public void PlaySound(AudioClip clip, float volumeMod = 1f)
    {
        if (volumeMod <= 0f) return;
        if (clip != null) SfxPlayer.PlayOneShot(clip, volumeMod);
    }
}
