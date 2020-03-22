using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void InteractWith(Transform interactor);
    string GetInteractName();
    string GetInteractInfo();
}
