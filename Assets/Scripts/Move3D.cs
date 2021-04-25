using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Move3D : MonoBehaviour
{
    private Camera mainCamera;
    private float CameraZDistance;
    private bool canMove = true;
    private bool shoot = false;

    public Attributes a;
    public GameObject projectile;

    void Start()
    {
        mainCamera = Camera.main;
        CameraZDistance =
            mainCamera.WorldToScreenPoint(transform.position).z; //z axis of the game object for screen view
        canMove = true;
        //Debug.LogError(gameObject.name);
    }

    private float counter = 0;

    void OnMouseDrag()
    {
        if (canMove)
        {
            Vector3 ScreenPosition =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance); //z axis added to screen point 
            Vector3 NewWorldPosition =
                mainCamera.ScreenToWorldPoint(ScreenPosition); //Screen point converted to world point

            transform.position = NewWorldPosition;
        }
        else
        {
            Debug.LogError("Can't Move!");
        }
    }

    private void OnMouseUp()
    {
        canMove = false;
        if (!shoot)
        {
            shoot = true;
            gameObject.AddComponent(typeof(TowerController));
            gameObject.GetComponent<TowerController>().projectile = projectile;
            gameObject.GetComponent<TowerController>().damage = Convert.ToSingle(a.Damage);
            gameObject.GetComponent<TowerController>().fireRate = 0.5f;
            gameObject.GetComponent<SphereCollider>().radius = 15;
            gameObject.GetComponent<SphereCollider>().isTrigger = true;
        }
    }
}