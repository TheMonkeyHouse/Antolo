using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : BaseTower
{
    public override void Die()
    {
        BattlefieldEventManager.instance.OnWallDestroyed(this.gameObject);
        Destroy(this.gameObject);
    }
}