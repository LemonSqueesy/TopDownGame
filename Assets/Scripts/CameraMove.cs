using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public GameObject aim;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(aim.transform.position.x, aim.transform.position.y, transform.position.z);
    }
}
