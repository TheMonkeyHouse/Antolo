using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public float size;

    // targeting
    // ability
    // bool isFlying
    // sprite

    private List<Vector3> pathing;

    private void Update()
    {
        // move towards next node in path
        // if intersecting with player structure, attack
        Vector3 heading = pathing[0] - transform.position;
        Vector3 direction = heading / heading.magnitude;

        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void GetPath()
    {
        // choose target by probability of inverse distance to center
        // for now just attack base
        pathing = new List<Vector3>();
        pathing.Add(new Vector3(BattlefieldController.instance.width/2f, BattlefieldController.instance.height/2f, 0));
    }

    public void Initialize(int health, float speed, float size)
    {
        this.health = health;
        this.speed = speed;
        this.size = size;
        GetPath();
    }
}
