using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrierenRun : StateBase
{
    public override string Name => "Run";

    private Frieren frieren;
    private Flee flee;
    private Animator anim;

    public FrierenRun(Frieren f)
    {
        frieren = f;
        anim = frieren.GetComponent<Animator>();
        flee = frieren.GetComponent<Flee>();
    }

    protected override void OnEnter()
    {
        anim.Play("Run", 0, 0);
        Debug.Log("run");
        StatesHandler.FrierenFlee = true;
        frieren.IncreaseSpeed(4);
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (!StatesHandler.MouseGrab)
        {
            if (frieren.fleeTarget != null)
            {
                flee.Move(frieren.fleeTarget.transform.position);
            }
        }
    }
    protected override void OnExit()
    {
        flee.StopFlee();
        frieren.RevertSpeed(4);
        StatesHandler.FrierenFlee = false;
        StatesHandler.doOnce = false;
        StatesHandler.FrierenChase = false;
        frieren.fernChase = false;
        frieren.ReestOrginal();
    }
}
