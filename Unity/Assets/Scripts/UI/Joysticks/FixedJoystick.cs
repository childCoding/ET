using ETModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    Vector2 joystickPosition = Vector2.zero;
    public Vector2 JoystickDir = Vector2.zero;
    Camera uiCamera = null;
    Camera mainCamera = null;
    void Start()
    {
        GameObject gameObject = GameObject.Find("UICamera");
        uiCamera = gameObject.GetComponent<Camera>();
        mainCamera = Camera.main;
        joystickPosition = RectTransformUtility.WorldToScreenPoint(uiCamera, background.position);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - joystickPosition;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        
        ClampJoystick();
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
        //Unit unit = UnitComponent.Instance.MyUnit;
        //Vector3 moveVector = (unit.Position + Vector3.right * this.Horizontal + Vector3.forward * this.Vertical) * 1000;
        //SessionComponent.Instance.Session.Send(new Frame_ClickMap() { X = (int)moveVector.x, Z = (int)moveVector.z });
        WorldDirection = mainCamera.cameraToWorldMatrix.MultiplyVector(new Vector3(inputVector.x, 0, -inputVector.y));
        //Log.Debug($"{WorldDirection} {mainCamera.transform.forward}");
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        WorldDirection = Vector3.zero;
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
        //Unit unit = Game.Scene.GetComponent<UnitComponent>().Get(UnitComponent.Instance.MyUnit.Id);
        //Vector3 moveVector = unit.Position * 1000;
        //SessionComponent.Instance.Session.Send(new Frame_ClickMap() { X = (int)moveVector.x, Z = (int)moveVector.z });
    }
}