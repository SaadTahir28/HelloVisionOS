using UnityEngine;
using Unity.PolySpatial.InputDevices;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using UnityEngine.InputSystem.LowLevel;

public class MoveObjectOnInput : MonoBehaviour
{
    private GameObject selectedObject;
    private Vector3 lastPosition;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void Update()
    {
        if(Touch.activeTouches.Count > 0)
        {
            foreach (var touch in Touch.activeTouches)
            {
                SpatialPointerState touchData = EnhancedSpatialPointerSupport.GetPointerState(touch);
                if (touchData.targetObject != null && touchData.Kind != SpatialPointerKind.Touch)
                {
                    //Now we are pinching
                    if(touch.phase == TouchPhase.Began)
                    {
                        selectedObject = touchData.targetObject;
                        lastPosition = touchData.interactionPosition;
                    }
                    else if(touch.phase == TouchPhase.Moved && selectedObject != null)
                    {
                        var deltaPosition = touchData.interactionPosition - lastPosition;
                        selectedObject.transform.position += deltaPosition;
                        lastPosition = touchData.interactionPosition;
                    }
                }
            }
        }

        if(Touch.activeTouches.Count == 0)
        {
            selectedObject = null;
        }
    }
}
