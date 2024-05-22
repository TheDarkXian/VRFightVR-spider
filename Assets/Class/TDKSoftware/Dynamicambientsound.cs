using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(AudioSource))]
public class Dynamicambientsound : MonoBehaviour {

    [SerializeField]
    AudioSource audiosoucre;
    Sandclock lifeTime;
    Sandclock returnTime;
    public void iniDAS(AudioClip audioClip,Vector3 pos) {

            

        audiosoucre.clip = audioClip;
        transform.position = pos;
        returnTime.SetSandclockTime(audioClip.length);
        returnTime.startSand(true);
        audiosoucre.Play();

    }
    private void Awake()
    {
        returnTime = new Sandclock(0);
        audiosoucre = transform.GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        
        audiosoucre.loop = false;
        audiosoucre.playOnAwake = false;
        returnTime.OnSanded.AddListener(ReturnPool);

	}
    void ReturnPool() {
        EffectPool.Instance.AudioReturnPool(this);
    
    }
	// Update is called once per frame
	void Update () {

        returnTime.UpdateSandclock();

	}
}
