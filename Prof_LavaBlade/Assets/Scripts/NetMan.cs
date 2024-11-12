using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NetMan : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    Spinner laveBladeSpinner;
    // Start is called before the first frame update
    void Start()
    {
        laveBladeSpinner = GameObject.Find("LavaBlade").GetComponent<Spinner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Runs once on each laptop in the game.
    public override void OnJoinedRoom() {
        laveBladeSpinner.transform.eulerAngles = 
            laveBladeSpinner.spin * (float) PhotonNetwork.Time;

        //create my player on all laptops in the room
        Vector3 pos = new Vector3(Random.Range(-8, 8), Random.Range(-8, 8), 0);
        PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity);
    }    
}
