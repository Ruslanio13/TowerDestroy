using UnityEngine;
using UnityEngine.EventSystems;

public class AimButton : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Gun _gun;
    public bool isPressed;
    public static AimButton instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void OnUpdateSelected(BaseEventData data)
    {
        if (isPressed)
        {
            _gun.UpdateArrow();
        }
    }
    public void OnPointerDown(PointerEventData data)
    {
        isPressed = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
        _gun.StartFire();
    }

    public void EnableButton() => gameObject.SetActive(true);
}
