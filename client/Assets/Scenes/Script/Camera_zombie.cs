using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_zombie : MonoBehaviour {

    // Use this for initialization
    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
        //Debug.Log(offset);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
        transform.LookAt(GetComponent<Rigidbody>().velocity);

    }
}
