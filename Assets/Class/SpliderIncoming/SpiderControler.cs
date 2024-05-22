using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderControler : SamertNPC, PoolMonoBehaviour
{
    public bool FindTarger;
    public Animator animator;
    public Inductor InductorRange;
    public Inductor AttackTange;
     Collider Cupuslecollider;
   SpiderAnimationsensor spiderAnimationsensor;
    public Rigidbody rig;
    public bool enableGIZMO;
    public HandCast castOne;
    public HandCast castTwo;
    
    public Transform castHead;
    public float attackdistance;
    public float attackdistance_Expend;
    

    [System.Serializable]
    public class HandCast {
    [SerializeField]
         Transform upTouch;
        [SerializeField]
         Transform downTouch;

         public Vector3 up {
            get {return upTouch.position; }
         }
         public Vector3 down {
            get { return downTouch.position; }
         }
        public float raduios;
    }
   protected  override void Awake()
    {
        base.Awake();
        spiderAnimationsensor = animator.gameObject.GetComponent<SpiderAnimationsensor>();
        Cupuslecollider = GetComponent<Collider>();
        rig = GetComponent<Rigidbody>();
    }
    // Use this for initialization
    protected override void Start () {
		base.Start ();

        spiderAnimationsensor.AttackActive += HitTarget;
        spiderAnimationsensor.OnDeathEnd += OnExitDeath;
        spiderAnimationsensor.OnDeathEnd += ActionOnDeath;

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (Cupuslecollider!=null) 
        {
        Cupuslecollider.enabled =true;

        }
    }

    public void ActionOnDeath() {

        Cupuslecollider.enabled = false;
        InvoeOnDeathEnd.Invoke();

    }
    protected override void Moveing()
    {
        base.Moveing();
        Vector3 TargetPos = Target.transform.position;
        TargetPos.y = transform.position.y;
        Vector3 movedir = TargetPos-transform.position;

        if (movedir.magnitude < 1f)
        {

        }
        else {
            movedir = movedir.normalized;
            if (Vector3.Angle(transform.forward, movedir) >= 90)
            {
                AimTarger(0.8f);
                rig.velocity = Vector3.zero;
            }
            else 
            {
                if (rig)
                {
                    rig.velocity = transform.forward * curMoveSpeed;
                }
                else
                {
                    transform.position += transform.forward * curMoveSpeed * Time.deltaTime;
                }
                AimTarger(0.5f);
            }
        }

  

    }

    private void AimTarger(float value) 
    {
        Vector3 pos = transform.position;
        Vector3 TargetPos = Target.transform.position;
        TargetPos.y = pos.y;
        Vector3 dir = TargetPos - pos;
        transform.forward = Vector3.Lerp(transform.forward, dir,value*Time.deltaTime);

    }
    protected void HitTarget() {

        HandCast temp = castOne;
        RaycastHit [] castoneList=
        Physics.CapsuleCastAll(temp.up, temp.down, temp.raduios, temp.up - temp.down);
       temp = castTwo;
        RaycastHit[] casttwoList =
        Physics.CapsuleCastAll(temp.up, temp.down, temp.raduios, temp.up - temp.down);

        List<RaycastHit> raycastHits = new List<RaycastHit>();
        raycastHits.AddRange(casttwoList);
        raycastHits.AddRange(castoneList);
        foreach (RaycastHit cast in raycastHits) {
            GameObject obj = cast.collider.gameObject;
            if (obj.tag == TagManger.Instance.Player) {
               
                GetDamage getDamage = obj.GetComponent<GetDamage>();
                getDamage.Hit(this.characterAttribute.Power);
            
            }
        }

    }
    protected override void Attack()
    {
        base.Attack();



    }
    protected override bool GetAttackAuthorization()
    {
        bool isclose = false;
            float distance = Vector3.Distance(castHead.position, Target.transform.position);
            if (distance <= attackdistance)
            {
                isclose = true;
            }
        if (State == NPCState.Attack)
        {

            if (distance <= attackdistance + attackdistance_Expend) { 
                isclose = true;


            }

        }


        return isclose;
    }
    protected override bool preceptron()
    {
        bool findTarger = false;
        if (Target != null)
        {
            findTarger = true;
            Vector3 taggerpos = Target.transform.position;
            taggerpos.y = 0;
            float distance = Vector3.Distance(taggerpos, castHead.position);
            if (distance < 1.0f && GetAttackAuthorization() == false)
            {
                findTarger = false;
            }
        }
        else {
            Target = GameLevelControl.Instance.GetAttackTarget();
        
        }

        return findTarger;
    }
    private void OnDrawGizmos()
    {
        if (enableGIZMO) {
            if (Vector3.Distance(castHead.position, Target.transform.position) < attackdistance)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.blue;
            }
            Gizmos.DrawWireSphere(castHead.position, attackdistance);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(castHead.position, attackdistance + attackdistance_Expend);
            Gizmos.DrawLine(transform.position, Target.transform.position);
            Gizmos.DrawWireSphere(castOne.up, castOne.raduios);
            Gizmos.DrawWireSphere(castOne.down, castOne.raduios);
            Gizmos.DrawWireSphere(castTwo.up, castTwo.raduios);
            Gizmos.DrawWireSphere(castTwo.down, castTwo.raduios);
        }
   

    }
    // Update is called once per frame
    protected override void Update () {
		base .Update ();

	}

    public void BeforeReturnPool()
    {


    }
    #region StateMachinSet
    protected override void OnEntryDeath()
    {
        base.OnEntryDeath();
        animator.SetBool("IsDead", true);

    }
    protected override void OnEntryAttack()
    {
        base.OnEntryAttack();
        animator.SetBool("IsOnAttach", true);

    }
    protected override void OnExitAttack()
    {
        base.OnExitAttack();
        animator.SetBool("IsOnAttach", false);

    }
    protected override void Idle()
    {
        base.Idle();

    }
    protected override void OnEntryMoveing()
    {
        base.OnEntryMoveing();
        animator.SetBool("IsOnTrack", true);


    }
    protected override void OnExitMoveing()
    {
        base.OnExitMoveing();
        animator.SetBool("IsOnTrack", false);

    }

    #endregion

}
