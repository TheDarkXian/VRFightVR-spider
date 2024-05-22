using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public abstract class ObserveValue<T> {
    private T _value;
    protected event UnityAction<T, T> InvokeOnChanged;
    protected ObserveValue(T ini){
    
    _value = ini;
    InvokeOnChanged+= delegate { };

    }
        public T Value {

        set {

            if (!value.Equals(_value)) {
                _value = value;

                OnChanged(value,_value);
            }


        }
        get {
        return _value; 
        } }
    
        protected virtual void OnChanged(T newvalue,T oldvalue) {

        InvokeOnChanged.Invoke(newvalue,oldvalue);

    }

    protected virtual void SetvalueHide(T set) {
        _value = set;
    }
}

public class MonitoringValue<T> : ObserveValue<T>
{
    public MonitoringValue(T ini) : base(ini)
    {
    }
}
public class SingleFloatEvent:UnityEvent<float> { 


}
public class Sandclock:ObserveValue<float> {
    
    public bool isonsanding {
        get { return IsOnSanding; }
    }
    bool IsOnSanding;
    bool ReSetOnSanded;
    private float sandclock_time;
    private new float Value {
        set { 
        base.Value = value;
        }
        get { 
return        base.Value;
        }
    }
 
    public SingleFloatEvent OnSanding;
    public  UnityEvent OnSanded;
    public Sandclock(float time,bool resetonSanded=true):base(0.0f) {

    sandclock_time = time;
    ReSetOnSanded = resetonSanded;
        OnSanded = new UnityEvent();
        OnSanding = new SingleFloatEvent();
    }
    public void SetSandclockTime(float time) { 
    sandclock_time=time;
    }
    protected override void OnChanged(float newvalue, float oldvalue)
    {
        OnSanding.Invoke(Value);
        if (newvalue >= sandclock_time)
        { 
            OnSanded.Invoke();
            SetvalueHide(Value - sandclock_time); 
            IsOnSanding = !ReSetOnSanded;

        }

    }
    
    public void startSand(bool Reset=false) {
        IsOnSanding = true;
        if (Reset) { 
                SetvalueHide(0.0f);
        }

    }
    
    public void stopSand(bool Reset=false) {
        IsOnSanding = false;    
    if (Reset)
        {
            SetvalueHide(0.0f);
        }
    }
    public void UpdateSandclock() {

    if (IsOnSanding) {
            Value += Time.deltaTime;
        }

    }
    public void FullSand() {

        SetvalueHide(sandclock_time);
    }

}
