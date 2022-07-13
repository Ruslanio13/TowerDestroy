using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CannonBall _cannonBall;
    [SerializeField] private GameObject _endMuzzle;
    [SerializeField] private GameObject _baseGun;
    [SerializeField] private bool _isPlayer;
    [SerializeField] private Transform _enemyTower;
    [SerializeField] private List<float> _botAimsAngles = new List<float>();
    [SerializeField] private Shield _enemyShield;
    [SerializeField] private Shield _botShield;
    private float _destinationX;
    private float _destinationY;

    public IEnumerator TakeAim()
    {
        float aimAngle;
        if (_enemyShield.IsActive()) 
            aimAngle = _botAimsAngles[Random.Range(5, _botAimsAngles.Count)];
        else
            aimAngle = _botAimsAngles[Random.Range(0, _botAimsAngles.Count)];

        while (Mathf.Abs(transform.eulerAngles.z - aimAngle) > 1f)
        {
            if (transform.eulerAngles.z > 300 && aimAngle>300 && aimAngle > transform.eulerAngles.z ||
                transform.eulerAngles.z < 300 && aimAngle < 300 && aimAngle > transform.eulerAngles.z ||
                transform.eulerAngles.z > 300 && aimAngle < 300 && aimAngle < transform.eulerAngles.z
                )
                transform.eulerAngles += new Vector3(0, 0, 30f) * Time.deltaTime;
            else
                transform.eulerAngles -= new Vector3(0, 0, 30f) * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        StartFire();
    }

    public void UpdateArrow()
    {
        if (_player.GetFireAbbility())
        {
            _destinationX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - _baseGun.transform.localPosition.x;
            _destinationY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - _baseGun.transform.localPosition.y;

            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > _baseGun.transform.position.x)
            {
                _destinationX *= -1;
                if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y > _baseGun.transform.position.y)
                    _destinationY *= -1;
            }
            if (transform.rotation.z < 0.5f)
            {
                if (Mathf.Abs(_destinationY) > .5f)
                    if (_destinationY > 0f)
                    {
                        transform.eulerAngles -= new Vector3(0, 0, 30f) * Time.deltaTime;
                    }
                    else
                    {
                        transform.eulerAngles += new Vector3(0, 0, 30f) * Time.deltaTime;
                    }
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 60 * Mathf.Sign(_destinationY));
            }
        }
    }

    public void StartFire()
    {
        if (_player.GetFireAbbility())
        {
            float t = (transform.eulerAngles.z + 150f);
            if (!_isPlayer)
                t -= 90f;
            var x = Mathf.Cos(t / 180 * Mathf.PI);
            var y = Mathf.Sin(t / 180 * Mathf.PI);
            _cannonBall.StartFire(new Vector2(_isPlayer ? -Mathf.Abs(x) : Mathf.Abs(x), y).normalized);
        }
    }

    public Vector2 GetMuzzlePosition() => _endMuzzle.transform.position;

}
