using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardMgr : MonoBehaviour
{
    [Header("Timer")]
    public Text textInTimer;
    public int time;

    [Space(20)] //줄 띄우기
    [Header("Left HP Bar")]
    public Image leftFore;
    public Image leftMid;

    [Space(20)] //줄 띄우기
    [Header("Right HP Bar")]
    public Image rightFore;
    public Image rightMid;

    // Use this for initialization
    void Start () {
        if (time == 0)
            time = 99;
        StartCoroutine(Timer());
        leftFore.fillAmount = rightFore.fillAmount = leftMid.fillAmount = rightMid.fillAmount = 1f;
    }
	
    enum myIndex {Left, Right}

    public void Hit()
    {
        StartCoroutine(Damage(myIndex.Left, 0.1f));
        StartCoroutine(Damage(myIndex.Right, 0.15f));
    }

    public void Heal()
    {
        StartCoroutine(Healing(myIndex.Left, 0.1f));
        StartCoroutine(Healing(myIndex.Right, 0.15f));
    }

    IEnumerator Damage(myIndex index, float Damage)
    {
        float remainingDamage = Damage;
        float remainingDuration = 1f;
        Image tImage;
        switch (index)
        {
            case myIndex.Left:
                tImage = leftMid;
                leftFore.fillAmount -= Damage;
                break;
            case myIndex.Right:
                tImage = rightMid;
                rightFore.fillAmount -= Damage;
                break;
            default:
                yield break;
        }

        while (remainingDamage > 0)
        {
            float damagePerFrame = remainingDamage / remainingDuration * Time.deltaTime;

            if (remainingDamage > damagePerFrame)
            {
                tImage.fillAmount -= damagePerFrame;
                remainingDamage -= damagePerFrame;
                remainingDuration -= Time.deltaTime;
            }
            else
            {
                tImage.fillAmount -= remainingDamage;
                remainingDamage = 0;
            }
            yield return null;
        }
    }

    IEnumerator Healing(myIndex index, float heal)
    {
        float remainingHealing = heal;
        float remainingDuration = 1f;
        Image tImage;
        switch (index)
        {
            case myIndex.Left:
                tImage = leftFore;
                leftMid.fillAmount += heal;
                break;
            case myIndex.Right:
                tImage = rightFore;
                rightMid.fillAmount += heal;
                break;
            default:
                yield break;
        }

        while (remainingHealing > 0)
        {
            float HealingPerFrame = remainingHealing / remainingDuration * Time.deltaTime;
            
            if (remainingHealing > HealingPerFrame)
            {
                tImage.fillAmount += HealingPerFrame;
                remainingHealing -= HealingPerFrame;
                remainingDuration -= Time.deltaTime;
            }
            else
            {
                tImage.fillAmount += remainingHealing;
                remainingHealing = 0;
            }
            yield return null;
        }
    }

    IEnumerator Timer()
    {
        while (--time > 0)
        {
            yield return new WaitForSeconds(1f);
            textInTimer.text = "" + time;
        }
    }
}
