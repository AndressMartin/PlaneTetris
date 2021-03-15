using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Transform customPivot;
    private float speed = -5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.localRotation.y);
        if (transform.localRotation.y >= 0 )
        {
            //Debug.Log("0");
            speed = -5f;
            //transform.rotation = new Quaternion(transform.rotation.x, -1, transform.rotation.z);
        }
        if (transform.localRotation.y <= -0.6f)
        {
            //Debug.Log("100");
            speed = 5f;
        }
        transform.RotateAround(customPivot.position, Vector3.up, speed * Time.deltaTime);
        transform.RotateAround(customPivot.position, Vector3.forward, speed * Time.deltaTime);
    }
}
