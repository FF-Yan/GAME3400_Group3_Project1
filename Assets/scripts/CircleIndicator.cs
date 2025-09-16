using UnityEngine;

public class CircleIndicator : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject targetObject;

    void LateUpdate()
    {
        Vector3 screenPosition = GetScreenPosition();

        RectTransform rect = (RectTransform)transform;
        
        if (screenPosition.z <= 0)
        {
            screenPosition.x = Screen.width - screenPosition.x;
            screenPosition.y = Screen.height - screenPosition.y;
            screenPosition.z = 1;
        }

        if (!IsOnScreen())
        {
            screenPosition.x = Mathf.Clamp(screenPosition.x, 20f, Screen.width  - 20f);
            screenPosition.y = Mathf.Clamp(screenPosition.y, 20f, Screen.height - 20f);
        }

        rect.anchoredPosition = new Vector2(
            screenPosition.x - Screen.width  * 0.5f,
            screenPosition.y - Screen.height * 0.5f);
    }


    // get the screen position of the target object from the camera
    public Vector3 GetScreenPosition()
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetObject.transform.position);
        return screenPosition;
    }

    // To check if the target object is on screen by checking if it is less than 0 
    // or greater than the screen limit or behind the screen
    public bool IsOnScreen()
    {
        Vector3 screenPosition = GetScreenPosition();

        if (screenPosition.x < 0 || screenPosition.x > Screen.width
        || screenPosition.y < 0 || screenPosition.y > Screen.height
        || screenPosition.z <= 0)
        {
            return false;
        }

        return true;
    }
}
