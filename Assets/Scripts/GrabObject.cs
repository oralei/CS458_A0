using System;
using UnityEngine;
using UnityEngine.XR;

public class GrabObject : MonoBehaviour
{
    InputDevice rightController;
    private Vector3 grabOffset;

    private GameObject grabbedObject = null;
    private GameObject objectInRange = null;

    private bool gripped = false;

    void Start()
    {
        // only right
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()
    {
        // grip button because CommonUsages.gripButton
        rightController.TryGetFeatureValue(CommonUsages.gripButton, out bool gripPressed);

        if (gripPressed && !gripped)
            TestGrab();
        else if (!gripPressed && gripped)
            Release();

        if (grabbedObject != null)
            grabbedObject.transform.position = transform.position + grabOffset;

        gripped = gripPressed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("grabbable"))
        {
            objectInRange = other.gameObject;
            Debug.Log("theres a grabbable object here");
        }
    }

    void TestGrab()
    {
        if (objectInRange == null) 
            return;

        grabbedObject = grabObject;
        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        grabOffset = grabbedObject.transform.position - transform.position;
    }

    void Release()
    {
        if (grabbedObject == null) 
            return;

        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        // takes controller velocity
        rightController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);
        rightController.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out Vector3 angularVelocity);

        // applies controlelr velocity to rb which is the object
        rb.linearVelocity = velocity;
        rb.angularVelocity = angularVelocity;

        grabbedObject = null;
    }
}