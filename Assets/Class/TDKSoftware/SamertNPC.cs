using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class SamertNPC : UniversalCharcter
{
    public GameObject Target;

    StateMachine IdleState;
	StateMachine TrackingState;
	StateMachine AtackState;
	StateMachine DeathState;
    Dictionary<NPCState, StateMachine> keyValuePairs;
	public NPCState State;
	NPCState oldState;
    protected override void Awake()
    {
        base.Awake();
        State = NPCState.Idle;
        oldState = State;
        IdleState = new StateMachine(Idle, OnEntryIdle, OnExitIdle);
        TrackingState = new StateMachine(Moveing, OnEntryMoveing, OnExitMoveing);
        AtackState = new StateMachine(Attack, OnEntryAttack, OnExitAttack);
        DeathState = new StateMachine(Death, OnEntryDeath, OnExitDeath);
        keyValuePairs = new Dictionary<NPCState, StateMachine>();
        keyValuePairs.Add(NPCState.Idle, IdleState);
        keyValuePairs.Add(NPCState.Moveing, TrackingState);
        keyValuePairs.Add(NPCState.Attack, AtackState);
        keyValuePairs.Add(NPCState.Death, DeathState);
        health.InvokeOnDead += SetIsDead;
        health.InvokeOnHit += OnGetDamage;
    }
    // Use this for initialization
    protected override void Start () {
		base.Start();


    }
    protected override void OnEnable()
    {
        base.OnEnable();
        State = NPCState.Idle;
        oldState = State;

    }
    protected virtual void OnGetDamage()
    {
 //       Debug.Log("GetHit");


    }
    protected virtual void SetIsDead() {
        State = NPCState.Death;
 //       Debug.Log("Death");

    }
    protected virtual void OnEntryDeath() { }
    protected virtual void OnExitDeath() { }
    protected virtual void Death() { }
    protected virtual void OnEntryAttack() { }
	protected virtual void OnExitAttack() { }
	protected virtual void OnEntryIdle() { 	}
	protected virtual void OnExitIdle() { 	}
	protected virtual void OnEntryMoveing() { }
	protected virtual void OnExitMoveing() { }
	protected virtual void Idle() { 


    }
    protected virtual void Moveing() { 

    }
    protected virtual void Attack() {


    }
	protected virtual bool preceptron() {

		return false;
	}
	protected virtual bool GetAttackAuthorization() { 
	
		return false;
    }
	
    // Update is called once per frame
    protected virtual void Update () {
        //感知目标
        if (State == NPCState.Death)
        {
            
        }
        else {

            if (preceptron())
            {
                if (GetAttackAuthorization())
                {
                    State = NPCState.Attack;
                }
                else { 
                State = NPCState.Moveing;

                }
            }
            else
            {
                State = NPCState.Idle;
            }

        }
		if (oldState != State) { 
		keyValuePairs[oldState].OnOutAction();
		keyValuePairs[State].OnEntryAction();
        }
        keyValuePairs[State].OnStateUpdate();

        oldState = State;

	}


}

public enum NPCState { 
Idle,
Moveing,
Attack,
Death
}
public class StateMachine {
	
	
	UnityAction EntryAction;
	UnityAction stateAction;
	UnityAction OutAction;
    public StateMachine(UnityAction stateaction,UnityAction EntyrAction,UnityAction Out) {

	stateAction += stateaction;
		EntryAction += EntyrAction;
		OutAction += Out;

	}
	public void OnStateUpdate() { 
	stateAction.Invoke();
	}
	public void OnOutAction() {
 OutAction.Invoke();
	}
	public void OnEntryAction() { 
	EntryAction.Invoke();	
	}

}
