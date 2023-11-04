using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGround;
    public Transform gunTip;
    public Transform camera;
    public float maxDistance = 100f;
    public SpringJoint joint;
    public Transform player;
    public PlayerMovement pm;

    void Start()
    {
        StopGrapple();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        } else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        pm.grappling = true;

        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance)) 
        { 
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
    }

    void DrawRope()
    {
        if (!joint) return;

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    void StopGrapple()
    {
        pm.grappling = false;

        lr.positionCount = 0;
        Destroy(joint);
    }
}
