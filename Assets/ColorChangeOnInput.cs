using UnityEngine;
using Unity.PolySpatial.InputDevices;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using UnityEngine.InputSystem.LowLevel;


public class ColorChangeOnInput : MonoBehaviour
{
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
                if(touch.phase == TouchPhase.Began)
                {
                    SpatialPointerState touchData = EnhancedSpatialPointerSupport.GetPointerState(touch);
                    if(touchData.targetObject != null)
                    {
                        // Now we are pinching/direct poking
                        ChangeObjectColor(touchData.targetObject);
                        break;
                    }
                }
            }
        }
    }

    private void ChangeObjectColor(GameObject obj)
    {
        var renderer = obj.GetComponent<Renderer>();
        if(renderer != null)
        {
            renderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}
