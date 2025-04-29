using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrierenCaught : StateBase
{
    public override string Name => "Caught";

    private Frieren frieren;
    private Animator anim;

    public FrierenCaught(Frieren f)
    {
        frieren = f;
        anim = frieren.GetComponent<Animator>();
    }

    protected override void OnEnter()
    {
        anim.Play("Cry", 0, 0);
    }

    protected override void OnUpdate(float deltaTime)
    {

    }
    protected override void OnExit()
    {
        frieren.fernChase = false;
        frieren.caught = false;
    }
}


