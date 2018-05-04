using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{
    [SerializeField]
    private EventSystem eventSystem;
    [SerializeField]
    private GameObject selectedObject;

    private bool buttonSelected;

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
