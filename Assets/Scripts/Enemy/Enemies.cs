using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class Enemies {

    public static Dictionary<string, EnemyBlueprint> enemyBlueprints = new Dictionary<string, EnemyBlueprint>();
    
    public static void LoadEnemies(){
        using (StreamReader r = new StreamReader("Assets/GameData/Enemies.json"))
        {
            string json = r.ReadToEnd();
            enemyBlueprints = JsonConvert.DeserializeObject<Dictionary<string, EnemyBlueprint>>(json);
        }
    }
    
}