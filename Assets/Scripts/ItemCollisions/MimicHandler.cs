using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MimicHandler : MonoBehaviour
{
    public GameObject FrierenObject;
    public ItemPlacer placer;

    private Animator anim;
    private SpriteRenderer sprite;
    [SerializeField] private CharacterValueScriptableObject characterValue;
    [SerializeField] private GameObject numtextObject;

    private float timer;
    private void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Stuck"))
        {
            timer += Time.deltaTime;
            if(timer >= characterValue.stucktime)
            {
                anim.SetTrigger("UnStuck");
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!StatesHandler.FrierenFlee && !StatesHandler.FrierenChase && !StatesHandler.FrierenAttack)
        {
            if (collision.CompareTag("Frieren") && !StatesHandler.FrierenStuck)
            {
                if (gameObject.transform.position.x >= FrierenObject.transform.position.x)
                 {
                     sprite.flipX = false;
                 }
                 else
                 {
                     sprite.flipX = true;
                 }
                 sprite.sortingOrder = 0;
                 FrierenObject.SetActive(false);
                 StatesHandler.FrierenStuck = true;
                 anim.enabled = true;
            }

        }
    }

    public void Release()
    {
        FrierenObject.SetActive(true);
        FrierenObject.GetComponent<Animator>().Play("Walk",0,0);
        FrierenObject.GetComponent<Frieren>().frierenGrimoireLevel += characterValue.MimicGrimoireLevelAmount;

        float randnum = Random.Range(-0.6f, 0.6f);
        Vector3 pos = new Vector3(gameObject.transform.position.x + randnum, gameObject.transform.position.y + 1.0f, gameObject.transform.position.z);
        numtextObject.GetComponent<TextMeshPro>().text = "+ " + characterValue.MimicGrimoireLevelAmount;
        Instantiate(numtextObject, pos, Quaternion.identity);

        StatesHandler.FrierenStuck = false;
        placer.itemList.Remove(gameObject);
        Destroy(gameObject);
    }


}
