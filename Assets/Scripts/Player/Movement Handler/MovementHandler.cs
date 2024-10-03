using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    public Vector2 sensitivity = new Vector2 {
        x = 2f,
        y = 2f
    };
    private Vector2 rot;
    public Camera playerCamera;
    public void Update()
    {
        Vector2 mouse = new Vector2
        {
            x = Input.GetAxis("Mouse X"),
            y = Input.GetAxis("Mouse Y")
        };

            rot.x -= mouse.y * sensitivity.y;
            rot.y -= mouse.x * sensitivity.x;

            rot.x = Mathf.Clamp(rot.x, -90f, 90f);
            this.transform.eulerAngles = new Vector3(0, rot.y * -1, 0);

            playerCamera.transform.localEulerAngles = new Vector3(rot.x, 0, 0);
    }
}
