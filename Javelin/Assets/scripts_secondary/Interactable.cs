using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public Vector3 m_current;
    private Vector3 m_past;
    public Text txt;
    public Text txt2;

    private void Update()
    {
    }

    public void Pressed()
    {
        if (m_past == null)
        {
            m_past = m_current;
        }
        Vector3 previous = Camera.main.WorldToScreenPoint(m_past);
        Vector3 current = Camera.main.WorldToScreenPoint(m_current);
        MoveInteractable(previous.x - current.x);
        m_past = m_current;
    }

    private void MoveInteractable(float amount)
    {
        if (transform.position.x < 0.12 && transform.position.x > -0.5)
        {
            transform.Translate(new Vector3(amount, 0, 0));
            txt.text = amount.ToString();
        }
    }
}
