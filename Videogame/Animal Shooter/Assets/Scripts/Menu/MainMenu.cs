using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject cam;
    public GameObject rot_camera;
    public float rot_velocity;
    private int stageCam;
    public GameObject StageCanvas_0;

    public GameObject stage_1_cam;
    public GameObject StageCanvas_Play;

    void Start() 
    {
        stageCam = 0;
        StageCanvas_0.SetActive(true);
    }

    void Update()
    {
        switch(stageCam) 
        {
            case 0:
                cam.transform.RotateAround(rot_camera.transform.position, Vector3.up, rot_velocity * Time.deltaTime);
                break;
            case 1:
                stage_1_cam.SetActive(true);
                cam.SetActive(false);
                StageCanvas_0.SetActive(false);
                StageCanvas_Play.SetActive(true);
                break;
            default:
                cam.transform.RotateAround(rot_camera.transform.position, Vector3.up, rot_velocity * Time.deltaTime);
                break;
        }

        if (Input.anyKeyDown && stageCam == 0)
        {
            stageCam += 1;
        }

        
    }

    public void Play() {
        SceneManager.LoadScene(1);
    }

    public void Quit() {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }
}
