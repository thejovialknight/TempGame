using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableHelper
{
    void OnInteract(Transform interactor);
    void OnEnter();
}
