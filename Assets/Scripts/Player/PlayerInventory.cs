using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerInventory : MonoBehaviour
{
    #region Values
    [SerializeField] private Animator _animator;
    private UI UI;


    [SerializeField] private GameObject collectableObject;
    [Header("Player")]
    [SerializeField] private GameObject Use;
    [SerializeField] private GameObject hand;
    


    [Header("Inventory")]
    
    public List<GameObject> inventory = new List<GameObject>(); // l'inventaire du joueur

    [SerializeField] public GameObject[] equipment; // La list des équipement dans la barre d'outils
    //[SerializeField] private string _actionType = null;
    #endregion

    void Start()
    {
        _animator = GetComponent<Animator>();
        UI = GameObject.Find("Canvas").GetComponent<UI>();
        hand = GameObject.Find("hand").gameObject;
        Use = transform.GetChild(0).gameObject;
        equipment = new GameObject[4];

    }


    void Update()
    {
        
    }
    #region OnPillage
    public void Collect(GameObject collectableObject)
    {
        print("Collected");

        collectableObject.transform.position = new Vector2(99, 99);
        inventory.Add(collectableObject);
        /*if (collectableObject.transform.tag == "Weapon")
            equipment[0] = collectableObject;*/
        //UI.UpdateUI(collectableObject.GetComponent<SpriteRenderer>().sprite, collectableObject.transform.tag/*, inventory*/);
        //Equip();
    }


    /*    public void Equip()
        {
            if (equipment[0] != null)
            {
                equipment[0].transform.parent = hand.transform;
                equipment[0].transform.position = Vector2.zero;
            }
        }*/
    #endregion

    #region Collider

    //this part detect if player touch a collectable object or if stop touch

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.GetComponent<Collectible>())
        {
            Collect(collision.gameObject);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {


/*        if (collision.gameObject == collectableObject)
        {
            collectableObject = null;
        }*/


    }
    #endregion
}
