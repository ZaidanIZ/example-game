using UnityEngine;

public class CameraLook : MonoBehaviour
{
    private Transform _cameraRotate;
    private Transform _parent;

    private void Start()
    {
        _parent = GetComponentInParent<Transform>();
        _cameraRotate = Camera.main.transform; 
    
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(53, -_parent.rotation.y, 0);
    }
}
