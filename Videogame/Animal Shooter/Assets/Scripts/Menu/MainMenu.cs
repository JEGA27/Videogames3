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

    [Space]
    public GameObject stage_1_cam;

    [Space]
    public GameObject StageCanvas_Play;

    [Space]
    public GameObject StageCanvas_Options;

    [Space]
    public GameObject StageCanvas_Extras;

    private int currentCanvasTab;

    void Start() 
    {
        stageCam = 0;
        StageCanvas_0.SetActive(true);
        StageCanvas_Play.SetActive(false);
        StageCanvas_Options.SetActive(false);
        StageCanvas_Extras.SetActive(false);
        currentCanvasTab = -1;
    }

    void Update()
    {
        switch(stageCam) 
        {
            case 0:
                cam.transform.RotateAround(rot_camera.transform.position, Vector3.up, rot_velocity * Time.deltaTime);
                break;
            case 1:
                StageCanvas_0.SetActive(false);

                StageCanvas_Options.SetActive(true);
                StageCanvas_Play.SetActive(false);
                StageCanvas_Extras.SetActive(false);
                break;
            case 2:
                stage_1_cam.SetActive(true);
                cam.SetActive(false);
                StageCanvas_0.SetActive(false);

                StageCanvas_Options.SetActive(false);
                StageCanvas_Play.SetActive(true);
                StageCanvas_Extras.SetActive(false);
                break;
            case 3:
                StageCanvas_0.SetActive(false);

                StageCanvas_Options.SetActive(false);
                StageCanvas_Play.SetActive(false);
                StageCanvas_Extras.SetActive(true);
                break;
            default:
                cam.transform.RotateAround(rot_camera.transform.position, Vector3.up, rot_velocity * Time.deltaTime);
                break;
        }

        if (Input.anyKeyDown && stageCam == 0)
        {
            stageCam = 2;
        }
        
        if (Input.GetKeyDown("right"))
        {
            if (stageCam == 3)
            {
                stageCam = 1;
            }
            else
            {
                stageCam += 1;
            }
        }

        if (Input.GetKeyDown("left"))
        {
            if (stageCam == 1)
            {
                stageCam = 3;
            }
            else
            {
                stageCam -= 1;
            }
        }
        
    }

    public void ShowOptions()
    {
        stageCam = 1;
    }

    public void ShowPlay()
    {
        stageCam = 2;
    }

    public void ShowExtras()
    {
        stageCam = 3;
    }

    public void Play() {
        SceneManager.LoadScene(1);
    }

    public void Quit() {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }
}
