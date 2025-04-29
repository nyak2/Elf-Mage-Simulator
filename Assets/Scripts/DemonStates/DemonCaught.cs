using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonCaught : StateBase
{
    public override string Name => "Caught";

    private Demon demon;
    private Animator anim;

    public DemonCaught(Demon d)
    {
        demon = d;
        anim = demon.GetComponent<Animator>();
    }

    protected override void OnEnter()
    {
        demon.MoveDemon();
        demon.TurnOffText();
        anim.Play("Death", 0, 0);
    }

    protected override void OnUpdate(float deltaTime)
    {

    }
    protected override void OnExit()
    {

    }
}

