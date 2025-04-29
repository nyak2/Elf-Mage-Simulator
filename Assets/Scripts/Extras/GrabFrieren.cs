using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GrabFrieren : MonoBehaviour
{
    [SerializeField] private GameObject frieren;
    [SerializeField] private CamerFollower camfollow;
    [SerializeField] private Toggle toggle;
    private bool candrag;
    private Vector3 mousepos;
    private void OnEnable()
    {
        StatesHandler.MouseGrab = false;
        camfollow.edgeMove = false;
        GetComponent<WrapAround>().enabled = true;
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Mathf.Abs(frieren.transform.position.x - mousepos.x)  <= 0.2f)
            {
                StatesHandler.MouseGrab = true;
            }

            if(candrag && StatesHandler.MouseGrab)
            {
                mousepos = new Vector3(Mathf.Clamp(mousepos.x, -31, 31), Mathf.Clamp(mousepos.y, -31, 31), 0);
                GetComponent<WrapAround>().enabled = false;
                frieren.transform.position = mousepos;
                camfollow.edgeMove = true;
            }
        }

        if(Input.GetMouseButtonUp(0)) 
        {
            if(StatesHandler.MouseGrab)
            {
                StatesHandler.MouseGrab = false;
                camfollow.edgeMove = false;
                GetComponent<WrapAround>().enabled = true;
            }
            
        }
    }
    //private void OnMouseDrag()
    //{
    //}

    //private void OnMouseUp()
    //{
    //}

    public void CanDrag()
    {
        if(toggle.isOn)
        {
            candrag = true;
        }
        else
        {
            candrag = false;
        }
    }
}
