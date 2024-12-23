using UnityEngine;
using Mirror;
using UnityEditor.EditorTools;

public class RoomPlayer : NetworkBehaviour
{
    private RoomManager roomManager;

    void Start()
    {
        roomManager = (RoomManager)NetworkManager.singleton; // �������� ������ �� RoomManager
    }
     
    // ������� ��� �������� �������
    [Command]
    public void CmdCreateRoom(string roomId)
    {
        if (!roomManager.rooms.ContainsKey(roomId))
        {
            roomManager.rooms.Add(roomId, new System.Collections.Generic.List<NetworkConnectionToClient>());
            roomManager.rooms[roomId].Add(connectionToClient);

            // ����������� ������� � �������� �������
            TargetRoomCreated(connectionToClient, true, "Room successfully created");
        }
        else
        {
            TargetRoomCreated(connectionToClient, false, "Room with this ID already exists");
        }
    }

    // ������� ��� ������������� � �������
    [Command]
    public void CmdJoinRoom(string roomId)
    {
        if (roomManager.rooms.ContainsKey(roomId))
        {
            roomManager.rooms[roomId].Add(connectionToClient);

            // ����������� ������� � �������������
            TargetRoomJoined(connectionToClient, true, "You have joined the room");
        }
        else
        {
            TargetRoomJoined(connectionToClient, false, "Room with this ID does not exist");
        }
    }

    // ����� ������� � ���������� �������� �������
    [TargetRpc]
    void TargetRoomCreated(NetworkConnection target, bool success, string message)
    {
        Debug.Log(message); // ���������� � ������� ���������
    }

    // ����� ������� � ���������� ������������� � �������
    [TargetRpc]
    void TargetRoomJoined(NetworkConnection target, bool success, string message)
    {
        Debug.Log(message); // ���������� � ������� ���������
    }
}
