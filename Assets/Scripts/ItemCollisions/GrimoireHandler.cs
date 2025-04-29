using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrimoireHandler : MonoBehaviour
{
    public GameObject frieren;
    public ItemPlacer placer;

    [SerializeField] private GameObject numtextObject;
    [SerializeField] private CharacterValueScriptableObject characterValue;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!StatesHandler.FrierenFlee && !StatesHandler.FrierenChase && !StatesHandler.FrierenAttack)
        {
            if(collision.CompareTag("Frieren"))
            {
                float randnum = Random.Range(-0.6f, 0.6f);
                Vector3 pos = new Vector3(gameObject.transform.position.x + randnum, gameObject.transform.position.y + 1.0f, gameObject.transform.position.z);
                numtextObject.GetComponent<TextMeshPro>().text = "+ " + characterValue.NormalGrimoireLevelAmount;
                Instantiate(numtextObject, pos, Quaternion.identity);

                Destroy(gameObject);
            }

        }
    }

    private void OnDestroy()
    {
        frieren.GetComponent<Frieren>().frierenGrimoireLevel += characterValue.NormalGrimoireLevelAmount;
        placer.itemList.Remove(gameObject);
    }
}
