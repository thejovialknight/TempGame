using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public int currentIndex = 0;
    public List<Transform> selections = new List<Transform>();

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Right"))
        {
            currentIndex++;
            if(currentIndex > selections.Count - 1)
            {
                currentIndex = 0;
            }
        }

        if (Input.GetButtonDown("Left"))
        {
            currentIndex--;
            if(currentIndex < 0)
            {
                currentIndex = selections.Count - 1;
            }
        }

        if(Input.GetButtonDown("Interact"))
        {
            MessageEventManager.RaiseOnSelect("MAIL_SORT", currentIndex);
        }

        transform.position = selections[currentIndex].position;
    }
}
