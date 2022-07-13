using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int _startHealthPoints;
    [SerializeField] private Image _healthImg;
    [SerializeField] private bool _canFire;
    [SerializeField] private Gun _gun;
    [SerializeField] private bool _isPlayer;
    private float _healthPoints;

    private void Start()
    {
        _healthPoints = _startHealthPoints;
        _healthImg.fillAmount = _healthPoints / _startHealthPoints;
    }

    public void GetDamage()
    {
        _healthPoints -= 1;
        _healthImg.fillAmount = _healthPoints / _startHealthPoints;
        if (_healthPoints == 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void SetFireAbbility(bool canFire)
    {
        _canFire = canFire;
        if (!_isPlayer && _canFire)
            StartCoroutine(_gun.TakeAim());
    }

    public bool GetFireAbbility() => _canFire;
}