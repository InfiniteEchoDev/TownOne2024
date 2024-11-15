using System;
using UnityEngine.Serialization;

[Serializable]
public class SavedValues
{
    public float GlobalVolume = 0.5f;
    public float MusicVolume = 0.5f;
    public float SfxVolume = 0.5f;
    
    [FormerlySerializedAs("Score")] 
    public int HighScore;
    // todo
    public string Name = "";
    // todo
    public float Time;
}