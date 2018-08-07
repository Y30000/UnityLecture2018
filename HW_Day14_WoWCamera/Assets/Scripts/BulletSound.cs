using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSound : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip[] sound;

    // Use this for initialization
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    public void PlaySound()
    {
        int index = Random.Range(0, sound.Length -1);
        //audioSource.clip = sound[index];
        //audioSource.Play();
        audioSource.PlayOneShot(sound[index],1f);   //없으면 default 1f
    }
}