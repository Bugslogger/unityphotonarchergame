using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpwanPlayerInGame : MonoBehaviour
{
    // code added by MrDark
    [Header("Players")]
    public GameObject[] players;
    public Transform[] playersPositions;
    // code ended by MrDark
    // Start is called before the first frame update
    void Start()
    {
        spwanPlayersInGameScene();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // code added by MrDark
    public void spwanPlayersInGameScene()
    {

        object playerSelectedNumber;
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(ArcharHGame.playerSelectedNumber, out playerSelectedNumber))
        {
            Debug.Log("Player Selected: " + (int)playerSelectedNumber);

            int playerActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
            Debug.Log("[Actor Number]: " + playerActorNumber);

            Vector3 pos = playersPositions[playerActorNumber - 1].position;
            Debug.Log("[Player Position]: " + pos);

            PhotonNetwork.Instantiate(players[(int)playerSelectedNumber].name, pos, Quaternion.identity);
        }
    }
    // code end
}
