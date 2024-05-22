using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour {
    [SerializeField]
    Vector3 flyDir;
    float FlySpeed;
    int Damage;
    Rigidbody rig;
    string targetTag;
    public TrailRenderer trailRenderer;
    public void IniBullet(
    Vector3 Dir,Vector3 pos,string target, float speed = 1, int damage = 1, float retrunTime=10.0f
    )
    {
        flyDir = Dir;
        FlySpeed = speed;
        Damage = damage;
        targetTag = target;
        transform.position = pos;
        transform.forward = Dir;
        Invoke("Miss",retrunTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        int dex = 0;
        GameObject obj = collision.gameObject;
        if (obj.tag == targetTag)
        {
            GetDamage getDamage = obj.GetComponent<GetDamage>();
            getDamage.Hit(Damage);
            dex = 1;
            }
        else {

            dex = 0;

        }
        EffectPool.Instance.SpawnEffect(transform.position, dex);
        Miss();
    }
    public void Miss() {
        rig.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        trailRenderer.Clear();
        BulletPool.Instance.ReturnPool(this);
        if (IsInvoking())
        {
            CancelInvoke();
        }
    }
    // Use this for initialization
    void Start () {
        rig = GetComponent<Rigidbody>();
     trailRenderer = GetComponent<TrailRenderer>();

    }
    private void OnDrawGizmos()
    {
        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = flyDir;
        Gizmos.DrawRay(ray);

    }
    // Update is called once per frame
    void Update () {

        rig.velocity = flyDir * FlySpeed;

	}
}
