using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletionMono<T> :MonoBehaviour where T: MonoBehaviour
{

    private static T _instance;
    public  static T Instance {
        get {
            if (_instance == null) {

              _instance=GameObject.FindObjectOfType<T>();

            }    
            return _instance;
        
        }
    
    
    }

}
