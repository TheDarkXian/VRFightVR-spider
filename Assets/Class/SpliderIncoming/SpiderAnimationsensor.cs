using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpiderAnimationsensor : MonoBehaviour {

    public UnityAction AttackActive;
    public UnityAction AttackOneEnd;
    public UnityAction OnDeathEnd;
    public void HitEnemy() {

        AttackActive.Invoke();
    
    }
    public void HitEnd() {

        try
        {
            AttackOneEnd.Invoke();

        }
        catch { 
        }

    }

    public void DeathEnd() {
        OnDeathEnd.Invoke();
    
    }

}
