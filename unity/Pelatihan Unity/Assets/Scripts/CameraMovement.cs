using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform targetObject; // Objek yang akan diikuti
    public Vector3 cameraOffset; // Jarak antara kamera dan objek
    public float sensitivity = 100.0f; // Sensitivitas mouse
    public float clampAngle = 80.0f; // Batas sudut kamera

    private float verticalRotation = 0.0f; // Rotasi vertikal
    private float horizontalRotation = 0.0f; // Rotasi horizontal

    void Start()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        verticalRotation = rotation.x;
        horizontalRotation = rotation.y;
    }

    void Update()
    {
        // Mengatur posisi kamera sesuai dengan posisi objek ditambah offset
        transform.position = targetObject.position + cameraOffset;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        horizontalRotation += mouseX * sensitivity * Time.deltaTime;
        verticalRotation += mouseY * sensitivity * Time.deltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0.0f);
        transform.rotation = localRotation;
    }
}