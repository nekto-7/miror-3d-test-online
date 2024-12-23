using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class RoomManager : NetworkManager
{
    // Словарь для хранения комнат и подключенных к ним игроков
    [HideInInspector]
    public Dictionary<string, List<NetworkConnectionToClient>> rooms = new Dictionary<string, List<NetworkConnectionToClient>>();

    // Переопределяем метод для добавления игрока
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // Создаем объект игрока
        GameObject player = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    // Удаление игрока из комнаты при отключении
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        foreach (var room in rooms)
        {
            if (room.Value.Contains(conn))
            {
                room.Value.Remove(conn); // Убираем игрока из комнаты
                break;
            }
        }

        base.OnServerDisconnect(conn); // Обычное удаление игрока
    }
}
