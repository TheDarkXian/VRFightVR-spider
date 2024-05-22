using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : SingletionMono<EffectPool> {

    
    [SerializeField]
    Pool audiopool;
    [SerializeField]
    Pool Effectpool;

    // Use this for initialization
    void Start () {
        audiopool.PoolIni();
        Effectpool.PoolIni(); 
    }
    public GameObject SpawnEffect(Vector3 pos,int dex) {
        GameObject obj = Effectpool.SpawnOBJ(pos);
        Bullethit bullethit = obj.GetComponent<Bullethit>();
        bullethit.IniEffect(dex);
        return obj;

    }
    public void RetrunEffectBulletct(Bullethit obj) {

        Effectpool.AppendQueues(obj.gameObject);
    

    }
    public Dynamicambientsound SpawnaudiofireIni(Vector3 pos,AudioClip audioClip) {
           GameObject obj= audiopool.SpawnOBJ(pos);
        Dynamicambientsound das = obj.GetComponent<Dynamicambientsound>();
        das.iniDAS(audioClip,pos);
        return das;
    }

    public void AudioReturnPool(Dynamicambientsound obj) {

        audiopool.AppendQueues(obj.gameObject);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
