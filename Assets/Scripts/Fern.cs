using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;

public class Fern : MonoBehaviour
{
    private void Start()
    {
        stateMachine = new();

        FernIdle idle = new(this);
        FernWander wander = new(this);
        FernPursuit pursuit = new(this);
        FernAttack attack = new(this);

        idle.AddTransition(pursuit, (timeInState) => InsideOfRange() && !StatesHandler.FrierenAttack && HasAnyGrimoires());
        idle.AddTransition(wander, (timeInState) => StartWander(timeInState));

        wander.AddTransition(pursuit, (timeInState) => InsideOfRange() && !StatesHandler.FrierenAttack && HasAnyGrimoires());
        wander.AddTransition(idle, (timeInState) => StartIdle(timeInState));

        pursuit.AddTransition(wander, (timeInState) => AlrCaught());
        pursuit.AddTransition(attack, (timeInState) => InsideOfAttackRange() && !StatesHandler.FrierenAttack && HasAnyGrimoires());
        pursuit.AddTransition(idle, (timeInState) => OutsideOfRange());

        stateMachine.SetInitialState(wander);
    }

    ///**********************************************************
    /// DO NOT EDIT ANYTHING BELOW THIS LINE
    ///**********************************************************
    [SerializeField] private CharacterValueScriptableObject characterValue;

    private StateMachine stateMachine;

    [HideInInspector]
    public float demonGrimoireLevel;


    [SerializeField] private float minChaseDistance;
    [SerializeField] private float minAttackDistance;
    [SerializeField] private Transform ChaseRange;
    [SerializeField] private Transform AttackRange;
    [SerializeField] private GameObject numtextObject;

    [HideInInspector]
    public GameObject frieren;

    public bool stolen;

    private void Update()
    {
        stateMachine.Tick(Time.deltaTime);

        ChaseRange.localScale = new Vector3(minChaseDistance, minChaseDistance, minChaseDistance) * 2;
        AttackRange.localScale = new Vector3(minAttackDistance, minAttackDistance, minAttackDistance) * 2;

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
        if(OutsideOfRange())
        {
            Destroy(gameObject);
        }
        //gameObject.SetActive(false);
    }



    public void DisableCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void ReduceGrimoireLevel()
    {
        float numtext;
        float num = Random.Range(1, 10) ;
        if(num >= frieren.GetComponent<Frieren>().frierenGrimoireLevel)
        {
            numtext = frieren.GetComponent<Frieren>().frierenGrimoireLevel;
        }
        else
        {
            numtext = num;
        }
        frieren.GetComponent<Frieren>().frierenGrimoireLevel -= num;
        SpawnNumText(numtext);
    }
    private void SpawnNumText(float num)
    {
        float randnum = Random.Range(-0.6f, 0.6f);
        Vector3 pos = new Vector3(gameObject.transform.position.x + randnum, gameObject.transform.position.y + 1.0f, gameObject.transform.position.z);
        numtextObject.GetComponent<TextMeshPro>().text = "- " + num.ToString();
        Instantiate(numtextObject, pos, Quaternion.identity); ;
    }


    #region transition Checks

    private bool StartWander(float statetime)
    {
        float num = Random.Range(characterValue.minidleTimeFern, characterValue.minidleTimeFern);
        return statetime > num;
    }

    private bool StartIdle(float stateTime)
    {
        float num = Random.Range(characterValue.minwanderTimeFern, characterValue.minwanderTimeFern);
        return stateTime > num;
    }

    private bool InsideOfRange()
    {
        if (frieren.activeSelf && !frieren.GetComponent<Frieren>().caught)
        {
            return ((frieren.transform.position - transform.position).magnitude <= minChaseDistance);
        }
        return false;
    }

    private bool OutsideOfRange()
    {
        if (frieren.activeSelf || stolen)
        {
            return ((frieren.transform.position - transform.position).magnitude > minChaseDistance * 2);
        }
        return false;
    }

    private bool InsideOfAttackRange()
    {
        if (frieren.activeSelf && !frieren.GetComponent<Frieren>().caught)
        {
            return ((frieren.transform.position - transform.position).magnitude <= minAttackDistance);
        }
        return false;
    }

    private bool HasAnyGrimoires()
    {
        return frieren.GetComponent<Frieren>().frierenGrimoireLevel > 0;
    }

    private bool AlrCaught()
    {
        return frieren.GetComponent<Frieren>().caught;
    }

    #endregion
}


