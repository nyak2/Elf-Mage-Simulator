using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum Items
{
    grimoires,
    mimics,
    demons,
    fern
}
public class ItemPlacer : MonoBehaviour
{
    private Items items;
    [SerializeField] private GameObject frieren;
    [SerializeField] private GameObject grimoires;
    [SerializeField] private GameObject mimics;
    [SerializeField] private GameObject demons;
    [SerializeField] private GameObject fern;
    [SerializeField] private GameObject Ui;

    [SerializeField] private TMP_InputField levelnumtext;
    [SerializeField] private GameObject demonLevelUi;
    [SerializeField] private TMP_Dropdown dropdown;


    public List<GameObject> itemList = new();
    public Queue<GameObject> itemQueue = new();

    private int numlabelGrim;
    private int numlabelMimic;
    private int numlabelDemon;
    private int numlabelFern;
    private int numLayerMimic = 0;

    private int setlevel;
    // Start is called before the first frame update
    void Start()
    {
        items = Items.grimoires;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && !StatesHandler.MouseGrab )
        {  
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos = new Vector3(Mathf.Clamp(pos.x, -31,31), Mathf.Clamp(pos.y,-31,31), 0);
        switch(items)
        {
            case Items.grimoires:
            {
                        GameObject g = Instantiate(grimoires, pos, Quaternion.identity);
                        g.name = grimoires.name + numlabelGrim;
                        g.GetComponent<GrimoireHandler>().placer = this ;
                        g.GetComponent<GrimoireHandler>().frieren = frieren;

                        itemList.Add(g);
                        itemQueue.Enqueue(g);

                        numlabelGrim++;
                        break;
            }
            case Items.mimics:
            {
                        GameObject m = Instantiate(mimics, pos, Quaternion.identity);
                        m.GetComponent<SpriteRenderer>().sortingOrder = numLayerMimic;
                        numLayerMimic--;

                        m.name = mimics.name + numlabelMimic;
                        m.GetComponent<MimicHandler>().placer = this;
                        m.GetComponent<MimicHandler>().FrierenObject = frieren;

                        itemList.Add(m);
                        itemQueue.Enqueue(m);

                        numlabelMimic++;
                        break;
            }
            case Items.demons:
            {
                        GameObject d = Instantiate(demons, pos, Quaternion.identity);
                        d.GetComponent<Demon>().demonGrimoireLevel = setlevel;
                        d.GetComponent<Demon>().frieren = frieren;
                        d.name = demons.name + numlabelDemon;

                        numlabelDemon++;
                        break;
            }

            case Items.fern:
            {
                        GameObject f = Instantiate(fern, pos, Quaternion.identity);
                        f.GetComponent<Fern>().frieren = frieren;
                        f.name = fern.name + numlabelFern;

                        numlabelFern++;
                        break;
            }
            }
        }
    }

    public void setItem()
    {
        switch (dropdown.value)
        {
            case 0:
                {
                    items = Items.grimoires;
                    demonLevelUi.SetActive(false);
                    break;
                }
            case 1:
                {
                    items = Items.mimics;
                    demonLevelUi.SetActive(false);
                    break;
                }
            case 2:
                {
                    items = Items.demons;
                    demonLevelUi.SetActive(true);
                    break;
                }
            case 3:
                {
                    items = Items.fern;
                    demonLevelUi.SetActive(false);
                    break;
                }
        }
    }
    //public void SelectGrimoire()
    //{
    //    items = Items.grimoires;
    //}

    //public void SelectMimic()
    //{
    //    items = Items.mimics;
    //}

    //public void SelectDemons()
    //{
    //    items = Items.demons;
    //}

    //public void SelectFern()
    //{
    //    items = Items.fern;
    //}

    public void ChangeDemonLevel()
    {
        if(int.TryParse(levelnumtext.text, out int num))
        {
            setlevel = num;
        }
        else
        {
            setlevel = 0;
        }
    }
}
