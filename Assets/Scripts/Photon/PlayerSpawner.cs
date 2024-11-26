using UnityEngine;
using Fusion;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject _playerPrefab;
    public GameObject[] _spawnPoss;

    public void PlayerJoined(PlayerRef player)
    {
        if(player == Runner.LocalPlayer)
        {
            Runner.Spawn(_playerPrefab, _spawnPoss[Random.Range(0, _spawnPoss.Length)].transform.position, Quaternion.identity);
        }
    }

    
}
