using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FernIdle : StateBase
{
    public override string Name => "Idle";

    private Fern fern;
    private Animator anim;

    public FernIdle(Fern f)
    {
        fern = f;
        anim = fern.GetComponent<Animator>();
    }

    protected override void OnEnter()
    {
        anim.Play("Idle", 0, 0);
    }

    protected override void OnUpdate(float deltaTime)
    {

    }

    protected override void OnExit()
    {

    }
}



