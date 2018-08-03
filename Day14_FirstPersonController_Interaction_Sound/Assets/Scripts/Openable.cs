using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : MonoBehaviour {
    Animator anim;
    bool isOpend = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ToggleDoor()
    {
        isOpend = !isOpend;
        anim.SetBool("isOpend", isOpend);
    }
}
