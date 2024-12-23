using UnityEngine;
using Mirror;
using UnityEditor.EditorTools;

public class RoomPlayer : NetworkBehaviour
{
    private RoomManager roomManager;

    void Start()
    {
        roomManager = (RoomManager)NetworkManager.singleton; // Получаем ссылку на RoomManager
    }
     
    // Команда для создания комнаты
    [Command]
    public void CmdCreateRoom(string roomId)
    {
        if (!roomManager.rooms.ContainsKey(roomId))
        {
            roomManager.rooms.Add(roomId, new System.Collections.Generic.List<NetworkConnectionToClient>());
            roomManager.rooms[roomId].Add(connectionToClient);

            // Информируем клиента о создании комнаты
            TargetRoomCreated(connectionToClient, true, "Room successfully created");
        }
        else
        {
            TargetRoomCreated(connectionToClient, false, "Room with this ID already exists");
        }
    }

    // Команда для присоединения к комнате
    [Command]
    public void CmdJoinRoom(string roomId)
    {
        if (roomManager.rooms.ContainsKey(roomId))
        {
            roomManager.rooms[roomId].Add(connectionToClient);

            // Информируем клиента о присоединении
            TargetRoomJoined(connectionToClient, true, "You have joined the room");
        }
        else
        {
            TargetRoomJoined(connectionToClient, false, "Room with this ID does not exist");
        }
    }

    // Ответ клиенту о результате создания комнаты
    [TargetRpc]
    void TargetRoomCreated(NetworkConnection target, bool success, string message)
    {
        Debug.Log(message); // Отображаем в консоли результат
    }

    // Ответ клиенту о результате присоединения к комнате
    [TargetRpc]
    void TargetRoomJoined(NetworkConnection target, bool success, string message)
    {
        Debug.Log(message); // Отображаем в консоли результат
    }
}
