using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    public bool open;
    [SerializeField]
    GameObject doors;
    Animator doorsAnim;
    private AudioController audioController;
    // Start is called before the first frame update
    void Start()
    {
        doorsAnim = doors.GetComponent<Animator>();
        open = false;
        audioController = doors.GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            open = !open;
            doorsAnim.SetBool("Open", open);

            if(open)
            {
                audioController.AudioSelect(0, 0.5f);
            }
            else
            {
                audioController.AudioSelect(1, 0.5f);
            }
        }


    }
}
