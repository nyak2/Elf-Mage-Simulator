using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonWander : StateBase
{
    public override string Name => "Wander";

    private Demon demon;
    private Wander wander;
    private Animator anim;

    public DemonWander(Demon d)
    {
        demon = d;
        anim = demon.GetComponent<Animator>();
        wander = demon.GetComponent<Wander>();
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

