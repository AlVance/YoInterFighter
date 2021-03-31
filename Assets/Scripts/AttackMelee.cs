using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMelee : MonoBehaviour
{
    MainManager mainManager;
    Platformer platformer;

    public KeyCode attackKey;

    public GameObject[] attackVisuals;
    public float timeAttack;
    bool canAttack;
    public float radiusAttack;
    float elapsedTime;


    int attackCount;
    public float timeCombo = .5f;
    bool onCombo;

    // Start is called before the first frame update
    void Start()
    {
        mainManager = FindObjectOfType<MainManager>();
        canAttack = true;
        SetGameobjectArray(attackVisuals, -1);
        attackCount = 0;
        for (int i = 0; i < attackVisuals.Length; i++)
        {
            attackVisuals[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            if (canAttack)
            {
                if (attackCount >= attackVisuals.Length)
                {
                    attackCount = 0;
                }
                if (!onCombo)
                {
                    elapsedTime = 0;
                    StopAllCoroutines();
                    StartCoroutine(AttackCicle(timeAttack));
                    SetGameobjectArray(attackVisuals, attackCount);
                    canAttack = false;
                }
                else
                {
                    if (attackCount < attackVisuals.Length - 1)
                    {
                        attackCount++;
                    }
                    else
                    {
                        attackCount = 0;
                    }
                    elapsedTime = 0;
                    StopAllCoroutines();
                    StartCoroutine(AttackCicle(timeAttack));
                    SetGameobjectArray(attackVisuals, attackCount);
                    canAttack = false;
                }
            }
        }
    }


    private IEnumerator AttackCicle(float waitTime)
    {
        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
            onCombo = false;
            yield return new WaitForSeconds(waitTime);
        }
        canAttack = true;
        SetGameobjectArray(attackVisuals, -1);
        onCombo = true;
        yield return new WaitForSeconds(timeCombo);
        onCombo = false;
        attackCount = 0;
        yield return null;
    }

    void SetGameobjectArray(GameObject[] itemsActive, int active)
    {
        for (int i = 0; i < itemsActive.Length; i++)
        {
            if (i == active)
            {
                itemsActive[i].SetActive(true);
                Debug.Log(itemsActive[i] + "Activado");
            }
            else
            {
                itemsActive[i].SetActive(false);
            }

        }
    }
}
