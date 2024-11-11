using UnityEngine;

public class SaveMgr : Singleton<SaveMgr>
{
    
    public void Save(int score)
    {
        //SaveUtil.SavedValues.Score = score;
        //SaveUtil.Save();

    }

    public void Load()
    {
        //SaveUtil.OnLoadCompleted += RefreshUI;
        //SaveUtil.Load();
    }
}
