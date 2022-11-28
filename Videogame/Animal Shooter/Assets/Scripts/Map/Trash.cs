using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Trash : MonoBehaviour
{
    PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EraseTrash()
    {
        PV.RPC("EraseTrashRPC", RpcTarget.All);
    }

    [PunRPC]
    public void EraseTrashRPC()
    {
        if(!PV.IsMine) return;
        
        PhotonNetwork.Destroy(gameObject);
    }
}
