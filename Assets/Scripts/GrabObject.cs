using System;
using UnityEngine;
using UnityEngine.XR;

public class GrabObject : MonoBehaviour
{
    InputDevice rightController;
    private Vector3 grabOffset;
    private GameObject grabbedObject = null;

    private bool gripLastFrame = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        // B button
        rightController.TryGetFeatureValue(CommonUsages.gripButton, out bool gripPressed);

        if (gripPressed && !gripLastFrame)
            TryGrab();
        else if (!gripPressed && gripLastFrame)
            Release();

        if (grabbedObject != null)
            grabbedObject.transform.position = transform.position + grabOffset;

        gripLastFrame = gripPressed;
    }

    void TryGrab()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("grabbable"))
            {
                grabbedObject = hit.gameObject;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabOffset = grabbedObject.transform.position - transform.position;
                Debug.Log("Grabbed: " + grabbedObject.name);
                break;
            }
        }
    }

    void Release()
    {
        if (grabbedObject == null) return;

        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        // XR velocity
        rightController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);
        rightController.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out Vector3 angularVelocity);

        rb.linearVelocity = velocity;
        rb.angularVelocity = angularVelocity;

        Debug.Log("Released with velocity: " + velocity);
        grabbedObject = null;
    }
}
