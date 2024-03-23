using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerRange : MonoBehaviour
{
    [SerializeField] private GameObject towerRangeVisualizer;
    public abstract List<GameObject> GetTargets();

    private void Awake()
    {
        towerRangeVisualizer.SetActive(false);
    }
    public void SetRadius(float radius)
    {
        gameObject.GetComponent<CircleCollider2D>().radius = radius;
        towerRangeVisualizer.transform.localScale = new Vector3(2*radius, 2*radius, 1);
    }

    public void SetVisualizerActive()
    {
        towerRangeVisualizer.SetActive(true);
    }
    public void SetVisualizerDeactive()
    {
        towerRangeVisualizer.SetActive(false);
    }
}
