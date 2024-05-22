using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : UniversalCharcter {
    [SerializeField]
    Transform Guns;
    [SerializeField]
    Text infotext;

    List<Gun> ListGun;
    Gun curGun;
    int gundex;
   protected override void Awake()
    {
        base.Awake();
        ListGun = new List<Gun>();

    }
    // Use this for initialization
    protected override void Start () {
        base.Start();

        foreach (Gun I in Guns.GetComponentsInChildren<Gun>(true)) {
            ListGun.Add(I);
            I.gameObject.SetActive(false);
        }

        gundex = 0;
        curGun = ListGun[gundex];
        curGun.gameObject.SetActive(true);

        VRInputSystem.Instance.RightControl.OnTriggerPressed.AddListener(OpenFire);
        VRInputSystem.Instance.RightControl.OnTriggerRelease.AddListener(StopFire);

        VRInputSystem.Instance.RightControl.OnGripClicked.AddListener(NextGun);

        health.InvokeOnDead += PlayerDeath;
        UpdateInfo();
    }
    public void UpdateInfo() {
        string info = "";
        info += "Bullect :"+curGun.bulletNums;
        info += "   "+GameLevelControl.Instance.KillerNum;
        infotext.text = info;
    
    }
    public void NextGun() {
        gundex += 1;
        gundex %= ListGun.Count;
        curGun.invokeOnFire.RemoveListener(UpdateInfo);
        SwitchGun(gundex);
        curGun.switchGun();
        curGun.invokeOnFire.AddListener(UpdateInfo);
    }
    public void SwitchGun(int dex) {
        
        StopFire();
        curGun.gameObject.SetActive(false);
        curGun = ListGun[gundex];
        curGun.gameObject.SetActive(true);    
    
     }
    public void OpenFire() {

        curGun.OpenFire();
    }
    public void StopFire() {

        curGun.StopFire();
    }
    public void PlayerDeath() {

        InvoeOnDeathEnd.Invoke();
    
    }
	// Update is called once per frame
	void Update () {
		
	}
}
