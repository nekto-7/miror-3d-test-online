using UnityEngine;
using Mirror;
public class Connection : MonoBehaviour
{
    public NetworkManager networkManager;

    public void JoinClient()
    {
        networkManager.networkAddress  = "localhost";
        networkManager.StartClient();
    }
    private void Check()
    {
        if(!Application.isBatchMode)
        {
            networkManager.StartClient();
        }
    }
}
