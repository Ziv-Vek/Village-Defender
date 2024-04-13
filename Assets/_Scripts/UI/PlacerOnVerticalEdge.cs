using UnityEngine;

public class PlacerOnVerticalEdge : MonoBehaviour
{
    [SerializeField] private bool isClipToRight = true;

    void Start()
    {
        if (Camera.main != null)
        {
            transform.position =
                Camera.main!.ScreenToWorldPoint(new Vector3(isClipToRight ? 0 : Screen.width, Screen.height * 0.5f,
                    Camera.main.nearClipPlane));
        }
        else
        {
            throw new System.Exception("Main camera not found");
        }
    }
}