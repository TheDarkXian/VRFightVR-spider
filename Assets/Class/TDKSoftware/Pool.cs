using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class Pool {
    
    public GameObject PreSet;
    public Transform ParentTr;
    Queue<GameObject> EmptyQueue;
    public List<GameObject> objList;
    public void PoolIni() { 
        EmptyQueue = new Queue<GameObject>();

    }

    public  GameObject SpawnOBJ(Vector3 pos)
{
    GameObject obj;
    if (EmptyQueue.Count <= 0)
    {

        obj = GameObject.Instantiate(PreSet,pos,Quaternion.identity, ParentTr);
            objList.Add(obj);
        }
    else
    {

        obj = EmptyQueue.Dequeue();
        obj.transform.position = pos;
        obj.transform.rotation = Quaternion.identity;
            obj.gameObject.SetActive(true);
    }
    return obj;
}

    public void AppendQueues(GameObject OBJ)
{

        if (EmptyQueue.Contains(OBJ))
        {

        }
        else
        {
            EmptyQueue.Enqueue(OBJ);
            OBJ.gameObject.SetActive(false);
        }
}

    }

