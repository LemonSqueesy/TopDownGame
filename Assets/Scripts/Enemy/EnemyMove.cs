using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    public GameObject Player;
    public float speed;
    public float MathResult;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var PlayerVector = new Vector2(Player.transform.position.x, Player.transform.position.y);
        var EnemyVector = new Vector2(transform.position.x, transform.position.y);

        if ((EnemyVector - PlayerVector).magnitude < 2)
        {
            MathResult = Mathf.Acos(Vector2.Dot((PlayerVector - EnemyVector).normalized, Player.transform.right.normalized)) * Mathf.Rad2Deg;
            EnemyLook(EnemyVector, PlayerVector);
            if (MathResult < 50)
            {

            }
        }
    }

    private void EnemyLook(Vector2 pos, Vector2 player)
    {
        Quaternion rot = Quaternion.LookRotation(player - pos);
        rot.eulerAngles = new Vector3(0, 0, -rot.eulerAngles.x);

        transform.rotation = rot;
    }
}
