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
    // public int globalEliminations;

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
    
    // IDs
    public string idProgress;
    string idScore;
    string idTrash;
    string idKills;



    // Start is called before the first frame update
    void Start()
    {
        // IDs
        idProgress = PhotonNetwork.LocalPlayer.UserId + "SWProgress";
        idScore = PhotonNetwork.LocalPlayer.UserId + "Score";
        idTrash = PhotonNetwork.LocalPlayer.UserId + "Trash";
        idKills = PhotonNetwork.LocalPlayer.UserId + "Kills";

        // Scores
        globalPoints = (int)PhotonNetwork.CurrentRoom.CustomProperties[idScore];
        globalTrash = (int)PhotonNetwork.CurrentRoom.CustomProperties[idTrash];
        trashPicked = 0;
        trashDelivered = 0;;
        trashRobbed = 0;
        eliminations = (int)PhotonNetwork.CurrentRoom.CustomProperties[idKills];
        // globalEliminations = (int)PhotonNetwork.CurrentRoom.CustomProperties[idKills];

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
        specialWeaponPoints = (int)PhotonNetwork.CurrentRoom.CustomProperties[idProgress];
        specialWeaponReady = false;

        if(specialWeaponPoints != 0)
            specialWeaponProgress = (specialWeaponPoints * 100) / specialWeaponCost;
    }

    // Update is called once per frame
    void Update()
    {   
        // globalEliminations = (int)PhotonNetwork.CurrentRoom.CustomProperties[idKills];
        if (Change())
            UpdateScore();
        // if (globalTrash != (int)PhotonNetwork.CurrentRoom.CustomProperties[idTrash])
        //     PhotonNetwork.CurrentRoom.CustomProperties[idTrash] = globalTrash;
    }

    void UpdateScore()
    {   
        // Update kills
        int diffElim = 0;
        int totalKills = (int)PhotonNetwork.CurrentRoom.CustomProperties[idKills];
        if (totalKills > eliminations)
            diffElim = totalKills - eliminations;

        // Update Global Points
        globalPoints += (trashPicked * trashMultPickG) + 
                        (trashDelivered * trashMultDeliverG) +
                        (trashRobbed * trashMultRobG) +
                        (diffElim * eliminationMultG);

        // Update Special Weapon Points
        specialWeaponPoints += (trashPicked * trashMultPickSW) + 
                               (trashDelivered * trashMultDeliverSW) + 
                               (trashRobbed * trashMultRobSW) +
                               (diffElim * eliminationMultSW);

        // Update kills count
        eliminations = totalKills;

        // Update Special Weapon Progress
        if(specialWeaponPoints != 0)
            specialWeaponProgress = (specialWeaponPoints * 100) / specialWeaponCost;

        globalTrash += trashDelivered;

        if (specialWeaponProgress >= 100)
            specialWeaponReady = true;

        var hash1 = PhotonNetwork.CurrentRoom.CustomProperties;
        hash1[idScore] = globalPoints;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash1);
        var hash2 = PhotonNetwork.CurrentRoom.CustomProperties;
        hash2[idTrash] = globalTrash;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash2);
        var hash3 = PhotonNetwork.CurrentRoom.CustomProperties;
        hash3[idProgress] = specialWeaponPoints;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash3);
        
        // PhotonNetwork.CurrentRoom.CustomProperties[idProgress] = specialWeaponPoints;
        // PhotonNetwork.CurrentRoom.CustomProperties[idScore] = globalPoints;
        // PhotonNetwork.CurrentRoom.CustomProperties[idTrash] = globalTrash;

        ResetScores();
    }

    void ResetScores()
    {
        // Reset Scores
        trashPicked = 0;
        trashDelivered = 0;
        trashRobbed = 0;
    }

    bool Change()
    {
        if(trashPicked != 0 || trashDelivered != 0 || trashRobbed != 0 || eliminations != (int)PhotonNetwork.CurrentRoom.CustomProperties[idKills] || globalTrash != (int)PhotonNetwork.CurrentRoom.CustomProperties[idTrash])
            return true;
        else
            return false;
    }
}
