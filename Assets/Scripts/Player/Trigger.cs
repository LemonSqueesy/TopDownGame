using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    private bool flag;
    Coroutine cor;

    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D cd)
    {
        if (cd.gameObject.tag == "Player" && !flag)
        {
            flag = true;
            cor = StartCoroutine(Wait(cd.gameObject));

            //cd.gameObject.transform.position = new Vector3(2.016F, -0.433F, 0);
        }
    }

    void OnTriggerExit2D(Collider2D cd)
    {

        if (cd.gameObject.tag == "Player")
        {
            flag = false;
            StopCoroutine(cor);
        }
    }

    public IEnumerator Wait(GameObject go)
    {
        yield return new WaitForSeconds(3F);
        if (flag)
        {
            go.transform.position = new Vector3(2.016F, -0.433F, 0);
        }

    }
}
