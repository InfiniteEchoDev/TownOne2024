using Gley.AllPlatformsSave;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    



   

    private void RefreshUI() 
    {
        Debug.Log("Toggle Value"+ SaveUtil.SavedValues.ToggleValue);
        Debug.Log ("int value" +SaveUtil.SavedValues.IntValue.ToString());
        Debug.Log ("string Value" +SaveUtil.SavedValues.StringValue);
    }

    public void Save()
    {
        SaveUtil.SavedValues.ToggleValue = true;
        SaveUtil.SavedValues.IntValue=10;
        SaveUtil.SavedValues.StringValue = "hey";
        SaveUtil.OnSaveCompleted += RefreshUI;
        SaveUtil.Save();
           
    }

   

    public void Load()
    {
        SaveUtil.OnLoadCompleted += RefreshUI;
        SaveUtil.Load();
    }

    
}
