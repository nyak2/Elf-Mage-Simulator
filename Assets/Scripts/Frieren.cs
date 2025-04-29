using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Frieren : MonoBehaviour
{
    private void Start()
    {
        stateMachine = new();

        FrierenIdle idle = new(this);
        FrierenRun run = new(this);
        FrierenWander wander = new(this);
        FrierenFindGrimoire find = new(this);
        FrierenChase chase = new(this);
        FrierenAttack attack = new(this);
        FrierenCaught caught = new(this);


        idle.AddTransition(caught, (timeInState) => GetCaught());
        idle.AddTransition(run, (timeInState) => InsideOfFleeRange() && (CheckGrimoireLevelBelow() || isFernChasing()));
        idle.AddTransition(chase, (timeInState) => InsideOfChaseRange() && CheckGrimoireLevelAbove());
        idle.AddTransition(find, (timeInState) => CheckItems() && StartFind(timeInState));
        idle.AddTransition(wander, (timeInState) => StartWander(timeInState));

        wander.AddTransition(run, (timeInState) => InsideOfFleeRange() && (CheckGrimoireLevelBelow() || isFernChasing()));
        wander.AddTransition(chase, (timeInState) => InsideOfChaseRange() && CheckGrimoireLevelAbove());
        wander.AddTransition(find, (timeInState) => CheckItems());
        wander.AddTransition(idle, (timeInState) => StartIdle(timeInState));

        run.AddTransition(caught, (timeInState) => GetCaught());
        run.AddTransition(idle, (timeInState) => OutsideOfFleeRange());

        find.AddTransition(run, (timeInState) => InsideOfFleeRange() && (CheckGrimoireLevelBelow() || isFernChasing()));
        find.AddTransition(idle, (timeInState) => CheckItemsEmpty());


        chase.AddTransition(run, (timeInState) => InsideOfFleeRange() && (CheckGrimoireLevelBelow() || isFernChasing()));
        chase.AddTransition(attack, (timeInState) => InsideOfAttackRange());
        chase.AddTransition(idle, (timeInState) => OutsideOfChaseRange());

        attack.AddTransition(idle, (timeInState) => AttackDuration(timeInState));

        caught.AddTransition(idle, (timeInState) => CryingDuration(timeInState));
        
        stateMachine.SetInitialState(idle);
    }

    ///**********************************************************
    /// DO NOT EDIT ANYTHING BELOW THIS LINE
    ///**********************************************************
    [SerializeField] private CharacterValueScriptableObject characterValue;

    public ItemPlacer placer;
    private StateMachine stateMachine;

    private int itemListCount;
    private PriorityQueue<GameObject> itemPriorityQueue = new();

    [HideInInspector]
    public float frierenGrimoireLevel;
    public float demonGrimoireLevel;

    [SerializeField] private TextMeshPro grimoreLevelText;

    [SerializeField] private Transform FleeRange;
    [SerializeField] private Transform ChaseRange;
    [SerializeField] private Transform AttackRange;

    [SerializeField] private float minDistanceFlee;
    [SerializeField] private float minDistanceChase;
    [SerializeField] private float minDistanceAttack;

/*    [HideInInspector] */public GameObject fleeTarget;

    /*[HideInInspector]*/ public GameObject ChaseTarget;
    public bool fernChase;
    public bool caught = false;
    private void Update()
    {
        stateMachine.Tick(Time.deltaTime);
        if(frierenGrimoireLevel <= 0)
        {
            frierenGrimoireLevel = 0;
        }
        grimoreLevelText.text = frierenGrimoireLevel.ToString("F0");

        FleeRange.localScale = new Vector3(minDistanceFlee, minDistanceFlee, minDistanceFlee) * 2;
        ChaseRange.localScale = new Vector3(minDistanceChase, minDistanceChase, minDistanceChase) * 2;
        AttackRange.localScale = new Vector3(minDistanceAttack, minDistanceAttack, minDistanceAttack) * 2;
    }


    #region GrimoireState
    public Vector3 LocateGrimoire(bool searchAgain)
    {
        if (searchAgain)
        {
            Queue<GameObject> queueChecker = new();
            PriorityQueue<GameObject> itemQueue = new();
            for (int i = 0; i < placer.itemList.Count; i++)
            {
                if (!queueChecker.Contains(placer.itemList[i]))
                {
                    float distance = Vector2.Distance(placer.itemList[i].gameObject.transform.position, transform.position);
                    itemQueue.Enqueue(placer.itemList[i], distance);
                    queueChecker.Enqueue(placer.itemList[i]);
                }

            }

            itemPriorityQueue = itemQueue;
            if (itemPriorityQueue.Count > 0)
            {
                return itemPriorityQueue.Dequeue().transform.position;
            }
        }
        else
        {
            if (itemPriorityQueue.Count > 0)
            {
                return itemPriorityQueue.Dequeue().transform.position;
            }

        }
        return Vector3.zero;
    }
    public void UpdateObjectListCount()
    {
        itemListCount = placer.itemList.Count;
    }

    public bool GrimoiresAdded()
    {
        if (itemListCount != placer.itemList.Count)
        {
            return true;
        }
        return false;
    }


    #endregion
    public void IncreaseSpeed(float speed)
    {
        GetComponent<SimpleVehicle>().maxSpeed = GetComponent<SimpleVehicle>().MaxSpeed + speed;
    }

    public void RevertSpeed(float speed)
    {
        GetComponent<SimpleVehicle>().maxSpeed = GetComponent<SimpleVehicle>().MaxSpeed - speed;
    }

    public void FlipSprite()
    {
        if(ChaseTarget.transform.position.x >= transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;

        }
    }

    public void ReestOrginal()
    {
        ChaseTarget = null;
        fleeTarget = null;
    }

    #region transition Checks
    private bool StartWander(float statetime)
    {
        float num = Random.Range(characterValue.minidleTime, characterValue.maxidleTime);
        return statetime > num;
    }

    private bool StartIdle(float stateTime)
    {
        float num = Random.Range(characterValue.minwanderTime, characterValue.minwanderTime);
        return stateTime > num;
    }

    private bool StartFind(float stateTime)
    {
        return stateTime > characterValue.findtime;
    }

    private bool CheckItems()
    {
        if (placer.itemList.Count > 0)
        {
            return true;
        }
        return false;
    }

    private bool CheckItemsEmpty()
    {
        if (placer.itemList.Count <= 0)
        {
            return true;
        }
        return false;
    }

    private bool InsideOfFleeRange()
    {
        if(fleeTarget != null)
        {
            return (fleeTarget.transform.position - transform.position).magnitude <= minDistanceFlee;
        }
        return false;
    }

    private bool OutsideOfFleeRange()
    {
        if (fleeTarget == null)
        {
            return true;
        }
        return (fleeTarget.transform.position - transform.position).magnitude > minDistanceFlee * 2;
    }

    private bool InsideOfChaseRange()
    {
        if (ChaseTarget != null)
        {
            return (ChaseTarget.transform.position - transform.position).magnitude <= minDistanceChase ;
        }
        return false;
    }

    private bool OutsideOfChaseRange()
    {
        if (ChaseTarget == null)
        {
            return true;
        }
        return (ChaseTarget.transform.position - transform.position).magnitude > minDistanceChase * 2;
    }


    private bool InsideOfAttackRange()
    {
        return (ChaseTarget.transform.position - transform.position).magnitude <= minDistanceAttack ;
    }


    private bool CheckGrimoireLevelBelow()
    {
        return frierenGrimoireLevel < demonGrimoireLevel;
    }

    private bool CheckGrimoireLevelAbove()
    {
        return frierenGrimoireLevel >= demonGrimoireLevel;
    }

    private bool AttackDuration(float timestate)
    {
        return timestate > characterValue.attacktime;
    }

    private bool isFernChasing()
    {
        return fernChase;
    }

    private bool GetCaught()
    {
        return caught;
    }

    private bool CryingDuration(float timestate)
    {
        return timestate > characterValue.cryingtime;
    }
    #endregion
}
