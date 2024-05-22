using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRTK;
public class VRInputSystem : SingletionMono<VRInputSystem>
{

    public InputControler leftControl;
    public InputControler RightControl;
    public   List<ActionControler> ListKey;
    public Dictionary<KeyCode, ActionControler> keyValuePairs;


    // Use this for initialization
    void Start () {

        SetObverse(leftControl);
        SetObverse(RightControl);
        keyValuePairs = new Dictionary<KeyCode, ActionControler>();
        if (ListKey != null) {
            foreach (ActionControler i in ListKey) {

                keyValuePairs.Add(i.key, i);
            
            }
        
        }

    }
    void SetObverse(InputControler ic) {
        ic.control.TriggerPressed += ic.OnTrrigerPressed;
        ic.control.TriggerReleased += ic.OnTrrigerReleased;
        
        ic.control.GripClicked += ic.OnGirpClickedd;
        ic.control.GripPressed += ic.OnGrripPressed;
        ic.control.GripReleased += ic.OnGrripReleased;

    }
    // Update is called once per frame
    void Update () {

        if (ListKey != null)
        {
            foreach (ActionControler i in ListKey)
            {

                if (Input.GetKeyDown(i.key)) {

                    i.KeyDown.Invoke();
                }
                if (Input.GetKey(i.key))
                {

                    i.KeyPress.Invoke();
                }
                if (Input.GetKeyUp(i.key))
                {

                    i.KeyUp.Invoke();
                }

            }

	}
}
[System.Serializable]
public class InputControler {

    public VRTK_ControllerEvents control;
    public UnityEvent OnTriggerPressed;
    public UnityEvent OnTriggerRelease;
    public UnityEvent OnGripPressed;
    public UnityEvent OnGripRelease;
    public UnityEvent OnGripClicked;
        public void OnTrrigerPressed(object sender, ControllerInteractionEventArgs e)
    {
        OnTriggerPressed.Invoke();

    }
    public void OnTrrigerReleased(object sender, ControllerInteractionEventArgs e)
    {
        OnTriggerRelease.Invoke();

        }
        public void OnGrripPressed(object sender, ControllerInteractionEventArgs e) 
        {
            OnGripPressed.Invoke();
        }
        public void OnGrripReleased(object sender, ControllerInteractionEventArgs e) 
        {
            OnGripRelease.Invoke();
        }
        public void OnGirpClickedd(object sender, ControllerInteractionEventArgs e) {
            OnGripClicked.Invoke();
        }
    }
    [System.Serializable]
    public class ActionControler
    {
        public KeyCode key;
        public UnityEvent KeyDown;
        public UnityEvent KeyPress;
        public UnityEvent KeyUp;

    }

}