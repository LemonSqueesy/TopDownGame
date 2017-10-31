using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerMove : MonoBehaviour
{
    public float RunSpeed;
    public float Speed;
    private float speedSave;
    public int Fatigue;
    private int fatigueSave;

    GameActionManager man;


    void Start()
    {
        fatigueSave = Fatigue;
        speedSave = Speed;
        man = GameActionManager.Instance;
    }


    void Update()
    {
        Vector2? mouseVector = new Vector2();
        Quaternion? rot;
        man.GetPlayerRotation(transform.position, out mouseVector, out rot);
        if (rot.HasValue)
            transform.rotation = rot.Value;

        var transformVector = new Vector2(transform.position.x, transform.position.y);
            
        if ((transformVector - mouseVector.Value).magnitude > 0.2)
        {
            var mov = man.GetMove();

            if (mov != Vector2.zero)
            {
                if (man.GetAction(GameActionManager.GameAction.Run) && Fatigue != 0)
                {
                    print(1);                 
                    Speed = RunSpeed;
                    Fatigue--;
                }
                else
                    Speed = speedSave;
                

                mov *= Speed * Time.deltaTime;
                transform.Translate(mov.x, mov.y, 0, Space.World);
                //transform.Translate(mov.y, -mov.x, 0, Space.Self);
            }

            if (Fatigue < fatigueSave && !(man.GetAction(GameActionManager.GameAction.Run)))
                Fatigue++;
        }
    }
}
