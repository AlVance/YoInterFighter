using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public KeyCode attackKey;

    bool canAttack;

    public float timeAttack = 0.1f;
    float currentTime;

    public Transform rootGun;
    public GameObject bullet;

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            if (Input.GetKeyDown(attackKey))
            {
                GameObject bulletIns = Instantiate(bullet, rootGun);
                bulletIns.GetComponent<BulletMove>().x = transform.localScale.x;
                bulletIns.GetComponent<HitCtrl>().playerObj = this.gameObject;
                bulletIns.transform.parent = null;
                currentTime = 0;
                canAttack = false;
            }
        }
        else
        {
            if (currentTime < timeAttack)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                currentTime = 0;
                canAttack = true;
            }
        }
    }
}
