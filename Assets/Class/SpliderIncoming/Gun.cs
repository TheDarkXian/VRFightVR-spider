using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : Weapon {

    public Transform FireHole;
    public GameObject ShowMesh;

   [Space]
    public float BulletSpeed;
    public int BulletDamga;

    [Space]
    public float fireinterval;
    public int Maxbullet;
    public int bulletNums;
    public GameObject Bullet;
    public UnityEvent invokeOnFire;

    [SerializeField]
    Animator gunAnimator;
    [SerializeField]
    string InvokeParname;
    [SerializeField]
   AnimationClip invokeanimationclip;
    [SerializeField]
    AudioClip fireaudioclip;
    [SerializeField]
    bool alinEffect;
    [SerializeField]
    ParticleSystem effect;

    float invokeanimationspeed {
        get {
            if (invokeanimationclip == null) {
                return 0;
            }
            return invokeanimationclip.length;
        }
    }

    bool isOnFire;
    bool allowFire;

    Sandclock openfiresandclock;
    private void OnDisable()
    {
        try
        {
            ShowMesh.gameObject.SetActive(false);

        }
        catch { 
        
        }
    }
    private void OnEnable()
    {
    }
    private void Awake()
    {
        openfiresandclock = new Sandclock(fireinterval,false);

    }
    public void switchGun() { 
    
    
        ShowMesh.gameObject.SetActive(true);

    }
    // Use this for initialization
    void Start () {

        bulletNums = Maxbullet;
        openfiresandclock.OnSanded .AddListener(AutoFire);
        openfiresandclock.FullSand();
        if (alinEffect) {
            foreach (ParticleSystem i in effect.transform.GetComponentsInChildren<ParticleSystem>())
            {

                ParticleSystem.MainModule f = i.main;
                f.simulationSpeed = fireinterval;
            }
            ParticleSystem.MainModule temp = effect.main;
            temp.simulationSpeed = fireinterval;
        }
        effect.Play();
        effect.gameObject.SetActive(false);
        ShowMesh.gameObject.SetActive(false);
        
    }
    public void FireBullet() {
        //   GameObject bullet = GameObject.Instantiate(Bullet, FireHole);
        Vector3 FlyDir = FireHole.forward;
        BulletPool.Instance.PoolIni 
            (
            FlyDir, FireHole.position,
            TagManger.Instance.Enemy,BulletSpeed,BulletDamga
            );
        EffectPool.Instance.SpawnaudiofireIni(FireHole.position,fireaudioclip);
        //bullet.transform.parent = null;
    }
    public void AutoFire() {

        if (isOnFire)
        {
            if (bulletNums <= 0)
            {
                StopFire();
            }
            else
            {
                bulletNums--;
                FireBullet();
                invokeOnFire.Invoke();
            }

        }
        else { 
        
            openfiresandclock.stopSand();

        }



    }
    public void OpenFire() {
        if (!isOnFire) {
            openfiresandclock.startSand();
            isOnFire = true;

            float timemultiple = fireinterval / invokeanimationspeed;

            if (gunAnimator != null) {
                gunAnimator.SetFloat("speed", timemultiple);
                gunAnimator.SetBool(InvokeParname, true);
            }

            effect.gameObject.SetActive(true);
            effect.Play();

        }


    }
    public void StopFire() {

        if (isOnFire) {
            effect.Stop();
            effect.gameObject.SetActive(false);
            isOnFire = false;
            if (gunAnimator) { 
            gunAnimator.SetBool(InvokeParname, false);

            }

        }


    }
    // Update is called once per frame
    void Update () {

        openfiresandclock.UpdateSandclock();

	}
}
