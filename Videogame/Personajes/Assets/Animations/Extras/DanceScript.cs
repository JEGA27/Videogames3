using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceScript : MonoBehaviour
{
    [SerializeField]
    Animator racoonAnim;
    [SerializeField]
    Animator robotAnim;
    [SerializeField]
    GameObject musicSource;
    [SerializeField]
    AudioClip audio;

    bool dancing = false;
    AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        music = musicSource.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Dance();
    }

    void Dance()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            dancing = !dancing;
        }

        if(dancing)
        {
            music.PlayOneShot(audio, 0.1f);
        }
        else
        {
            music.Stop();
        }

        racoonAnim.SetBool("Dance", dancing);
        robotAnim.SetBool("Dance", dancing);

    }
}
