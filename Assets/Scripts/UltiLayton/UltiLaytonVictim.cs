using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiLaytonVictim : MonoBehaviour
{
    Platformer platformer;
    MainManager mainMngr;

    public GameObject block_Pref;
    public GameObject alertDontAttack;

    AttackMelee attackCmpnt;
    int indexBlock = 0;

    public GameObject ultiPref;

    // Start is called before the first frame update
    void Start()
    {
        mainMngr = FindObjectOfType<MainManager>();
        platformer = GetComponent<Platformer>();
        ultiPref = GameObject.Find("UltiLaytonPos");
        Ultimate();
    }

    private void Update()
    {
        if (platformer.typeSelect == Platformer.TypeAttack.Melee)
        {
            if(attackCmpnt != null)
            {
                if (Input.GetKeyDown(attackCmpnt.attackKey))
                {
                    Vector3 posAlert = transform.position + (Vector3.up * 2);
                    Instantiate(alertDontAttack, posAlert, Quaternion.identity);
                }
            }
        }
    }

    void Ultimate()
    {
        if(platformer.typeSelect == Platformer.TypeAttack.Melee)
        {
            attackCmpnt = GetComponent<AttackMelee>();
            attackCmpnt.enabled = false;
            for (int i = 0; i < ultiPref.transform.childCount; i++)
            {
                GameObject block = Instantiate(block_Pref, ultiPref.transform.GetChild(i));
                block.transform.parent = null;
                block.GetComponent<UltiLaytonBlock>().idBlock = i;
            }
        }
    }

    void FinishUltimate()
    {
        if (platformer.typeSelect == Platformer.TypeAttack.Melee)
        {
            attackCmpnt.enabled = true;
        }
        Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<UltiLaytonBlock>())
        {
            UltiLaytonBlock ultiBlock = collision.GetComponent<UltiLaytonBlock>();
            if(ultiBlock.idBlock == indexBlock)
            {
                if (ultiBlock.idBlock < 2)
                {
                    Destroy(collision.gameObject);
                    indexBlock++;
                }
                else
                {
                    Destroy(collision.gameObject);
                    FinishUltimate();
                }
            }
        }
    }
}
