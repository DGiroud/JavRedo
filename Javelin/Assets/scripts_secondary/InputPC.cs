using UnityEngine;

[RequireComponent(typeof(Raycaster))]
public class InputPC : MonoBehaviour
{
    [SerializeField] private Camera m_camera;

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
    /// check for mouse click, and call select if clicking on an interactable object
    /// </summary>
    void Update()
    {
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
        GameObject hitObject = m_raycaster.Raycast(ray);

        // click
        if (Input.GetMouseButtonDown(0))
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
        else if (Input.GetMouseButtonUp(0))
        {
            // if an interactable is being held
            if (m_heldInteractable)
                m_heldInteractable.OnDeselect(); // call deselect

            m_heldInteractable = null;
        }
    }
}
