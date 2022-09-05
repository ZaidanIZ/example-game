using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpObjct : MonoBehaviour
{
    

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;

    private ObjectGrabbable objectGrabbable;

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (objectGrabbable == null){
                float pickUpDistance = 2f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask)) {
                    Debug.Log(raycastHit.transform);
                    if (raycastHit.transform.TryGetComponent(out ObjectGrabbable objectGrabbable)) {
                    objectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
            }else{
                objectGrabbable.drop();
                objectGrabbable = null;
            }
        }
    }
}
