using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FernPursuit : StateBase
{
    public override string Name => "Pursuit";

    private Fern fern;
    private Pursuit pursuit;
    private Animator anim;

    public FernPursuit(Fern f)
    {
        fern = f;
        pursuit = fern.GetComponent<Pursuit>();
        anim = fern.GetComponent<Animator>();
    }

    protected override void OnEnter()
    {
        anim.Play("Walk", 0, 0);
        fern.IncreaseSpeed(6.5f);
    }

    protected override void OnUpdate(float deltaTime)
    {
        pursuit.Move(fern.frieren);
    }

    protected override void OnExit()
    {
        fern.RevertSpeed(6.5f);
        pursuit.StopPrusuit();
    }
}
