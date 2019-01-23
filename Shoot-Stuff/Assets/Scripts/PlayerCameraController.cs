using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private float lookSensitivity;
    [SerializeField]
    private float smoothing;

    private GameObject player;
    private Vector2 smoothedVelocity;
    private Vector2 currentLookingPos;

    void Start()
    {
        player = transform.parent.gameObject;
    }

    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        var x = Input.GetAxisRaw("Mouse X");
        var y = Input.GetAxisRaw("Mouse Y");

        Vector2 inputValues = new Vector2(x, y);

        // To have different vertical and horizontal smoothing then these values would be different in the next line
        inputValues = Vector2.Scale(inputValues, new Vector2(lookSensitivity * smoothing, lookSensitivity * smoothing));

        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f / smoothing);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f / smoothing);

        currentLookingPos += smoothedVelocity;

        transform.localRotation = Quaternion.AngleAxis(-currentLookingPos.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(currentLookingPos.x, player.transform.up);
    }
}
