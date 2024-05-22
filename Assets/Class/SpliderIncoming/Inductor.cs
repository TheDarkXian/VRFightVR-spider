using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inductor : MonoBehaviour {
    public UnityAction<GameObject> invokeOnEntry;
    public UnityAction<GameObject> invokeOnExit;
    private void OnTriggerEnter(Collider other)
    {
        try
        {
            invokeOnEntry.Invoke(other.gameObject);

        }
        catch { 
        
        }
        }
    private void OnTriggerExit(Collider other)
    {
        try
        {
            invokeOnExit.Invoke(other.gameObject);

        }
        catch { 
        
        }
    
        }
}
