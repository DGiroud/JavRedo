using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Raycaster))]
public class InputOculus : MonoBehaviour
{
    [SerializeField] private Transform m_rightHandAnchor;

    private Raycaster m_raycaster;
    private Interactable m_heldInteractable;

    /// <summary>
    /// cache reference to raycaster component
    /// </summary>
    void Start()
    {
        m_raycaster = GetComponent<Raycaster>();
    }

    /// <summary>
    /// check for oculus index trigger, and call select if clicking on an interactable object
    /// </summary>
    void Update()
    {
        Vector3 origin = m_rightHandAnchor.position;
        Vector3 direction = m_rightHandAnchor.forward;
        GameObject hitObject = m_raycaster.Raycast(origin, direction);
        
        // index trigger pressed
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            // if an object was hit
            if (hitObject)
            {
                Interactable interactable = hitObject.GetComponent<Interactable>();

                // if it's interactable
                if (interactable)
                {
                    interactable.OnSelect(); // call select
                    m_heldInteractable = interactable;
                }
            }
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)) // index trigger lifted
        {
            // if an interactable is being held
            if (m_heldInteractable)
                m_heldInteractable.OnDeselect(); // call deselect

            m_heldInteractable = null;
        }
    }
}
