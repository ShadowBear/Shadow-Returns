using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonLoader : MonoBehaviour
{
    string path;
    string jsonString;

    private void Start()
    {
        path = Application.streamingAssetsPath + "/Creature.json";
        jsonString = File.ReadAllText(path);
        Creature Yumo = JsonUtility.FromJson<Creature>(jsonString);
        //Debug.Log(Yumo.Level);
        //Yumo.Level = 25;
        //Debug.Log(Yumo.Level);
        string newYumo = JsonUtility.ToJson(Yumo);
        //Debug.Log(newYumo);
    }
}

[System.Serializable]
public class Creature
{
    public string Name;
    public int Level;
    public int[] Stats;


}