using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Used by the generic Persistent class to be able to hold them in a list for bulk saving and loading.
/// </summary>
public interface IPersInterface
{
    void Load();
    void Save();
}

/// <summary>
/// Makes a variable persistent, this will create a json file for this variable. It's not preferable to use for basic variables.
/// Best method is to use structs as the generic type. Example: 
/// 
/// [System.Serializable]
/// public struct ProgressData{
///     //data here
/// }
/// 
///  //Inside a class
/// public Persistent<ProgressData> progressData = new Persistent<ProgressData>("progressData");
/// 
/// The given string parameter is the name of the json file, it should be unique for each persistent object.
/// Otherwise they'll overrite each other and most likely will error due to type conflict.
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// 
[System.Serializable]
public class Persistent<T> : IPersInterface
{
    [SerializeField] public T data;
    [SerializeField] private string name;

    public Persistent(string name)
    {
        if(name==""){
            return;
        }
        this.name = name;
        PersistenceManager.allPersistents.Add(this);
    }
    
  
    public void Load(){
        if(!File.Exists(Application.persistentDataPath + "/"+name+"Save.json")){
            Save();
        }
        using (StreamReader sr = File.OpenText(Application.persistentDataPath + "/"+name+"Save.json"))
        {
            string json = "";
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                json += s;
            }
            data = (T)JsonUtility.FromJson(json,typeof(T));
        }
    }

    public void Save(){
        string dataString = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/"+name+"Save.json", dataString);
    
    }

}

/// <summary>
/// Holds every persistent object to be able to save them all or load them all. 
/// </summary>

public static class PersistenceManager{
    public static List<IPersInterface> allPersistents = new List<IPersInterface>();


    /// <summary>
    /// Loads every json data to the game.
    /// </summary>
    public static void LoadAll(){
        foreach (var item in allPersistents)
        {
            item.Load();
        }
    }

    /// <summary>
    /// Saves all persistent objects to json.
    /// </summary>
    public static void SaveAll(){
        foreach (var item in allPersistents)
        {
            item.Save();
        }
    }
}
