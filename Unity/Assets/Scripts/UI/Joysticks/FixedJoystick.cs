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
        WorldDirection = mainCamera.cameraToWorldMatrix.MultiplyVector(new Vector3(inputVector.x, 0, -inputVector.y));
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        IsJoystick = true;
        OnDrag(eventData);
    }
    private bool IsJoystick = false;
    public override void OnPointerUp(PointerEventData eventData)
    {
        IsJoystick = false;
        WorldDirection = Vector3.zero;
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
    public void Update()
    {
        if (IsJoystick)
            return;
        inputVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }else if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //move
        }
        WorldDirection = mainCamera.cameraToWorldMatrix.MultiplyVector(new Vector3(inputVector.x, 0, -inputVector.y));
    }
}