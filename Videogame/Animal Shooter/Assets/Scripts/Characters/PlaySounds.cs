using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    public List<AudioClip> audioClips;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int index) {
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }
}
