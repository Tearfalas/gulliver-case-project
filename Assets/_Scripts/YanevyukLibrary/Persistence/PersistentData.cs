using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;



[System.Serializable]
public struct UnlockData{
    [SerializeField]
    public List<bool> unlocks;

}
public class PersistentData : MonoSingleton<PersistentData>
{

    [SerializeField]  public Persistent<UnlockData> unlockData=new Persistent<UnlockData>("unlockData");
    
    void Awake()
    {

        LoadData();
    }


    public void SaveData(){
        PersistenceManager.SaveAll();
    }

    public void LoadData(){
        PersistenceManager.LoadAll();
    }

    public void OpenSavePath()
    {
        string itemPath = Application.dataPath +"/";
        print(itemPath);
        itemPath = itemPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
        System.Diagnostics.Process.Start("explorer.exe", "/root,"+itemPath);
    }

    /// <summary>
    /// Callback sent to all game objects before the application is quit.
    /// </summary>
    void OnApplicationQuit()
    {
        SaveData();
    }
    
}
