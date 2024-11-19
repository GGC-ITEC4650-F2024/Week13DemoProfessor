using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable //add health to network stream
{
    Text namePlate;
    PhotonView myNetView;
    Rigidbody myBod;
    Transform heathBarTran;
    Text scoreTxt;

    public int health;
    public int score;

    // Happens Before Collisions or Anything Else
    void Awake()
    {
        myBod = GetComponent<Rigidbody>();
        myNetView = GetComponent<PhotonView>();
        heathBarTran = transform.Find("Canvas/GreenHealth").GetComponent<Transform>();
        scoreTxt = transform.Find("Canvas/ScorePlate").GetComponent<Text>();
        namePlate = GetComponentInChildren<Text>(); //finds first Text child
    }

    // Start is called before the first frame update
    void Start()
    {
        //update name plate to this player's owner's name
        namePlate.text = myNetView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        scoreTxt.text = "" + score;
        float hp = health / 100f;
        if (hp > 0)
        {
            heathBarTran.localScale = new Vector3(hp, 1, 1);
        }
        else //DEAD
        {
            heathBarTran.localScale = new Vector3(0, 1, 1);
            myBod.constraints = RigidbodyConstraints.FreezeAll;
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                5);
            namePlate.color = Color.gray;
        }


        if (myNetView.Owner == PhotonNetwork.LocalPlayer)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 f = new Vector3(h, v, 0);
            myBod.AddForce(500 * f * Time.deltaTime);
            //tell all my clones about my new position
            // done by PhotonRigidBodyView component
        }
        else
        {
            //get position from network and update this cube
            // done by PhotonRigidBodyView component
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (myNetView.Owner == PhotonNetwork.LocalPlayer)
        {
            health -= 1;
            //tell all my clones about my health?
        }
    }

    //add health to network stream
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (myNetView.Owner == PhotonNetwork.LocalPlayer)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (int)stream.ReceiveNext();
        }
    }

    [PunRPC]
    public void increaseScore(int n) {
        this.score += n;
    }
}
