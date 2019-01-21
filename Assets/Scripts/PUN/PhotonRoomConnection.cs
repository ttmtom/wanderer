﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fyp.Constant;

namespace Fyp.Game.Network {
    public class PhotonRoomConnection : Photon.PunBehaviour {
        public bool spawned = false;
        public void CreateRoom(string playerName) {
            Debug.Log("create room");
            if (PhotonNetwork.CreateRoom(playerName, new RoomOptions() { MaxPlayers = 2 }, null)) {
                Debug.Log("create room success");
            }
            else {
                Debug.Log("create room fail");
            }
        }

        public void JoinRoom(string targetName) {
            if(PhotonNetwork.JoinRoom(targetName)) {
                Debug.Log("join room success");
            } else {
                Debug.Log("join room fail");
            }
        }

        // public override void OnConnectedToMaster() {
        //     PhotonNetwork.JoinLobby(TypedLobby.Default);
        //     Debug.Log("OnConnectedToMaster");
        // }

        public void ConnectToServer() {
            Debug.Log("connect");
            if(PhotonNetwork.ConnectUsingSettings(GameConstant.GAME_VERSION)) {
                Debug.Log("Connected to server");
                // NetworkChangeScene.ChangeToScene((int) GameConstant.ScenceName.WaitingRoom);
                UI.ChangeSence.MenuToWaitingRoom();
            } else {
                Debug.Log("Connect to server failed");
            }
        }

        public override void OnJoinedRoom() {
            Debug.Log ("Joined Room");
            Debug.Log(PhotonNetwork.isMasterClient);
            Debug.Log(PhotonNetwork.isNonMasterClientInRoom);
            if(PhotonNetwork.isMasterClient && photonView.isMine) {
                PhotonNetwork.Instantiate("PlayerCharacter", new Vector3(-1f,5f,0f), Quaternion.identity, 0);
                spawned = true;
            } else {
                PhotonNetwork.Instantiate("Player2Character", new Vector3(1f,5f,0f), Quaternion.identity, 0);
                spawned = true;
            }
        }

        public void DisconnectFromServer() {
            Debug.Log("Disconnect");
            PhotonNetwork.Disconnect();
            UI.ChangeSence.WaitingRoomToMenu();
        }
    }
}