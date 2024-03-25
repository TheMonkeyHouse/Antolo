using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : ReloadingHitscanAttackingTower {
    public override bool Attack(GameObject target)
    {
        bool didAttack = base.Attack(target);
        
        if (didAttack)
        {
            // get bullet spawn offset
            Vector3 bulletStartOffset = new Vector3(0.0f, 0.5f, 0f);
            bulletStartOffset = sr.transform.rotation*bulletStartOffset;
            GameObject newBullet = Instantiate(Resources.Load<GameObject>("Prefabs/Bullet"), transform);
            newBullet.transform.position = newBullet.transform.position + new Vector3(0.5f, 0.5f, 0f) + bulletStartOffset;
            newBullet.GetComponent<Projectile>().Initialize(target.transform.position + new Vector3(0.5f, 0.5f, 0f), 200f, 0.1f);
        }
        return didAttack;
    }
}