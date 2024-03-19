using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    private List<GameObject> enemiesInRange = new List<GameObject>();

    private void Awake()
    {
        BattlefieldEventManager.instance.EnemyDestroyed += EnemyDestroyed;
    }
    
    private void OnTriggerEnter2D(Collider2D col){
        if (!col.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        enemiesInRange.Add(col.gameObject);
    }
    private void OnTriggerExit2D(Collider2D col){
        if (!col.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        enemiesInRange.Remove(col.gameObject);
    }

    public List<GameObject> GetTargets()
    {
        return new List<GameObject>(enemiesInRange);
    }

    private void EnemyDestroyed(GameObject enemy)
    {
        this.enemiesInRange.Remove(enemy);
    }

    public void SetRadius(float radius)
    {
        gameObject.GetComponent<CircleCollider2D>().radius = radius;
    }
}
