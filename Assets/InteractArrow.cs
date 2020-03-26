using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractArrow : MonoBehaviour, IInteractableHelper
{
    public SpriteRenderer arrowSpriteRenderer;

    //public GameObject arrowTransform;

    void Awake()
    {
        //spriteRenderer = arrowTransform.GetComponent<SpriteRenderer>();
    }

    public void OnEnter()
    {
        arrowSpriteRenderer.enabled = true;
    }

    public void OnInteract(Transform interactor)
    {

    }

    void Update()
    {
        arrowSpriteRenderer.enabled = false;
    }
}
