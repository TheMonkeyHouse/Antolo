using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int health;
    public float damage;
    public float attackSpeed;

    // targeting
    // ability
    // bool isAOE
    // sprite
    // cooldown

    private void Update()
    {
        // move towards next node in path
        // if intersecting with player structure, attack
    }

    private void GetPath()
    {

    }

    public void Initialize(int health, float damage, float attackSpeed)
    {
        this.health = health;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
    }
}
