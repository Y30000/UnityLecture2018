using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Health : NetworkBehaviour
{
    public int maxHealth = 100;
    [SyncVar(hook ="OnChangeHealth")]   //싱크 변수
    public float currentHealth = 100;   //후킹 걸면 값은 자동으로 안바뀜
    public bool destoryOnDeath;

    public RectTransform healthBar;
    public event System.Action OnDeath;

    private void Start()
    {
        RedrawHealthBar();
    }

    void RedrawHealthBar()
    {
        healthBar.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);
    }

    public void DecreaseHP(float amount)
    {
        if (!isServer)  //서버에서만 돌아감
            return;
        
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            if (destoryOnDeath)
                Destroy(gameObject);
            else
            {
                currentHealth = maxHealth;
                RpcRespawn();
            }
            //Die();
        }
    }

    [ClientRpc]
    void RpcRespawn()      //Remote Procedure Call  원격 함수 호출    //서버가 호출하는데 클라에서만 실행됨
    {
        if (isLocalPlayer)  //isLocalPlayer 은 컨트롤 권한을 가진놈밖에 없음  //내려갔다 다시 올라간뒤 뿌려주는 개념
            transform.position = Vector3.zero;
    }

    void OnChangeHealth(float health)
    {
        currentHealth = health; //후킹 걸면 값은 자동으로 안바뀜
        RedrawHealthBar();
    }

    public void IncreaseHP(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        RedrawHealthBar();
    }

    [ContextMenu("Self Destruct")]
    public void Die() {
        if (OnDeath != null)
            OnDeath();
        //Destroy(gameObject);
    }
}