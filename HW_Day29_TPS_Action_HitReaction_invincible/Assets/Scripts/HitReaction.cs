using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReaction : MonoBehaviour {

    public GameObject hitFXPrefab;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int amount , string reactionType ,Vector3 knuckBackDir)
    {
        if (anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("MoveAround"))
            anim.SetTrigger(reactionType);

        GetComponent<Health>().DecreaseHP(amount);
        GetComponent<Rigidbody>().velocity = knuckBackDir * 2;
        GameObject clone = Instantiate(hitFXPrefab,
                                       transform.position,
                                       Quaternion.LookRotation(-knuckBackDir));

        Destroy(clone, 1.5f);
    }

    public void TakeDamage(int amount, Vector3 knuckBackDir)
    {

        GetComponent<Health>().DecreaseHP(amount);
        GetComponent<Rigidbody>().velocity = knuckBackDir * 2;
        GameObject clone = Instantiate(hitFXPrefab,
                                       transform.position,
                                       Quaternion.LookRotation(-knuckBackDir));

        Destroy(clone, 1.5f);
    }
}
