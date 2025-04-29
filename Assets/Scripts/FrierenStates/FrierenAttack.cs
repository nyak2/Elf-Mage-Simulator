using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrierenAttack : StateBase
{
    public override string Name => "Attack";

    private Frieren frieren;
    private Animator anim;

    public FrierenAttack(Frieren f)
    {
        frieren = f;
        anim = frieren.GetComponent<Animator>();
    }

    protected override void OnEnter()
    {
        StatesHandler.FrierenAttack = true;
        anim.Play("Attack", 0, 0);
        frieren.ChaseTarget.GetComponent<Demon>().caught = true;
        frieren.FlipSprite();
    }

    protected override void OnUpdate(float deltaTime)
    {

    }

    protected override void OnExit()
    {
        StatesHandler.FrierenAttack = false;
        StatesHandler.doOnce = false;
        StatesHandler.FrierenChase = false;
        frieren.fernChase = false;
        frieren.ChaseTarget = null;
        frieren.fleeTarget = null;
    }
}


