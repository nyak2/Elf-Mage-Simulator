using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FernAttack : StateBase
{
    public override string Name => "Wander";

    private Fern fern;
    private Flee flee;
    private Animator anim;

    public FernAttack(Fern f)
    {
        fern = f;
        anim = fern.GetComponent<Animator>();
        flee = fern.GetComponent<Flee>();
    }

    protected override void OnEnter()
    {
        fern.ReduceGrimoireLevel();
        fern.IncreaseSpeed(10.0f);
        fern.frieren.GetComponent<Frieren>().caught = true;
        fern.frieren.GetComponent<Frieren>().fleeTarget = null;
        fern.stolen = true;
        fern.DisableCollider();
        //anim.Play("Walk", 0, 0);
    }

    protected override void OnUpdate(float deltaTime)
    {
        flee.Move(fern.frieren.transform.position);
        fern.KillSelf();
    }

    protected override void OnExit()
    {
    }
}
