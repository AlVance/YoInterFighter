using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMngrPj : MonoBehaviour
{
    Platformer platformer;
    public Animator myAnim;

    public string statePj;

    public bool onAttack;
    public int attackCount;

    // Start is called before the first frame update
    void Start()
    {
        platformer = GetComponent<Platformer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onAttack)
        {
            statePj = platformer.currentState;
        }
        switch (statePj)
        {
            case "Idle":
                myAnim.SetBool("walking", false);
                break;
            case "Walk":
                Debug.Log("Anda");
                    myAnim.SetBool("walking", true);
                break;
            case "Attack":
                myAnim.SetInteger("combo", attackCount);
                myAnim.SetTrigger("attack");
                break;
            case "Jump":
                break;
            case "Fall":
                break;
        }
    }
}
