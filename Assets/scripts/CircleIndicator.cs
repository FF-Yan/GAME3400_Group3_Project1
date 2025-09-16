using UnityEngine;

public class CircleIndicator : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject targetObject;
    public RectTransform indicator;

    void LateUpdate()
    {
        Vector3 screenPosition = GetScreenPosition();

        float dist = Vector3.Distance(mainCamera.transform.position, targetObject.transform.position);

        // If the object is very close, hide the indicator, otherwise no
        if (IsOnScreen() && dist < 5f)
        {
            indicator.localScale = Vector3.zero;
        }
        else
        {
            indicator.localScale = Vector3.one;
        }
        
        if (!IsOnScreen())
        {
            // to avoid the object is in the back and the circle indicator will not be controlled
            if (screenPosition.z < 0)
            {
                indicator.localScale = Vector3.zero;
            }
            // keep the indicator in the screen
            screenPosition.x = Mathf.Clamp(screenPosition.x, 20f, Screen.width - 20f);
            screenPosition.y = Mathf.Clamp(screenPosition.y, 20f, Screen.height - 20f);
        }

        // place the indicator in the right place by make the center from the origin to the center
        indicator.anchoredPosition = new Vector2(
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