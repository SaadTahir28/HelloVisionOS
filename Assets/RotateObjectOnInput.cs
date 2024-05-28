using UnityEngine;
using Unity.PolySpatial.InputDevices;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using UnityEngine.InputSystem.LowLevel;

public class RotateObjectOnInput : MonoBehaviour
{
    public float rotationSpeed = 0.1f;
    public float followRotationSpeed = 0.1f;
    public GameObject followingObject;

    private GameObject selectedObject;
    private Vector3 lastInteractionPosition;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            foreach (var touch in Touch.activeTouches)
            {
                SpatialPointerState touchData = EnhancedSpatialPointerSupport.GetPointerState(touch);
                if (touchData.targetObject != null && touchData.Kind != SpatialPointerKind.Touch)
                {
                    // Now we are pinching
                    if (touch.phase == TouchPhase.Began)
                    {
                        selectedObject = touchData.targetObject;
                        lastInteractionPosition = touchData.interactionPosition;
                    }
                    else if (touch.phase == TouchPhase.Moved && selectedObject != null)
                    {
                        var deltaPosition = touchData.interactionPosition - lastInteractionPosition;
                        float rotationAmount = deltaPosition.x * rotationSpeed; // Adjust the multiplier as needed
                        selectedObject.transform.Rotate(Vector3.forward, rotationAmount);
                        lastInteractionPosition = touchData.interactionPosition;
                    }
                }
            }
        }

        if (Touch.activeTouches.Count == 0)
        {
            selectedObject = null;
        }

        FollowObject();
    }

    private void FollowObject()
    {
        if (selectedObject == null)
            return;
        var lastRotation = followingObject.transform.rotation;
        lastRotation.y = selectedObject.transform.rotation.z * followRotationSpeed;
        followingObject.transform.rotation = lastRotation;
    }
}


