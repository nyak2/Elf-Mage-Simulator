using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PursuitCheck : MonoBehaviour
{
    [SerializeField] Frieren frieren;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!StatesHandler.FrierenChase && !StatesHandler.doOnce)
        {
            if (collision.CompareTag("Demon"))
            {
                frieren.ChaseTarget = collision.gameObject;
                frieren.demonGrimoireLevel = collision.gameObject.GetComponent<Demon>().demonGrimoireLevel;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!StatesHandler.FrierenChase && !StatesHandler.doOnce)
        {
            if (collision.CompareTag("Demon"))
            {
                frieren.ChaseTarget = collision.gameObject;
                frieren.demonGrimoireLevel = collision.gameObject.GetComponent<Demon>().demonGrimoireLevel;
            }
        }
    }

}

