using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FleeCheck : MonoBehaviour
{
    [SerializeField] Frieren frieren;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(!StatesHandler.FrierenFlee)
        //{
            if (collision.CompareTag("Demon") || collision.CompareTag("Fern"))
            {
                if(collision.CompareTag("Demon") && !frieren.fernChase)
                {
                frieren.fleeTarget = collision.gameObject;
                frieren.demonGrimoireLevel = collision.gameObject.GetComponent<Demon>().demonGrimoireLevel;
                }
                else if(collision.CompareTag("Fern"))
                {
                    frieren.fleeTarget = collision.gameObject;
                    frieren.fernChase = true;
                }
            }
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (!StatesHandler.FrierenFlee)
        //{
            if (collision.CompareTag("Demon") || collision.CompareTag("Fern"))
            {
                if (collision.CompareTag("Demon") && !frieren.fernChase)
                {
                    frieren.fleeTarget = collision.gameObject;
                    frieren.demonGrimoireLevel = collision.gameObject.GetComponent<Demon>().demonGrimoireLevel;
                }
                else if (collision.CompareTag("Fern"))
                {
                    frieren.fleeTarget = collision.gameObject;
                    frieren.fernChase = true;
                }   
        }

        //}
    }

}
