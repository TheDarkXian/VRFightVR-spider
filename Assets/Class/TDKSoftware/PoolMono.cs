using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolMono : MonoBehaviour {
    [HideInInspector]
    public Pool targetpool;
    public UnityEvent invokeOnReturn;
    public void ReturnPool()
    {
        invokeOnReturn.Invoke();
        targetpool.AppendQueues(this.gameObject);
    }

}
public interface PoolMonoBehaviour
{

    void BeforeReturnPool();


}
