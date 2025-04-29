using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    private enum Scenes
    {
        FrierenSimulator,
    }
    [SerializeField] private GameObject Ui;
    private bool close = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            close = !close;
            Ui.SetActive(!close);
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(Scenes.FrierenSimulator.ToString());
    }
}
