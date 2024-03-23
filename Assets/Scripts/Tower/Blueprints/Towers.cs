using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class Towers {

    public static Dictionary<string, TowerBlueprint> towerBlueprints = new Dictionary<string, TowerBlueprint>();
    public static Dictionary<string, Sprite> towerSprites = new Dictionary<string, Sprite>();
    
    public static void LoadTowers(){
        using (StreamReader r = new StreamReader("Assets/GameData/Towers.json"))
        {
            string json = r.ReadToEnd();
            towerBlueprints = JsonConvert.DeserializeObject<Dictionary<string, TowerBlueprint>>(json);
        }
        foreach (string key in towerBlueprints.Keys)
        {
            towerSprites[key] = Resources.Load<Sprite>("TowerSprites/" + key);
        }
    }
}