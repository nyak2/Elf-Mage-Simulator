using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrierenWander : StateBase
{
    public override string Name => "Wander";

    private Frieren frieren;
    private Wander wander;
    private Animator anim;

    public FrierenWander(Frieren f)
    {
        frieren = f;
        anim = frieren.GetComponent<Animator>();
        wander = frieren.GetComponent<Wander>();
    }

    protected override void OnEnter()
    {
        StatesHandler.FrierenWander = true;
        anim.Play("Walk",0,0);
    }

    protected override void OnUpdate(float deltaTime)
    {
        if(!StatesHandler.MouseGrab)
        { 
            wander.StartWandering();
        }
    }

    protected override void OnExit()
    {
        Debug.Log("StopWandering");
        wander.StopWandering();
    }
}
