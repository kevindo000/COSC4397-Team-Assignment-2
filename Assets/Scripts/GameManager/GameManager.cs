using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    //private float playerHealth = 100f;
    GlobalState g;
    public GameObject castleObject;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount){
        //playerHealth -= amount;
        g.health -= amount;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy"){
            EnemyController enemyController = other.GetComponent<EnemyController>();
            //playerHealth -= (float) enemyController.attackStrength;
            g.health -= enemyController.attackStrength;
            Destroy(other.gameObject);
            //Debug.Log(playerHealth);
        }
    }
}
