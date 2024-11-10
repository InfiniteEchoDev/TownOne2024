using Gley.AllPlatformsSave;
using System;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;

public static class SaveUtil
{
    public static Action OnSaveCompleted;
    public static Action OnLoadCompleted;
    public static SavedValues SavedValues;
    
    private static void SaveComplete(SaveResult result, string message)
    {
        if (result == SaveResult.Error)
        {
            Debug.LogError(message);
        }
        OnSaveCompleted?.Invoke();
    }
    public static void Save()
    {
        string path = Application.persistentDataPath + "/" + "SaveData";
        Gley.AllPlatformsSave.API.Save(SavedValues, path, SaveComplete, false);
    }
    public static void Load()
    {
        string path= Application.persistentDataPath + "/" + "SaveData";

        Gley.AllPlatformsSave.API.Load<SavedValues>(path, LoadComplete, false);

    }

    private static void LoadComplete(SavedValues data, SaveResult result, string message)
    {
        if (result == SaveResult.Success)
        {
            SavedValues = data;
        }
        if (result == SaveResult.Error || result == SaveResult.EmptyData)
        {
            SavedValues = new SavedValues();
        }
        OnLoadCompleted?.Invoke();
    }
}
