using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSelection : MonoBehaviourPunCallbacks
{

    public GameObject[] playerUI;
    private int playerSelectedNumber;
    // Start is called before the first frame update
    void Start()
    {
        playerSelectedNumber = 0;
        // playerUI[playerSelectedNumber].SetActive(true);
        ShowPlayerUI(playerSelectedNumber);

    }

    #region public methods
    public void NextButtonnClicked()
    {
        playerSelectedNumber += 1;
        if (playerSelectedNumber >= playerUI.Length)
        {
            playerSelectedNumber = 0;
        }
        ShowPlayerUI(playerSelectedNumber);
    }

    public void PrevButtonClicked()
    {
        playerSelectedNumber -= 1;
        if (playerSelectedNumber < 0)
        {
            playerSelectedNumber = playerUI.Length - 1;
        }
        ShowPlayerUI(playerSelectedNumber);
    }

    public void OnJoinButtonClicked()
    {
        Debug.Log("[Join Button Clicked]");
        PhotonNetwork.JoinRandomRoom();
    }
    public void LoadGameScene()
    {
        Debug.Log("[Scene Loaded]");
        PhotonNetwork.LoadLevel("PlayScene");
    }
    #endregion

    #region private methods
    private void ShowPlayerUI(int y)
    {
        Debug.Log("[Player Selected Number]: " + y);
        foreach (GameObject player in playerUI)
        {
            Debug.Log("[Loop Executing]");
            player.SetActive(false);
        }
        playerUI[y].SetActive(true);


        ExitGames.Client.Photon.Hashtable playerSelectionProps = new ExitGames.Client.Photon.Hashtable() { { ArcharHGame.playerSelectedNumber, playerSelectedNumber } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerSelectionProps);


    }
    #endregion


    #region Photon Callbacks

    // Room Callbacks
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("[Player entered room]: " + newPlayer.NickName);
    }

    // Match Making callbacks
    public override void OnCreatedRoom()
    {
        Debug.Log("[Created Room]: " + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnCreateRoomFailed(short returnCode, string messgae)
    {
        Debug.Log("[Room Creation Failed]: " + messgae);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("[Joined Room]: " + PhotonNetwork.LocalPlayer.NickName);
        LoadGameScene();
    }

    public override void OnJoinRoomFailed(short returnCode, string messgae)
    {
        Debug.Log("[Join room Failed]: " + messgae);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("[Join Random room Failed]: " + message);

        string roomname = "Room" + Random.Range(10, 100000);

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(roomname, options);

    }
    #endregion
}
