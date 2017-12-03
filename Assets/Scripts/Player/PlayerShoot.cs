using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform PointShoot;
    public GameObject MazleGo;
    public Bullet FlashGo;

    GameActionManager man;

    void Start()
    {
        
        if (!PointShoot || !FlashGo)
            Debug.LogError("Настрой префаб, маслёнок!!!");
        man = GameActionManager.Instance;
        if (MazleGo)
            MazleGo.SetActive(false);
    }

    bool flagShoot;
    void Update()
    {
        if (man.GetAction(GameActionManager.GameAction.Shoot))
        {
            if (!flagShoot)
            {
                StartCoroutine(Shoot(0.05f));
                flagShoot = true;
            }
        }
        else
        {
            MazleGo.SetActive(false);
            flagShoot = false;
        }
    }

    public IEnumerator Shoot(float t)
    {     
        MazleGo.SetActive(true);
        Instantiate(FlashGo,PointShoot.position,MazleGo.transform.rotation* Quaternion.Euler(-90,0,0));
        yield return new WaitForSeconds(t);
        MazleGo.SetActive(false);
        yield return new WaitForSeconds(t);
        flagShoot = false;
    }
}
