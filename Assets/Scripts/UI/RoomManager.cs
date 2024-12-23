using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class RoomManager : NetworkManager
{
    // ������� ��� �������� ������ � ������������ � ��� �������
    [HideInInspector]
    public Dictionary<string, List<NetworkConnectionToClient>> rooms = new Dictionary<string, List<NetworkConnectionToClient>>();

    // �������������� ����� ��� ���������� ������
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // ������� ������ ������
        GameObject player = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    // �������� ������ �� ������� ��� ����������
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        foreach (var room in rooms)
        {
            if (room.Value.Contains(conn))
            {
                room.Value.Remove(conn); // ������� ������ �� �������
                break;
            }
        }

        base.OnServerDisconnect(conn); // ������� �������� ������
    }
}
