using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMove : MonoBehaviour
{

    public float speed;
    public float runSpeed;

    GameActionManager man;


    void Start()
    {
        man = GameActionManager.Instance;
    }

    void Update()
    {
        Quaternion rot;
        Vector2 mouseVector;
        man.GetMouseLook2D(transform.position, out mouseVector, out rot);
        transform.rotation = rot * Quaternion.Euler(0, 0, 90);

        var transformVector = new Vector2(transform.position.x, transform.position.y);
        if ((transformVector - mouseVector).magnitude > 0.2)
        {
            if (man.GetAction(GameActionManager.GameAction.MoveUp))
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
            if (man.GetAction(GameActionManager.GameAction.MoveDown))
            {

            }
            if (man.GetAction(GameActionManager.GameAction.MoveRight))
            {

            }
            if (man.GetAction(GameActionManager.GameAction.MoveLeft))
            {

            }
            if (man.GetAction(GameActionManager.GameAction.Run))
            {
                transform.position += transform.right * runSpeed * Time.deltaTime;
            }
        }
    }
}
