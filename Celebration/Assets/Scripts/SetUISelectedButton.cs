using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SetUISelectedButton : MonoBehaviour
{
    [SerializeField] GameObject buttonToSelect;
    private void OnEnable()
    {
        StartCoroutine(SetButton());
    }

    IEnumerator SetButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(buttonToSelect);


    }
}
