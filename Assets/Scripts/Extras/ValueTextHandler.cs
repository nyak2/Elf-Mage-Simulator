using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValueTextHandler : MonoBehaviour
{
    [SerializeField] private float time;
    // Start is called before the first frame update
    void Start()
    { 
        float i = Random.Range(2.0f, 2.5f);
        LeanTween.moveLocalY(gameObject, gameObject.transform.position.y + i, time).setOnComplete(Destroy);
        //LeanTween.alpha(gameObject, 0, time).setOnComplete(Destroy);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

}
