using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CamerFollower : MonoBehaviour
{
    [SerializeField] private Transform Frieren;
    private bool freeCamera;
    private float horizontal;
    private float vertical;
    [SerializeField] private float speed;
    [SerializeField] private float edgeSize;
    [SerializeField] private float moveamt;
    public bool edgeMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            freeCamera = !freeCamera;
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)// scrolling down
        {
            Camera.main.orthographicSize++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)//scrolling up
        {
            Camera.main.orthographicSize--;
        }
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 6, 21);

    }

    private void LateUpdate()
    {
        if(!edgeMove)
        {
            if (freeCamera)
            {
                horizontal = Input.GetAxis("Horizontal") * speed;
                vertical = Input.GetAxis("Vertical") * speed;
                Camera.main.transform.position = new Vector3(transform.position.x + horizontal, transform.position.y + vertical, -5);
            }
            else
            {
                Vector3 targetpos = new Vector3(Frieren.position.x, Frieren.position.y, -10);
                Camera.main.transform.position = targetpos;
            }
        }
        else
        {
            float newposX = 0.0f;
            float newposY = 0.0f;
            if (Input.mousePosition.x > Screen.width - edgeSize)
            {
                newposX = Camera.main.transform.position.x + (moveamt * Time.deltaTime);
                Camera.main.transform.position = new Vector3(newposX, Camera.main.transform.position.y, -5);
            }
            else if (Input.mousePosition.x < edgeSize)
            {
                newposX = Camera.main.transform.position.x -  (moveamt * Time.deltaTime);
                Camera.main.transform.position = new Vector3(newposX, Camera.main.transform.position.y, -5) ;
            }

            if(Input.mousePosition.y > Screen.height - edgeSize)
            {
                newposY = Camera.main.transform.position.y + (moveamt * Time.deltaTime);
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, newposY, -5);

            }
            else if (Input.mousePosition.y < edgeSize)
            {
                newposY = Camera.main.transform.position.y - (moveamt * Time.deltaTime);
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, newposY, -5);
            }

        }
        Camera.main.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -27, 27), Mathf.Clamp(transform.position.y, -27, 27), -5);
    }
}
