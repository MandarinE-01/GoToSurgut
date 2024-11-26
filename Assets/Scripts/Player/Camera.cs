using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform PlayerTarget;
    [SerializeField] private float _mouseSensivity = 10f;

    [Header("Boarder")]
    [SerializeField] private sbyte _boarderUp = 70;
    [SerializeField] private sbyte _boarderBottom = -70;

    private float vertcalRotation;
    private float horizontalRotation;
    private void LateUpdate()
    {
        if(PlayerTarget == null)
        {
            return;
        }

        transform.position = PlayerTarget.position;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        vertcalRotation -= mouseY * _mouseSensivity;
        vertcalRotation = Mathf.Clamp(vertcalRotation, _boarderBottom, _boarderUp);

        horizontalRotation += mouseX * _mouseSensivity;

        transform.rotation = Quaternion.Euler(vertcalRotation, horizontalRotation, 0);
    }
}
