using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    private List<GameObject> enemiesInRange = new List<GameObject>();

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

    public GameObject GetTarget()
    {
        // targeting function, for now furthest along the track, should be first in list
        if (enemiesInRange.Count == 0)
        {
            return null;
        }
        return enemiesInRange[0];
    }

    public void SetRadius(float radius)
    {
        gameObject.GetComponent<CircleCollider2D>().radius = radius;
    }
}
