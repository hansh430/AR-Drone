using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static UnityEngine.EventSystems.PointerEventData;

public class ARPlacement : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private GameObject placementIndicator;
    [SerializeField] private GameObject placementInstruction;
    [SerializeField] private GameObject scanPlaneInstruction;
    [SerializeField] private GameObject inputButtons;
    private GameObject spawnedObject;
    private Pose placementPose;
    private ARRaycastManager raycastManager;
    private bool placementPoseIsValid = false;

    private void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }
    private void Update()
    {
        if (spawnedObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlceObject();
        }
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void ARPlceObject()
    {
        spawnedObject = Instantiate(objectToSpawn, placementPose.position, placementPose.rotation);
        if (inputButtons != null)
        {
            inputButtons.SetActive(true);
        }
    }
    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }
    private void UpdatePlacementIndicator()
    {
        if (spawnedObject == null && placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementInstruction.SetActive(true);
            scanPlaneInstruction.SetActive(false);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
            placementInstruction.SetActive(false);
        }
    }
}
