using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    [SerializeField] private Image JoystickBorder;
    [SerializeField] private Image JoystickController;
    [SerializeField] Camera Cam;

    private bool joystickTouched;
    private Vector2 JoystickContorllerOrigin;

    void Start()
    {
        JoystickContorllerOrigin = JoystickBorder.rectTransform.transform.position;
        JoystickController.rectTransform.position = JoystickContorllerOrigin;

        joystickTouched = false;
    }

    void Update()
    {
        JoystickContorllerOrigin = JoystickBorder.rectTransform.transform.position;

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Moved)
                {
                    // Move UI to position
                    Vector3 position;
                    RectTransformUtility.ScreenPointToWorldPointInRectangle(JoystickController.rectTransform, new Vector2(touch.position.x, touch.position.y), Cam, out position);

                    // Move if controller is still inside the joystick circle
                    if (IsInRange(position, JoystickBorder.rectTransform))
                    {
                        joystickTouched = true;
                        JoystickController.transform.position = position;
                    }

                }

                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    // Move to center of controller released
                    joystickTouched = false;
                    JoystickController.rectTransform.position = JoystickContorllerOrigin;
                }
            }
        }
    }

    #region Public Functions
    public Vector2 GetDirection()
    {
        Vector2 JoystickControllerLocation = new Vector2(JoystickController.transform.position.x, JoystickController.transform.position.y);

        // Getting the normalized difference between controller location and it's origin
        return Vector3.Normalize(JoystickControllerLocation - JoystickContorllerOrigin);
    }

    public float GetRotation()
    {
        // Getting x and y
        float verticalChange = JoystickController.transform.position.y - JoystickContorllerOrigin.y;
        float horizontalChange = JoystickController.transform.position.x - JoystickContorllerOrigin.x;

        // if no change return nothing (stationary)
        if (horizontalChange == 0 && verticalChange == 0)
            return float.NaN;

        // Getting angle using trigonemtry (T-O-A)
        return Mathf.Atan2(verticalChange, horizontalChange) * Mathf.Rad2Deg;
    }

    public bool JoyStickTouched { get { return joystickTouched; } }
    #endregion

    private bool IsInRange(Vector3 joystickControllerPosition, RectTransform joyStickBorderRectTransform)
    {
        // Get Border Corners
        Vector3[] BackgroundConrners = new Vector3[4];
        joyStickBorderRectTransform.GetWorldCorners(BackgroundConrners);

        // Return true if it does not touch any corner
        return joystickControllerPosition.x >= BackgroundConrners[0].x &&
               joystickControllerPosition.y <= BackgroundConrners[1].y &&
               joystickControllerPosition.x <= BackgroundConrners[2].x &&
               joystickControllerPosition.y >= BackgroundConrners[3].y;
    }
}
