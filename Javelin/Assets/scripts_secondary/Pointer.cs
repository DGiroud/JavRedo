﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pointer : MonoBehaviour
{
    public float m_Distance = 10.0f;
    public LineRenderer lineRenderer = null;
    public LayerMask m_AllObjects = 0;
    public LayerMask m_InteractibleObjects = 0;
    [SerializeField] private RoomManger RM;

    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;

    [SerializeField] private Transform m_rightHandAnchor;
    private Transform OriginController = null;
    private GameObject m_CurrentObject = null;
    private GameObject m_lastHitObject;
    private bool m_grabbingSlider = false;
    private Interactable m_slider;

    

    private void Start()
    {
        SetLineColor();
    }

    private void Awake()
    {
        VRInteraction.onControllerSource += UpdateOrigin;
        VRInteraction.onTouchPadDown += ProcessTouchPadDown;
    }

    private void Update()
    {
        Vector3 hitPoint = UpdateLine();

        if (!m_grabbingSlider)
            m_CurrentObject = UpdatePointerStatus();

        if (OnPointerUpdate != null)
            OnPointerUpdate(hitPoint, m_CurrentObject);

        ProcessTouchPadDown();
    }
    

    private void OnDestroy()
    {
        VRInteraction.onControllerSource -= UpdateOrigin;
        VRInteraction.onTouchPadDown -= ProcessTouchPadDown;
    }

    private void UpdateOrigin(OVRInput.Controller controller, GameObject gameObject) {
        OriginController = gameObject.transform;

        if (controller == OVRInput.Controller.Touchpad)
        {
            lineRenderer.enabled = false;
        }
        else {
            lineRenderer.enabled = true;
        }
    }

    private void ProcessTouchPadDown() {
        if (!m_CurrentObject)
            return;

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Interaction interaction = m_CurrentObject.GetComponent<Interaction>();

            if (interaction)
                interaction.interactor.OnSelect();

            if (!m_grabbingSlider)
            {
                m_slider = m_CurrentObject.GetComponent<Interactable>();
                m_grabbingSlider = true;
            }
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)) {
            
            //m_slider.Pressed(lineRenderer);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (m_grabbingSlider)
            {
                m_grabbingSlider = false;
            }
        }

       // Press press = m_CurrentObject.GetComponent<Press>();
        //press.PressedButton();
    }

    private GameObject UpdatePointerStatus()
    {
        RaycastHit hit = CreateRayCast(m_InteractibleObjects);
        Material mat = hit.transform.GetComponent<Renderer>().material;

        if (hit.collider)
        {
            mat.SetFloat("_ASEOutlineWidth", 0.3f);
        }

        m_lastHitObject = hit.collider.gameObject;
        lineRenderer.SetPosition(1, hit.point);

        return hit.collider.gameObject;
    }

    private Vector3 UpdateLine() {
        RaycastHit hit = CreateRayCast(m_AllObjects);
        Vector3 endPos = OriginController.position + (OriginController.forward * m_Distance);

        if (hit.collider != null)
            endPos = hit.point;

        if (m_lastHitObject != null && hit.collider.gameObject != m_lastHitObject)
        {
            m_lastHitObject.GetComponent<Renderer>().material.SetFloat("_ASEOutlineWidth", 0.0f);
        }

        lineRenderer.SetPosition(0, OriginController.position);
        lineRenderer.SetPosition(1, endPos);

        return endPos;
    }

    private RaycastHit CreateRayCast(int layer) {
        RaycastHit hit;
        Ray ray = new Ray(OriginController.position, OriginController.forward);
        Physics.Raycast(ray, out hit, m_Distance, layer);     
        return hit;
    }

    private void SetLineColor() {
        if (lineRenderer != null)
            return;

        Color endCol = Color.white;
        endCol.a = 0;
        lineRenderer.endColor = endCol;
    }

}
