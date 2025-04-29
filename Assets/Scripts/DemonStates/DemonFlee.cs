using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonFlee : StateBase
{
    public override string Name => "Flee";

    private Demon demon;
    private Evasion evasion;
    private Animator anim;

    public DemonFlee(Demon d)
    {
        demon = d;
        anim = demon.GetComponent<Animator>();
        evasion = demon.GetComponent<Evasion>();
    }

    protected override void OnEnter()
    {
        anim.Play("Walk", 0, 0);
        demon.IncreaseSpeed(3);
        Debug.Log("demon run");
    }

    protected override void OnUpdate(float deltaTime)
    {
        evasion.Move(demon.frieren);
    }
    protected override void OnExit()
    {
        demon.RevertSpeed(3);
        evasion.StopEvasion();
        //flee.StopFlee();
    }
}


