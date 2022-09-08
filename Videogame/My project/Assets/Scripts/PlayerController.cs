using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector3 PlayerMosueInput;
    private float xRot;
    private float originalSpeed;
    private bool isGrounded = false;
    private bool running = false;
    private bool dancing = false;
    private Animator anim;
    private AudioSource audio;


    [SerializeField]
    private Transform playerCamera;
    [SerializeField]
    private Rigidbody PlayerBody;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float sensitivity;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private AudioClip musicDance;
    [SerializeField]
    private GameObject audioObject;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        originalSpeed = speed;
        audio = audioObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMosueInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();
        Dance();

        anim.SetBool("Jumping", !isGrounded);
        anim.SetBool("Dancing", dancing);
        if(PlayerBody.velocity.y == 0)
        {
            anim.SetFloat("Speed", PlayerBody.velocity.magnitude);
        }

        if(dancing)
        {
            audio.PlayOneShot(musicDance, 0.05f);
        }
        else
        {
            audio.Stop();
        }
    }

    private void MovePlayer()
    {
          Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * speed;

          if(Input.GetKeyDown(KeyCode.LeftShift))
          {
              running = !running;
          }

          if(running)
          {
              speed = originalSpeed*2;
          }
          else
          {
              speed = originalSpeed;
          }

          PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);

          if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
          {
              PlayerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
          }
    }

    private void MovePlayerCamera()
    {
        xRot -= PlayerMosueInput.y * sensitivity;

        transform.Rotate(0f, PlayerMosueInput.x * sensitivity, 0f);
        //playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }

    private void Dance()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            dancing = !dancing;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

  /*  void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exited");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }*/

}
