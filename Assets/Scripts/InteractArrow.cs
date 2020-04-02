using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractArrow : MonoBehaviour, IInteractableHelper
{
    public SpriteRenderer arrowSpriteRenderer;
    public Animator arrowAnimator;
    public AudioClip enterSound;
    bool isEnabled;
    bool isEntered = false;

    public void OnEnter()
    {
        isEnabled = true;
        if(!isEntered) {
            isEntered = true;
            AudioSource.PlayClipAtPoint(enterSound, transform.position);
            arrowAnimator.SetTrigger("StartPoint");
        }
    }

    public void OnInteract(Transform interactor)
    {

    }

    void LateUpdate()
    {
        if(isEnabled) {
            arrowSpriteRenderer.enabled = true;
            isEnabled = false;
        }
        else {
            arrowSpriteRenderer.enabled = false;
            isEntered = false;
        }
    }
}
