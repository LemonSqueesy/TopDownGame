using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public float LifeTime;
    public float Power;
    public LayerMask layerMask;

    private float time;
    void Start()
    {
        time = Time.time;
        dir = transform.forward;
    }

    Vector3 dir;
    void FixedUpdate()
    {
        var p = transform.position + dir * Time.deltaTime * Speed;
        Quaternion rot = Quaternion.LookRotation(dir);
        // rot.eulerAngles = new Vector3(0, 0, rot.eulerAngles.z);
        transform.rotation = rot;
        transform.position += dir * Time.deltaTime * Speed;

        var hitRay = Physics2D.Raycast(transform.position, transform.forward, 0.19f, layerMask);
        if (hitRay.transform)
        {
            var phy = hitRay.transform.gameObject.GetComponent<Rigidbody2D>();
            if (phy)
            {
                phy.AddForceAtPosition(transform.forward * Power, hitRay.point);
            }
            var col = hitRay.collider.sharedMaterial;
            if (col)
            {
                dir = Vector2.Reflect(dir, hitRay.normal);
            }
            else
            {
                print("Destroyed");
                //Destroy(gameObject);
            }
        }
        if (Time.time - time > LifeTime)
            Destroy(gameObject);
    }

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    var phy = col.gameObject.GetComponent<Rigidbody2D>();
    //    if(phy)
    //    {
    //        phy.AddForceAtPosition(transform.up * Power, col.contacts[0].point);
    //    }
    //    print("Destroyed");
    //    Destroy(gameObject) ;

    //}
}
