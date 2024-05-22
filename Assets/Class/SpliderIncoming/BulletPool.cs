using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : SingletionMono<BulletPool> {

   [SerializeField]
    Pool pool;
    private void Start()
    {
        pool.PoolIni();
    }

    public  Bullet PoolIni(Vector3 dir,Vector3 pos,string target,float speed=1,int damage=1)
    {
        GameObject bulletobj= pool.SpawnOBJ(pos);
        Bullet bullet = bulletobj.GetComponent<Bullet>();
        bullet.IniBullet(dir,pos,target,speed,damage);
        return bullet;
    }
    public void ReturnPool(Bullet obj) {

        pool.AppendQueues(obj.gameObject);
    }

}
