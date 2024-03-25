using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private SpriteRenderer sr;
    private float projectileSpeed;
    private Vector3 targetPosition;
    private float lifetime;
    private float timeAlive;
    
    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime*projectileSpeed);
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifetime)
        {
            Destroy(this.gameObject);
        }
        if (transform.position == targetPosition)
        {
            Destroy(this.gameObject);
        }
    }
    public void Initialize(Vector3 targetPosition, float projectileSpeed, float lifetime)
    {
        this.targetPosition = targetPosition;
        this.projectileSpeed = projectileSpeed;
        this.lifetime = lifetime;
        // rotate sprite towards target
        float angle = Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x -transform.position.x ) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        sr.transform.rotation = targetRotation;
    }
}