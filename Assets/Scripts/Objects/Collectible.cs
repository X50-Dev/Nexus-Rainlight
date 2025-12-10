using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value;

    public GameManager manager;
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    public void CollectMoney()
    {
        manager.ChangeMoney(value);
    }
}
