using System;
using System.IO;
using UnityEngine;

public static class SaveUtil
{
    private const string SaveFileName = "SaveData.json"; 
    public static Action OnSaveCompleted;
    public static Action OnLoadCompleted;
    public static SavedValues SavedValues;
    
    public static void Save()
    {
        string path = Path.Combine(Application.persistentDataPath, SaveFileName); // Application.persistentDataPath + "/" + "SaveData";
        string data = JsonUtility.ToJson(SavedValues);
        File.WriteAllText(path,data);
        OnSaveCompleted?.Invoke();
    }
    
    public static void Load()
    {
        string path = Path.Combine(Application.persistentDataPath, SaveFileName);
        string data = File.ReadAllText(path);
        SavedValues = JsonUtility.FromJson<SavedValues>(data) ?? new SavedValues();
        OnLoadCompleted?.Invoke();
    }
}
