using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;

public class Demon : MonoBehaviour
{
    private void Start()
    {
        stateMachine = new();

        DemonIdle idle = new(this);
        DemonWander wander = new(this);
        DemonFlee flee = new(this);
        DemonCaught captured = new(this);

        idle.AddTransition(flee, (timeInState) => InsideOfRange() && CheckGrimoireLevelBelow());
        idle.AddTransition(wander, (timeInState) => StartWander(timeInState));

        wander.AddTransition(flee, (timeInState) => InsideOfRange() && CheckGrimoireLevelBelow());
        wander.AddTransition(idle, (timeInState) => StartIdle(timeInState));

        flee.AddTransition(captured, (timeInState) => GetCaught() );
        flee.AddTransition(idle, (timeInstate) => OutsideOfRange());
        stateMachine.SetInitialState(wander);
    }

    ///**********************************************************
    /// DO NOT EDIT ANYTHING BELOW THIS LINE
    ///**********************************************************
    [SerializeField] private CharacterValueScriptableObject characterValue;

    private StateMachine stateMachine;

    [HideInInspector]
    public float demonGrimoireLevel;

    [SerializeField] private TextMeshPro grimoreLevelText;
    [SerializeField] private GameObject leveltext;

    [SerializeField] private float minFleeDistance;

    [HideInInspector]
    public GameObject frieren;

    public bool caught = false;

    private void Update()
    {
        stateMachine.Tick(Time.deltaTime);
        grimoreLevelText.text = demonGrimoireLevel.ToString("F0");
        //FleeChaseRange.localScale = new Vector3(minDistanceFleeChase, minDistanceFleeChase, minDistanceFleeChase);
        //AttackRange.localScale = new Vector3(minDistanceAttack, minDistanceAttack, minDistanceAttack);
    }

    public void IncreaseSpeed(float speed)
    {
        GetComponent<SimpleVehicle>().maxSpeed = GetComponent<SimpleVehicle>().MaxSpeed + speed;
    }

    public void RevertSpeed(float speed)
    {
        GetComponent<SimpleVehicle>().maxSpeed = GetComponent<SimpleVehicle>().MaxSpeed - speed;
    }

    public void KillSelf()
    {
        Destroy(gameObject);
    }

    public void TurnOffText()
    {
        leveltext.SetActive(false);
    }

    public void MoveDemon()
    {
        if(frieren.transform.position.x < transform.position.x)
        {
            transform.position = new Vector3(frieren.transform.position.x + 1.3f, transform.position.y, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            transform.position = new Vector3(frieren.transform.position.x - 1.3f, transform.position.y, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }


    #region transition Checks

    private bool StartWander(float statetime)
    {
        float num = Random.Range(characterValue.minidleTimeDemon, characterValue.minidleTimeDemon);
        return statetime > num;
    }

    private bool StartIdle(float stateTime)
    {
        float num = Random.Range(characterValue.minwanderTimeDemon, characterValue.minwanderTimeDemon);
        return stateTime > num;
    }

    private bool InsideOfRange()
    {
        if(frieren.activeSelf)
        {
            return ((frieren.transform.position - transform.position).magnitude <= minFleeDistance);
        }
        return false;
    }

    private bool OutsideOfRange()
    {
        if (frieren.activeSelf)
        {
            return ((frieren.transform.position - transform.position).magnitude > minFleeDistance * 2);
        }
        return false;
    }

    private bool GetCaught()
    {
        return caught;
    }

    private bool CheckGrimoireLevelBelow()
    {
        return demonGrimoireLevel <= frieren.GetComponent<Frieren>().frierenGrimoireLevel;
    }

    #endregion
}

