using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonIdle : StateBase
{
    public override string Name => "Idle";

    private Demon demon;
    private Animator anim;


    public DemonIdle(Demon d)
    {
        demon = d;
        anim = demon.GetComponent<Animator>();
    }

    protected override void OnEnter()
    {
        anim.Play("Idle", 0, 0);
        //Debug.Log("idling");
    }

    protected override void OnUpdate(float deltaTime)
    {

    }
    protected override void OnExit()
    {

    }
}
