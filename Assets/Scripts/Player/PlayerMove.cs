using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;
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
        Vector2? mouseVector = new Vector2();
        Quaternion? rot;
        //man.GetAction(GameActionManager.GameAction.MoveUp);
        man.GetPlayerRotation(transform.position, out mouseVector, out rot);
        if (rot.HasValue)
            transform.rotation = rot.Value;

        var transformVector = new Vector2(transform.position.x, transform.position.y);
        if ((transformVector - mouseVector.Value).magnitude > 0.2)
        {
            var mov = man.GetMove();
          

            if (mov != Vector2.zero)
            {
                //mov.x += transform.right.x;
                //mov.y += transform.right.y;
                mov *= speed * Time.deltaTime;
                transform.Translate(mov.y,-mov.x,0,Space.Self);
                //transform.position += new Vector3(mov.x, mov.y);
            }
               

            //if (man.GetAction(GameActionManager.GameAction.MoveUp))
            //{
            //    transform.position += transform.right * speed * Time.deltaTime;
            //}
            //if (man.GetAction(GameActionManager.GameAction.MoveDown))
            //{
            //    transform.position -= transform.right * speed * Time.deltaTime;
            //}
            //if (man.GetAction(GameActionManager.GameAction.MoveRight))
            //{
            //    transform.position -= transform.up * speed * Time.deltaTime;
            //}
            //if (man.GetAction(GameActionManager.GameAction.MoveLeft))
            //{
            //    transform.position += transform.up * speed * Time.deltaTime;
            //}
            //if (man.GetAction(GameActionManager.GameAction.Run) && man.GetAction(GameActionManager.GameAction.MoveUp) && fatigue > 0)
            //{
            //    transform.position += transform.right * runSpeed * Time.deltaTime;
            //    fatigue--;
            //}
            //if (man.GetAction(GameActionManager.GameAction.Run) && man.GetAction(GameActionManager.GameAction.MoveDown) && fatigue > 0)
            //{
            //    transform.position -= transform.right * runSpeed * Time.deltaTime;
            //    fatigue--;
            //}
            //if (man.GetAction(GameActionManager.GameAction.Run) && man.GetAction(GameActionManager.GameAction.MoveRight) && fatigue > 0)
            //{
            //    transform.position -= transform.up * runSpeed * Time.deltaTime;
            //    fatigue--;
            //}
            //if (man.GetAction(GameActionManager.GameAction.Run) && man.GetAction(GameActionManager.GameAction.MoveLeft) && fatigue > 0)
            //{
            //    transform.position += transform.up * runSpeed * Time.deltaTime;
            //    fatigue--;
            //}
            //if (fatigue != saveFatigue && !(man.GetAction(GameActionManager.GameAction.Run)))
            //{
            //    fatigue++;
            //}
        }
    }
}
