using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    public virtual void OnHover() {}
    public virtual void OnUnhover() {}
    public virtual void OnSelect() {}
    public virtual void OnDeselect(){}
}