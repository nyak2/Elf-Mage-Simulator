using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FernWander : StateBase
{
    public override string Name => "Wander";

    private Fern fern;
    private Wander wander;
    private Animator anim;

    public FernWander(Fern f)
    {
        fern = f;
        wander = fern.GetComponent<Wander>();
        anim = fern.GetComponent<Animator>();
    }

    protected override void OnEnter()
    {
        anim.Play("Walk", 0, 0);
    }

    protected override void OnUpdate(float deltaTime)
    {
        wander.StartWandering();
    }

    protected override void OnExit()
    {
        wander.StopWandering();
    }
}
