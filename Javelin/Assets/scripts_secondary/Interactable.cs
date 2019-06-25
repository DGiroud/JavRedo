using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    public virtual void OnHover() { Debug.Log("Hovering"); }
    public virtual void OnUnhover() { Debug.Log("Unhovering"); }
    public virtual void OnSelect() { Debug.Log("Selecting"); }
    public virtual void OnDeselect() { Debug.Log("Deselecting"); }
}