using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shield : MonoBehaviour
{
    [SerializeField] private Button _shieldButton;
    [SerializeField] private Sprite _activeButtonImg;
    [SerializeField] private Sprite _notActiveButtonImg;
    [SerializeField] private TextMeshProUGUI _timerTMPro;
    [SerializeField] private GameObject _timerField;
    [SerializeField] private GameObject _shield;
    [SerializeField] private bool _isBot;
    private bool _isActive;
    private int _healthPoints = 3;
    private int _botTimer;

    private void Start()
    {
        _isActive = false;
        if (!_isBot)
            _shieldButton.onClick.AddListener(() => ActivateShield());
        else
            StartCoroutine(BotSleepShieldTimer());
    }

    public bool IsActive() => _isActive;

    public void ActivateShield()
    {
        _isActive = true;
        _shield.SetActive(true);
        StartCoroutine(Timer());
        if (!_isBot)
        {
            _timerField.SetActive(true);
            _shieldButton.image.sprite = _activeButtonImg;
            _shieldButton.interactable = false;
        }
    }

    private IEnumerator Timer(bool isActive = true)
    {
        int timer = 15;
        while (timer >= 0)
        {
            if (!_isBot)
                _timerTMPro.text = timer.ToString("0:00");
            timer -= 1;
            yield return new WaitForSeconds(1f);
        }
        if (isActive)
        {
            HideShield();
        }
        else if (!_isBot)
        {
            _shieldButton.interactable = true;
            _shieldButton.image.sprite = _activeButtonImg;
            _timerField.SetActive(false);
        }

    }

    private IEnumerator BotSleepShieldTimer()
    {
        int timer = 0;
        _botTimer = GetRandomTimeForBot();
        while (timer < _botTimer)
        {
            timer += 1;
            yield return new WaitForSeconds(1);
        }
        ActivateShield();
        StartCoroutine(Timer());
    }

    public void GetDamage()
    {
        _healthPoints -= 1;
        if (_healthPoints == 0)   
            HideShield();      
    }

    private void HideShield()
    {
        _shield.SetActive(false);
        StopAllCoroutines();
        _isActive = false;
        _healthPoints = 3;
        if (!_isBot)
        {
            _shieldButton.image.sprite = _notActiveButtonImg;
            StartCoroutine(Timer(false));
        }
        else
        {
            StartCoroutine(BotSleepShieldTimer());
        }
    }

    private int GetRandomTimeForBot() => Random.Range(16, 32);
}
