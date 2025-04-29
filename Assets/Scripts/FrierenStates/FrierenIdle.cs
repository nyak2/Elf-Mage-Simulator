using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrierenIdle : StateBase
{
    public override string Name => "Idle";

    private Frieren frieren;
    private Animator anim;


    public FrierenIdle(Frieren f)
    {
        frieren = f;
        anim = frieren.GetComponent<Animator>();
    }

    protected override void OnEnter()
    {
        StatesHandler.FrierenWander = false;
        anim.Play("Idle",0,0);
        Debug.Log("idling");
    }

    protected override void OnUpdate(float deltaTime)
    {

    }
    protected override void OnExit()
    {
    }
}
