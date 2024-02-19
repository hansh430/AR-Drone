using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineParts : MonoBehaviour
{
    [SerializeField] private GameObject turbineGO;
    private bool isActive=true;

    public void EnableDisableObject()
    {
        turbineGO.SetActive(!isActive);
        isActive = !isActive;
    }
}
