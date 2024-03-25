using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunTower : HitscanAttackingTower 
{
    private bool shootRightGun;
    public override bool Attack(GameObject target)
    {
        bool didAttack = base.Attack(target);
        if (didAttack)
        {
            // get bullet spawn offset
            Vector3 bulletStartOffset = new Vector3(0f, 0.3f, 0f);
            
            if (shootRightGun)
            {
                bulletStartOffset += new Vector3(0.25f, 0f, 0f);
            }
            else 
            {
                bulletStartOffset += new Vector3(-0.25f, 0f, 0f);
            }
            shootRightGun = !shootRightGun;
            // rotate vector to match tower transform
            bulletStartOffset = sr.transform.rotation*bulletStartOffset;
            GameObject newBullet = Instantiate(Resources.Load<GameObject>("Prefabs/Bullet"), transform);
            newBullet.transform.position = newBullet.transform.position + new Vector3(0.5f, 0.5f, 0f) + bulletStartOffset;
            newBullet.GetComponent<Projectile>().Initialize(target.transform.position + new Vector3(0.5f, 0.5f, 0f), 100f, 0.1f);
        }
        return didAttack;
    }
}