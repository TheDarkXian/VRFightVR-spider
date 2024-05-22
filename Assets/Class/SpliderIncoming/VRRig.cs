using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRRig : MonoBehaviour {


    public GameObject HeadMap;
    public GameObject LeftHandMap;
    public GameObject RightMap;
    public GameObject BodyMap;
    public bool enableMap;
    private GameObject _autalHeadSet;
    private GameObject _lefthandSet;
    private GameObject _RighthandSet;
    private GameObject _BodySet;
    private GameObject HeadRig
    {
        get {
            if (_autalHeadSet == null) {
                _autalHeadSet = VRTK_SDKManager.instance.loadedSetup.actualHeadset;
            }
            return _autalHeadSet;
        }
    
    }
    private GameObject LeftRig {
        get {
            if (_lefthandSet == null) {
            _lefthandSet=
                VRTK_SDKManager.instance.loadedSetup.actualLeftController;
            }
            return _lefthandSet;
        }
    }
    private GameObject RightRig
    {
        get
        {
            if (_RighthandSet == null)
            {
                _RighthandSet =
                    VRTK_SDKManager.instance.loadedSetup.actualRightController;
            }
            return _RighthandSet;
        }
    }
    private GameObject BodyRig {

        get {
            if (_BodySet == null) {

                _BodySet = VRTK_SDKManager.instance.loadedSetup.actualBoundaries;
          
            }
            return _BodySet;

        }

    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (enableMap) {

            MapTr(HeadRig.transform, HeadMap.transform);
            MapTr(LeftRig.transform, LeftHandMap.transform);
            MapTr(RightRig.transform, RightMap.transform);
            MapTr(BodyRig.transform, BodyMap.transform);
        }


    }
	void MapTr(Transform Rig,Transform Map) {
        try
        {
            Map.position = Rig.position;
            Map.rotation = Rig.rotation;
        }
        catch { 
        
        }


	}

}
