using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
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
    public GameObject Panel_PlayJoin;
    public GameObject Panel_PlayCreate;

    [Space]
    public GameObject StageCanvas_Options;
    public GameObject optionsPanel;
    public Slider sensitivitySlider;
    public Text sensitivityTxt;
    public GameObject controlsPanel;

    [Space]
    public GameObject StageCanvas_Extras;
    public GameObject videoPanel;
    public Text videoName;
    public VideoClip raccoonMov;
    public VideoClip ratMov;
    public RawImage videoRaw;
    public VideoPlayer videoPlayer;

    [Space]
    public GameObject aboutUSPanel;
    public GameObject aboutUSImage;

    [Space]
    public GameObject StageCanvas_Loading;

    private int currentCanvasTab;

    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListPrefab;

    void Start() 
    {
        stageCam = 0;
        StageCanvas_0.SetActive(true);

        StageCanvas_Play.SetActive(false);

        StageCanvas_Options.SetActive(false);
        StageCanvas_Extras.SetActive(false);
        videoPanel.SetActive(false);
        StageCanvas_Loading.SetActive(false);
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
                GameManager.sensitivity = sensitivitySlider.value;
                sensitivityTxt.text = sensitivitySlider.value.ToString();
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
                StageCanvas_Loading.SetActive(false);
                break;
            case 3:
                StageCanvas_0.SetActive(false);

                StageCanvas_Options.SetActive(false);
                StageCanvas_Play.SetActive(false);
                StageCanvas_Extras.SetActive(true);
                
                break;
            case 4:
                StageCanvas_Loading.SetActive(true);

                StageCanvas_0.SetActive(false);
                StageCanvas_Options.SetActive(false);
                StageCanvas_Play.SetActive(false);
                StageCanvas_Extras.SetActive(false);
                break;
            default:
                cam.transform.RotateAround(rot_camera.transform.position, Vector3.up, rot_velocity * Time.deltaTime);
                break;
        }

        if (Input.anyKeyDown && stageCam == 0)
        {
            stageCam = 4;
            // Connect to Photon
            PhotonNetwork.ConnectUsingSettings();
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
        PlaySounds menu = GetComponent<PlaySounds>();
        menu.PlaySound(1);
    }

    public void ShowControls()
    {
        controlsPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ShowOptionsPanel()
    {
        controlsPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void ShowPlay()
    {
        stageCam = 2;
        PlaySounds menu = GetComponent<PlaySounds>();
        menu.PlaySound(1);
    }

    public void ShowExtras()
    {
        stageCam = 3;
        PlaySounds menu = GetComponent<PlaySounds>();
        menu.PlaySound(1);
    }

    public void ShowAboutTW() {
        stageCam = 3;
        videoPanel.SetActive(true);
        aboutUSPanel.SetActive(false);
        PlaySounds menu = GetComponent<PlaySounds>();
        menu.PlaySound(1);
    }

    public void PlayVideoRaccon()
    {
        videoPanel.SetActive(true);
        videoName.text = Characters.Raccoon.name;
        videoRaw.texture = videoPlayer.texture;
        videoPlayer.clip = raccoonMov;
        videoPlayer.Prepare();
        videoPlayer.Play();
        PlaySounds menu = GetComponent<PlaySounds>();
        menu.PlaySound(1);
    }

    public void PlayVideoRat()
    {
        videoPanel.SetActive(true);
        videoName.text = Characters.Rat.name;
        videoRaw.texture = videoPlayer.texture;
        videoPlayer.clip = ratMov;
        videoPlayer.Prepare();
        videoPlayer.Play();
        PlaySounds menu = GetComponent<PlaySounds>();
        menu.PlaySound(1);
    }

    public void ShowAboutUS() {
        aboutUSPanel.SetActive(true);
        videoPanel.SetActive(false);
        PlaySounds menu = GetComponent<PlaySounds>();
        menu.PlaySound(1);
    }

    public void ShowJoinPanel()
    {
       Panel_PlayJoin.SetActive(true);
       Panel_PlayCreate.SetActive(false);
       PlaySounds menu = GetComponent<PlaySounds>();
       menu.PlaySound(1);
    }

    public void ShowCreatePanel()
    {
       Panel_PlayJoin.SetActive(false);
       Panel_PlayCreate.SetActive(true);
       PlaySounds menu = GetComponent<PlaySounds>();
       menu.PlaySound(1);
    }

    public void Play() {
        SceneManager.LoadScene(1);
        PlaySounds menu = GetComponent<PlaySounds>();
        menu.PlaySound(1);
    }

    public void Quit() {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }

    // Photon connection to master
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    // Photon connection to lobby
    public override void OnJoinedLobby()
    {
        stageCam = 2;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform child in roomListContent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomListPrefab, roomListContent).GetComponent<ListItem>().SetUp(roomList[i]);
        }
    }

}
