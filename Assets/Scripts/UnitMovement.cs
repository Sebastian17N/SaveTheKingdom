using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField]
    Vector2 MovementArea;

    Camera Camera;
    void Start()
    {
        Camera = FindObjectOfType<Camera>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, MovementArea * 2f);

    }
    void Update()
    {
        //if (Input.GetMouseButton(GetComponent<>)); jak klikn¹æ, ¿eby wtedy siê porusza³a Unit?

        Vector2 targetPosition;
        if (!Input.GetMouseButton(0))
            return;

        targetPosition = (Vector2)Camera.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.x = Mathf.Clamp(targetPosition.x, -MovementArea.x, MovementArea.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -MovementArea.y, MovementArea.y);

        transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
    }
}
