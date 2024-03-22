using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerRange : MonoBehaviour
{
    public abstract List<GameObject> GetTargets();

    public void SetRadius(float radius)
    {
        gameObject.GetComponent<CircleCollider2D>().radius = radius;
    }
}
