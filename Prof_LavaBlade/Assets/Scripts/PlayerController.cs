using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPunCallbacks
{
    Text namePlate;
    PhotonView myNetView;
    // Start is called before the first frame update
    void Start()
    {
        myNetView = GetComponent<PhotonView>();
        
        //update name plate to this player's owner's name
        namePlate = GetComponentInChildren<Text>();
        namePlate.text = myNetView.Owner.NickName;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
