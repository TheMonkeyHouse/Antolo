using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportTowerRange : TowerRange
{
    private List<GameObject> towersInRange = new List<GameObject>();

    private void Awake()
    {
        BattlefieldEventManager.instance.TowerDestroyed += TowerDestroyed;
    }
    
    private void OnTriggerEnter2D(Collider2D col){
        if (!(col.gameObject.CompareTag("Tower") || col.gameObject.CompareTag("Wall")))
        {
            return;
        }
        towersInRange.Add(col.gameObject);
    }
    private void OnTriggerExit2D(Collider2D col){
        if (!(col.gameObject.CompareTag("Tower") || col.gameObject.CompareTag("Wall")))
        {
            return;
        }
        towersInRange.Remove(col.gameObject);
    }

    public override List<GameObject> GetTargets()
    {
        return new List<GameObject>(towersInRange);
    }

    private void TowerDestroyed(GameObject tower)
    {
        this.towersInRange.Remove(tower);
    }
}
