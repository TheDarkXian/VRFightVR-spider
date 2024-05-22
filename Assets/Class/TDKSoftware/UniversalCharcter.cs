using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UniversalCharcter : MonoBehaviour ,GetDamage{
	[SerializeField]
	public CharacterAttribute characterAttribute;
    [SerializeField]
     public Health health;
    [SerializeField]
    protected float curMoveSpeed;
    public UnityEvent InvoeOnDeathEnd;
    
    public void Hit(int value)
    {
        health.GetHit(value);    

    }



    protected virtual void Awake() {
		health = new Health(characterAttribute.maxHealth);

	}
    protected virtual void OnEnable()
    {
        curMoveSpeed = characterAttribute.maxmoveSpeed;
        health.Reset();
    }
    // Use this for initialization
    protected virtual void Start () {

        curMoveSpeed = characterAttribute.maxmoveSpeed;


	}
	// Update is called once per frame
	void Update () {
    
	}
}
[System.Serializable]
public struct CharacterAttribute {
	public int maxHealth;
	public float maxmoveSpeed;
	public int Power;
}

public interface GetDamage {

	void Hit(int value);

}
[System.Serializable]
public class Health:ObserveValue<int> {
	public event UnityAction InvokeOnHit;
	public event UnityAction InvokeOnDead=delegate { };
    public SingleFloatEvent OnValueChanged;
    int MaxHealth;
    [SerializeField]
     int curHealth;
	public int CurHealth { 
	get { return Value; }
	}
	public Health(int value) :base(value){

        MaxHealth = value;
        curHealth = MaxHealth;
        OnValueChanged = new SingleFloatEvent();
    }
    protected override void OnChanged(int newvalue,int oldvalue)
    {
        curHealth = newvalue;
        OnValueChanged.Invoke(newvalue);
            if (newvalue <= 0) { 
			InvokeOnDead.Invoke();
			}
            
    }

    public void GetHit(int value) {
		int DeduceValue = Value - value;
		if (DeduceValue < 0) {
			DeduceValue = 0;
		}
        if (InvokeOnHit != null) {
            InvokeOnHit.Invoke();

        }

        Value = DeduceValue;
    }
	public void SetHeal(int value) {
		Value = value;
	}
    public void Reset() {

        SetvalueHide(MaxHealth);
        curHealth = MaxHealth;
    }
    public float Process() {
    float val=(float)CurHealth / (float)MaxHealth;
        return val;
    }

}

