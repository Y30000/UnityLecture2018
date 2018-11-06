using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesSounds : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip[] sound;
    private AudioClip soundClip;

    // Use this for initialization
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    public void PlaySound()
    {
        int index = Random.Range(0, sound.Length);
        soundClip = sound[index];
        audioSource.clip = soundClip;
        audioSource.Play();
    }
}
