using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator transition;
    public float transitionTime;

    private void Awake()
    {
        transition = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void End()
    {
        transition.SetTrigger("Start");
    }
}
