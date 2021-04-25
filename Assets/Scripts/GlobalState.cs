using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour
{
    public long money { get; set; }
    public int health { get; set; }
    public bool gameOver => health < 0;
    void Start()
    {
        money = 650;
        health = 100;
    }
}