using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject target = null;
    private float speed = 10f;
    public float bulletDamage;

    void Start()
    {
        /*this.bulletDamage = this.transform.parent.GetComponent<TowerController>().damage;*/
            SoundManager.PlaySound(SoundManager.Sound.towerShoot);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        translateToTarget();        
    }

    public void SetDamage(float tower_damage)
    {
        this.bulletDamage = tower_damage;
    }

    public void SetTarget(GameObject target){
        if(target == null){
            DestroyObject(this.gameObject);

        } else {
            this.target = target;
            this.gameObject.SetActive(true);
        }
    }

    void translateToTarget()
    {
        if(target!=null){
            Vector3 movement = 
                new Vector3(
                     target.transform.position.x - this.gameObject.transform.position.x ,
                     target.transform.position.y - this.gameObject.transform.position.y ,
                     target.transform.position.z - this.gameObject.transform.position.z  
                );
            this.gameObject.transform.Translate(Vector3.Normalize(movement) * Time.deltaTime * speed);
        } else {
            DestroyObject(this.gameObject);
        }
    }

    private IEnumerator DestroyObject(){
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }

}
