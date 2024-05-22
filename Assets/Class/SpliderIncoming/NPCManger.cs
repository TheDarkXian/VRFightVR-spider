using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCManger : SingletionMono<NPCManger> {
    
    [System.Serializable]
    public class NPCspawn
    {
        [SerializeField]
        Pool pool;
        public int MaxSpawnNum;
        public int MaxExitNum;
        public int AreadySpawnNum;
        public int CurNum;
        public Transform spawnPos;
        public float spawnTime;
        Sandclock sandclock;
        public bool isonspawn { get { return sandclock.isonsanding; } }
        public void iniNPCspawn() {
            pool.PoolIni();
            sandclock = new Sandclock(spawnTime,false);
            sandclock.OnSanded.AddListener(SpawnOne);
            CurNum=0;
        }
        public void StartSpawn() {
            
            sandclock.startSand();
        
         }
        public void StopSpawn() {

            sandclock.stopSand();
        }
        public void SpawnOne() {

            if (AreadySpawnNum<MaxSpawnNum) {

                if (CurNum < MaxExitNum)
                {
                    CurNum++;
                    AreadySpawnNum++;
                    GameObject gobj = pool.SpawnOBJ(spawnPos.position);
                    PoolMono poolMono = gobj.GetComponent<PoolMono>();
                    poolMono.targetpool = pool;
                    try
                    {
                        poolMono.invokeOnReturn.RemoveListener(OBJdeath);
                    }
                    catch { 
                    }
                    poolMono.invokeOnReturn.AddListener(OBJdeath);
                    ;
                }
                else
                {
                    StopSpawn();
                }
            }
        }
        public void OBJdeath() {
            CurNum--;
            GameLevelControl.Instance.KillerAdd();
            if (CurNum == 0&&AreadySpawnNum==MaxSpawnNum) {
                GameLevelControl.Instance.GameOver();
            }
        }
        public void ClearTarget() {

            foreach (GameObject i in pool.objList) {
                SamertNPC universalCharcter = i.GetComponent<SamertNPC>();
                universalCharcter.Target = null;
            }

        }
        public void UpdateNPCsapwn() {
            sandclock.UpdateSandclock();
        }
    }
    [SerializeField]
    NPCspawn spiderspawn;
    private void Awake()
    {
        spiderspawn.iniNPCspawn();

    }
    // Use this for initialization
    void Start () {
		
	}
    public void ClearAllNPCTarget() {
        spiderspawn.ClearTarget();
    
    }
    public void Spawnflip() {

        if (spiderspawn.isonspawn)
        {
            spiderspawn.StopSpawn();
        }
        else {
            spiderspawn.StartSpawn();
        }
    
    }
	// Update is called once per frame
	void Update () {

        spiderspawn.UpdateNPCsapwn();
    
	}
}

