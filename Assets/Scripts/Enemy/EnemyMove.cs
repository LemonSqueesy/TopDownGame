using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyExtensions
{
    public static Quaternion LookRotation2D(this Quaternion qua, Vector2 vec)
    {
        float angle = (Mathf.Atan2(-vec.x, vec.y) * Mathf.Rad2Deg) + 90;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
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
        var moveDirection = PlayerVector - EnemyVector;

        if ((EnemyVector - PlayerVector).magnitude < 2)
        {
            MathResult = Mathf.Acos(Vector2.Dot((PlayerVector - EnemyVector).normalized, transform.right.normalized)) * Mathf.Rad2Deg;
            print(MathResult);
            if (MathResult < 90)
            {
                EnemyLook(moveDirection);
            }
        }
    }

    private void EnemyLook(Vector2 moveDirection)
    {
        Quaternion quaternion = new Quaternion();
        quaternion = quaternion.LookRotation2D(moveDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 0.05f);
    }
}
