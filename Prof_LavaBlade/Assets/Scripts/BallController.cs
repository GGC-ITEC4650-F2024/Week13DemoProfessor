using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class BallController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if(PhotonNetwork.IsMasterClient) { //if this pc is the server
            //who did the ball hit?
            GameObject g = collision.gameObject;
            if(g.tag == "Player") {
                //tell all players to add 10 points to this cube.
                PhotonView pv = g.GetComponent<PhotonView>();
                pv.RPC("increaseScore", RpcTarget.AllBuffered, 10);
            }


        
        }
    }
}
