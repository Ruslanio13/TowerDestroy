using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public static CannonBall instance;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _firePower;
    [SerializeField] private Player _enemyPlayer;
    [SerializeField] private Gun _playerGun;
    [SerializeField] private GameObject _cannonBallPool;
    [SerializeField] private Shield _enemyShield;
    [SerializeField] private Player _player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        _rigidbody.gravityScale = 0;
        _rigidbody.velocity = Vector2.zero;
    }

    public void StartFire(Vector2 direction)
    {
        if (_rigidbody.gravityScale == 0)
        {
            _rigidbody.gravityScale = 1;
            gameObject.transform.position = _playerGun.GetMuzzlePosition();
            _rigidbody.velocity = (direction * _firePower);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tower" || collision.gameObject.tag == "Gun")
            _enemyPlayer.GetDamage();
        if (collision.gameObject.tag == "Shield")
            collision.gameObject.GetComponentInParent<Shield>().GetDamage();
        gameObject.transform.position = _cannonBallPool.transform.position;
        _rigidbody.velocity = Vector2.zero;
        TurnManager.instance.SetFireAbbilityAllPlayers();
        _rigidbody.gravityScale = 0;
    }
}
