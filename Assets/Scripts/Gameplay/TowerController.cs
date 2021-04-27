using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    public GameObject projectile;
    private GameObject detectedEnemyObject;
    public float damage;
    public float fireRate = 2f; 

    // Start is called before the first frame update
    void Awake(){
        damage = 5.0f;
    }
    void LaunchProjectile(){
        if(detectedEnemyObject!=null){
            GameObject newBullet = GameObject.Instantiate(projectile, this.transform.position, Quaternion.identity); 
            newBullet.SetActive(true);
            BulletController controller = newBullet.GetComponent<BulletController>();
            controller.SetTarget(detectedEnemyObject);
            controller.SetDamage(damage);
            Debug.Log(damage);
        } else {
            Debug.Log("No enemy detected, cancel firing");
            CancelInvoke("LaunchProjectile");
        }
    }


    private void OnTriggerStay(Collider other)
    {
        // InvokeRepeating("LaunchProjectile", 0f, 1f);
        if(other.gameObject.tag == "Enemy" && detectedEnemyObject == null && !IsInvoking("LaunchProjectile")){
            detectedEnemyObject = other.gameObject;
            InvokeRepeating("LaunchProjectile", 0f, fireRate);
        }
    }

    private void OnTriggerExit(Collider other) {
        // CancelInvoke("LaunchProjectile");
        if(other.gameObject == detectedEnemyObject){
            detectedEnemyObject = null;
            CancelInvoke("LaunchProjectile");
        }
    }
}
