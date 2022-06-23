using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class NetworkManage : MonoBehaviourPunCallbacks
{
    [Header("LoginPannel")]

    public TMP_InputField TextInputBox;
    // UI pannels
    public GameObject LoginPannel;
    public GameObject LobbyPannel;

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("[Script is running]");

        PhotonNetwork.AutomaticallySyncScene = true;

        ActivatePannel(LoginPannel.name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion

    #region Public Methods

    public void SetPlayerNameOnButtonClick()
    {

        string playerName = TextInputBox.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                PhotonNetwork.ConnectUsingSettings();
            }
        }
    }


    // activate ui pannels or diactivate ui pannels
    public void ActivatePannel(string pannelName)
    {
        LoginPannel.SetActive(LoginPannel.name.Equals(pannelName));
        LobbyPannel.SetActive(LobbyPannel.name.Equals(pannelName));
    }

    //  load game scene
    public void LoadGameScene()
    {
        Debug.Log("[Scene Loaded]");
        PhotonNetwork.LoadLevel("SelectPlayer");
    }

    #endregion

    #region photon callback
    public override void OnConnected()
    {
        Debug.Log("connect To Network");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " Is Connected To Photon Network");
        ActivatePannel(LobbyPannel.name);
        LoadGameScene();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("[Disconnected]: " + cause);
    }
    #endregion
}
