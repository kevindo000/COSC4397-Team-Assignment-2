using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalState : MonoBehaviour
{
    public Text infoBox;
    private long money1;
    private int health1;

    public void setText()
    {
        string temp = "Money: " + money.ToString() + '\n' + "Health: " + health.ToString();
        infoBox.text = temp;
    }

    public long money
    {
        get => money1;
        set
        {
            money1 = value;
            setText();
        }
    }
    public int health {
        get => health1;
        set {
            health1 = value;
            setText();
        }
    }
    public bool gameOver => health < 0;
    void Start()
    {
        money = 650;
        health = 100;
    }
}