using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlShip : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int totalValue, totalWeight, maxWeight;
    [SerializeField] public List<GameObject> shipInventory;

    void Start()
    {
        totalValue = 0;
        totalWeight = 0; 
        maxWeight = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

/*

C:\Users\mehdi\Documents\UnityProject\UnityGit\NexusRainLight\Assets\00_Script\Interact\LvlShip.cs*/