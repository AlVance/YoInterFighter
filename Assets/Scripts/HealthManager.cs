using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int health;
    public void SetHealth(int damageAtFall)
    {
        health -= damageAtFall;
    }
}
