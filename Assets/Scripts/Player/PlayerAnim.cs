using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator Move, 
        Inventory, Attack, collect;
    // Start is called before the first frame update
    void Start()
    {
        Move = gameObject.AddComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
