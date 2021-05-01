using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirctionalMatching : MonoBehaviour
{
    public GameObject x;
    public GameObject y;
    public GameObject z;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.right = x.transform.position;
        transform.up = y.transform.position;
        transform.forward = z.transform.position;
    }
}
