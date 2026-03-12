using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.XR;


public class ButtonInput : MonoBehaviour
{
    InputDevice rightController;
    public LineRenderer Line;

    // create ray
    RaycastHit hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        // B button
        rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool rPressed);

        // Always at start of sphere.
        Line.SetPosition(0, transform.position);

        if (rPressed)
        {
            Debug.Log("Button pressed!");
        }

        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit);
        Button button = null;

        if (hit.collider != null)
        {
            Line.SetPosition(1, hit.point); // goes to hit surface
            if (hit.collider.CompareTag("button"))
            {
                button = hit.collider.GetComponent<Button>();
                button.Select();
                if (rPressed)
                {
                    ExecuteEvents.Execute(button.gameObject,
                        new BaseEventData(EventSystem.current),
                        ExecuteEvents.submitHandler);
                }
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        else
        {
            Line.SetPosition(1, transform.position + transform.forward * 20); // default 20 units
        }
    }
}
