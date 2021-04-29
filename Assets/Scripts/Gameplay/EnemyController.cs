using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Vector3.MoveTowards example.

// A cube can be moved around the world. It is kept inside a 1 unit by 1 unit
// xz space. A small, long cylinder is created and positioned away from the center of
// the 1x1 unit. The cylinder is moved between two locations. Each time the cylinder is
// positioned the cube moves towards it. When the cube reaches the cylinder the cylinder
// is re-positioned to the other location. The cube then changes direction and moves
// towards the cylinder again.
//
// A floor object is created for you.
//
// To view this example, create a new 3d Project and create a Cube placed at
// the origin. Create Example.cs and change the script code to that shown below.
// Save the script and add to the Cube.
//
// Now run the example.

public class EnemyController : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed;
    private Transform target;
    public GameObject[] pathTokens;
    private int i = 0;
    private int playerHealth = 100;
    private float towerAttack;
    public int attackStrength;
    public Slider healthBar;
    public GameObject healthBarUI;
    public Image Fill;

    // The greater the number for attack speed the slower the attack
    public float attackSpeed;
    public float maxHealth = 100.0f;
    public float health;

    private void Start()
    {
        // Grab pathTokens values and place on the target.
        target = pathTokens[i].transform;
        
        health = maxHealth;        
        healthBar.value = CalculateHealth();
        // Movement();
    }

    /*void Awake()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        healthBar.value = CalculateHealth();
        if (playerHealth <= 0)
        {
            Debug.Log("GAME OVER");
        }
        if(health < maxHealth){
            healthBarUI.SetActive(true);
        }
        if(health > maxHealth){
            health = maxHealth;
        }
        if(health < maxHealth*.3f){
            if(health % 3 == 0){
                healthBarUI.GetComponentInChildren<Image>().color = Color.white;
            } else{
                healthBarUI.GetComponentInChildren<Image>().color = Color.red;
            }
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        if (Vector3.Distance(transform.position, target.position) < 0.001f)
        {
            i++;
            if (i < pathTokens.Length)
            {
                target = pathTokens[i].transform;
            }

        }

        if (i == pathTokens.Length)
        {
            Debug.Log("START");
            StartCoroutine(Waiter());
            i = pathTokens.Length + 1;
        }
        else if (i > pathTokens.Length)
        {
            /*health -= 1;
            Debug.Log("health = " + health);*/
            i = pathTokens.Length + 1;
        }
    }

    IEnumerator Waiter()
    {
        while (playerHealth > 0)
        {
            Debug.Log("Player's Health: " + playerHealth);
            yield return new WaitForSeconds(attackSpeed);
            playerHealth -= attackStrength;
        }

        Debug.Log("Player is dead");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit");
            towerAttack = other.GetComponent<BulletController>().bulletDamage;
            Debug.Log("Tower Attack: " + towerAttack);
            health -= towerAttack;
            SoundManager.PlaySound(SoundManager.Sound.playerHit);
            Destroy(other.gameObject);
        }


    }
    public float CalculateHealth(){
        return health/maxHealth;
    }
}