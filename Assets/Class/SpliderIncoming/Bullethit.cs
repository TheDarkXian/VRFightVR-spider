using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullethit : MonoBehaviour {

    [SerializeField]
    ParticleSystem ground;
    [SerializeField]
    ParticleSystem spilder;
    float retrunTime;
    // Use this for initialization
    void Start () {
		

	}
    public void IniEffect(int dex) {
        ParticleSystem temp=null;
        if (dex == 0) {
            temp = ground;
        }
        if (dex == 1) {
            temp = spilder;
        }
        ParticleSystem.MainModule mainModule = temp.main;
        retrunTime = mainModule.duration;
        temp.Play();
        Invoke("RetrunPool", retrunTime);
    }

    public void RetrunPool() {

        EffectPool.Instance.RetrunEffectBulletct(this);
    
    }

}
