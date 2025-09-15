using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Utils
{
  public class NetWorkPhoton : MonoBehaviourPunCallbacks
  {
    string gameVersion = "";
    void Awake()
    {
      PhotonNetwork.AutomaticallySyncScene = true; //
    }

    void Start()
    {
      Init();
    }

    public void Init()
    {
      Connect();
    }

    public void Connect()
    {
      if (PhotonNetwork.IsConnected == true)
      {
        PhotonNetwork.JoinRoom(gameVersion);
        Debug.Log("Join Room");
      }
      else
      {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connect");
      }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
      Debug.Log("PUN: OnJoinRandomFailed() was called by PUN. So Create Room");

      PhotonNetwork.CreateRoom(null, new RoomOptions());
    }

    public override void OnJoinedRoom()
    {
       Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN.");
    }

    public override void OnConnectedToMaster()
    {
      Debug.Log("PUN: OnConnectedToMaster() was called by PUN");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
      Debug.LogWarningFormat("PUN: OnDisconnected() was called by PUN with reason {0}", cause);
    }

  }
}