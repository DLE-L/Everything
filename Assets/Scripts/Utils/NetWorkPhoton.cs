using System.Collections.Generic;
using System.Text;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Utils
{
  public class NetWorkPhoton : MonoBehaviourPunCallbacks
  {
    private const byte MAX_PLAYERS = 0b10;
    private const string CHARACTER_SET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const int CODE_LENGTH = 6;

    private HashSet<string> RoomID = new();

    string gameVersion = "1";
    void Awake()
    {
      PhotonNetwork.AutomaticallySyncScene = true;

      
    }

    void Start()
    {
      Connect();
    }

    public void Connect()
    {
      if (PhotonNetwork.IsConnected == false)
      {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
      }
    }

    public void CreateRoom()
    {
      string roomId = GenerateRoomCode();
      while (RoomID.Contains(roomId))
      {
        roomId = GenerateRoomCode();
      }

      RoomID.Add(roomId);

      RoomOptions options = new();
      options.MaxPlayers = MAX_PLAYERS;

      PhotonNetwork.CreateRoom(roomId, options);
    }

    public void JoinRoom(string roomId)
    {
      PhotonNetwork.JoinRoom(roomId);
    }

    public string GenerateRoomCode()
    {
      StringBuilder roomID = new();
      for (int i = 0; i < CODE_LENGTH; i++)
      {
        int rand = UnityEngine.Random.Range(0, CHARACTER_SET.Length);
        roomID.Append(CHARACTER_SET[rand]);
      }
      return roomID.ToString();
    }

    public override void OnJoinedRoom()
    {
      Debug.Log("PUN: OnJoinedRoom()");
      Debug.Log($"{PhotonNetwork.CurrentRoom.Name}");
      //PhotonNetwork.Instantiate()
    }

    public override void OnConnectedToMaster()
    {
      Debug.Log("PUN: OnConnectedToMaster()");
    }

    public override void OnCreatedRoom()
    {
      Debug.Log($"PUN: OnCreatedRoom()");
    }    


    public override void OnDisconnected(DisconnectCause cause)
    {
      Debug.LogWarningFormat("PUN: OnDisconnected() was called by PUN with reason {0}", cause);
    }
  }
}