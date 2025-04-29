using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrierenFindGrimoire : StateBase
{
    public override string Name => "FindGrimoire";

    private Frieren frieren;
    private Seeker seeker;
    private Animator anim;

    private Vector3 destination;
    public FrierenFindGrimoire(Frieren f)
    {
        StatesHandler.FrierenSeek = true;
        frieren = f;
        anim = frieren.GetComponent<Animator>();
        seeker = frieren.GetComponent<Seeker>();
    }

    protected override void OnEnter()
    {
        StatesHandler.FrierenWander = false;
        anim.Play("Walk", 0, 0);
        destination = frieren.LocateGrimoire(true);
        frieren.IncreaseSpeed(2);
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (!StatesHandler.MouseGrab)
        {
            if (frieren.GrimoiresAdded())
            {
                destination = frieren.LocateGrimoire(true);
            }
            else
            {
                frieren.UpdateObjectListCount();
            }
 
            if (seeker.HasReachedDestination)
            {
                destination = frieren.LocateGrimoire(false);
                seeker.hasReached = false;

            }
            seeker.Move(destination);

        }
    }

    protected override void OnExit()
    {
        Debug.Log("StopSeeking");
        seeker.StopSeek();
        frieren.RevertSpeed(2);
    }
}
