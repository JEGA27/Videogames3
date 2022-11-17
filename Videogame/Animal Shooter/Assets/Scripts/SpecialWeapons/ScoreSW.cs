using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ScoreSW : MonoBehaviour
{
    [Header("Scores")]
    public int globalPoints;
    public int globalTrash;
    public int trashPicked;
    public int trashDelivered;
    public int trashRobbed;
    public int eliminations;

    [Header("Score Multipliers")]
    public int trashMultPickG;
    public int trashMultPickSW;

    public int trashMultRobG;
    public int trashMultRobSW;
    
    public int trashMultDeliverG;
    public int trashMultDeliverSW;

    public int eliminationMultG;
    public int eliminationMultSW;

    [Header("Special Weapon")]
    public int specialWeaponPoints;
    public int specialWeaponCost;
    public int specialWeaponProgress;
    public bool specialWeaponReady;
    public string idProgress;

    // Start is called before the first frame update
    void Start()
    {
        // Scores
        globalPoints = 0;
        globalTrash = 0;
        trashPicked = 0;
        trashDelivered = 0;
        trashRobbed = 0;
        eliminations = 0;

        // Score Multipliers (Default)
        trashMultPickG = 1;
        trashMultPickSW = 1;

        trashMultRobG = 5;
        trashMultRobSW = 5;

        trashMultDeliverG = 3;
        trashMultDeliverSW = 3;

        eliminationMultG = 30;
        eliminationMultSW = 30;

        // Special Weapon Settings (Default)
        specialWeaponCost = 200;
        specialWeaponProgress = 0;
        idProgress = PhotonNetwork.LocalPlayer.UserId + "SWProgress";
        specialWeaponPoints = (int)PhotonNetwork.CurrentRoom.CustomProperties[idProgress];
        specialWeaponReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Change())
            UpdateScore();
    }

    void UpdateScore()
    {
        // Update Global Points
        globalPoints += (trashPicked * trashMultPickG) + 
                        (trashDelivered * trashMultDeliverG) + 
                        (trashRobbed * trashMultRobG) +
                        (eliminations * eliminationMultG);

        // Update Special Weapon Points
        specialWeaponPoints += (trashPicked * trashMultPickSW) + 
                               (trashDelivered * trashMultDeliverSW) + 
                               (trashRobbed * trashMultRobSW) +
                               (eliminations * eliminationMultSW);

        PhotonNetwork.CurrentRoom.CustomProperties[idProgress] = specialWeaponPoints;

        // Update Special Weapon Progress
        if(specialWeaponPoints != 0)
            specialWeaponProgress = (specialWeaponPoints * 100) / specialWeaponCost;

        globalTrash += trashDelivered;

        if (specialWeaponProgress >= 100)
            specialWeaponReady = true;
        
        ResetScores();
    }

    void ResetScores()
    {
        // Reset Scores
        trashPicked = 0;
        trashDelivered = 0;
        trashRobbed = 0;
        eliminations = 0;
    }

    bool Change()
    {
        if(trashPicked != 0 || trashDelivered != 0 || trashRobbed != 0 || eliminations != 0)
            return true;
        else
            return false;
    }
}
