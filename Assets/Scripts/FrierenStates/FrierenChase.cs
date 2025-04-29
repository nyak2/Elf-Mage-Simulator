using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrierenChase : StateBase
{
    public override string Name => "Chase";

    private Frieren frieren;
    private Pursuit pursuit;
    private Animator anim;

    public FrierenChase(Frieren f)
    {
        frieren = f;
        anim = frieren.GetComponent<Animator>();
        pursuit = frieren.GetComponent<Pursuit>();
    }

    protected override void OnEnter()
    {
        StatesHandler.FrierenChase = true;
        StatesHandler.doOnce = true;
        frieren.IncreaseSpeed(5);
        anim.Play("Walk", 0, 0);
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (!StatesHandler.MouseGrab)
        {
            pursuit.Move(frieren.ChaseTarget);
        }
    }

    protected override void OnExit()
    {
        StatesHandler.FrierenChase = false;
        StatesHandler.doOnce = false;
        frieren.fernChase = false;
        frieren.RevertSpeed(5);
        pursuit.StopPrusuit();
    }
}

