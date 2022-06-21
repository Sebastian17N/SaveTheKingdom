using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cameraMain;
    [SerializeField] private float minCamSize;
    [SerializeField] private float maxCamSize;
    private Vector3 frameOrigin;

    public SpriteRenderer mapRenderer;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x /2;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x /2;
        
        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y /2;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y /2;
    }

    void Update()
    {
        PanCamera();
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void PanCamera()
    {
        var camerOnMousePosition = 
            cameraMain.ScreenToWorldPoint(Input.mousePosition);
         
        if (Input.GetMouseButtonDown(0))
            frameOrigin = camerOnMousePosition;

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPosition = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPosition = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPosition - touchOnePrevPosition).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 0.01f);

        }else if (Input.GetMouseButton(0))
        {
            Vector3 direction = frameOrigin - camerOnMousePosition;            

            cameraMain.transform.position = 
               ClampCamera(cameraMain.transform.position + direction);
        }
    }

    private void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp
            (Camera.main.orthographicSize - increment, minCamSize, maxCamSize);

        //cameraMain.transform.position = ClampCamera(cameraMain.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cameraMain.orthographicSize;
        float camWidth = cameraMain.orthographicSize * cameraMain.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }

}
