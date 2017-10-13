using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMove : MonoBehaviour
{

    public float speed;
    public float runSpeed;
    public int fatigue;
    private int saveFatigue;

    GameActionManager man;


    void Start()
    {
        saveFatigue = fatigue;
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
                transform.position -= transform.right * speed * Time.deltaTime;
            }
            if (man.GetAction(GameActionManager.GameAction.MoveRight))
            {
                transform.position += transform.up * speed * Time.deltaTime;
            }
            if (man.GetAction(GameActionManager.GameAction.MoveLeft))
            {
                transform.position -= transform.up * speed * Time.deltaTime;
            }
            if (man.GetAction(GameActionManager.GameAction.Run) && man.GetAction(GameActionManager.GameAction.MoveUp) && fatigue > 0)
            {
                transform.position += transform.right * runSpeed * Time.deltaTime;
                fatigue--;
            }
            if (man.GetAction(GameActionManager.GameAction.Run) && man.GetAction(GameActionManager.GameAction.MoveDown) && fatigue > 0)
            {
                transform.position -= transform.right * runSpeed * Time.deltaTime;
                fatigue--;
            }
            if (man.GetAction(GameActionManager.GameAction.Run) && man.GetAction(GameActionManager.GameAction.MoveRight) && fatigue > 0)
            {
                transform.position += transform.up * runSpeed * Time.deltaTime;
                fatigue--;
            }
            if (man.GetAction(GameActionManager.GameAction.Run) && man.GetAction(GameActionManager.GameAction.MoveLeft) && fatigue > 0)
            {
                transform.position -= transform.up * runSpeed * Time.deltaTime;
                fatigue--;
            }
            if (fatigue != saveFatigue && !(man.GetAction(GameActionManager.GameAction.Run)))
            {
                fatigue++;
            }
        }
    }
}
