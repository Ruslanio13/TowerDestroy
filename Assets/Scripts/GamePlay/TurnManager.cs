using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Player _enemy;
    public static TurnManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SetFireAbbilityAllPlayers()
    {
        _player.SetFireAbbility(!_player.GetFireAbbility());
        _enemy.SetFireAbbility(!_enemy.GetFireAbbility());
    }
}
