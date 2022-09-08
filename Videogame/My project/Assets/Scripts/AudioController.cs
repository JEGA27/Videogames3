using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] sonidos;
    private AudioSource audio;
    // Start is called before the first frame update
    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AudioSelect(int clip, float volume)
    {
        audio.Stop();
        audio.PlayOneShot(sonidos[clip], volume);
    }


}
