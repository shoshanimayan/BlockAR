using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

using System;

public class managerAR : MonoBehaviour
{
    public Camera arCamera;
    GameObject ObjectToPlace;
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject PlaceIndicator;
    private ARSessionOrigin aROrigin;
    private ARRaycastManager m_RaycastManager;
    private Pose placementPose;
    public Text t;
    private bool placementIsValid = false;
    GameObject lookedAt;
    // Start is called before the first frame update
    void Start()
    {
        ObjectToPlace = obj1;
        aROrigin = GetComponent<ARSessionOrigin>();
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lookedAt == null) { t.text = "no object found"; }
        else { t.text = lookedAt.name; }
        UpdatePlacementPose();
        UpdatePlacementIndicator();
     
    }

    public void color1() { ObjectToPlace = obj1; }
    public void color2() { ObjectToPlace = obj2; }
    public void color3() { ObjectToPlace = obj3; }

    public void Place() {

        if (placementIsValid) {

            if (lookedAt == null)
            {
                Instantiate(ObjectToPlace, placementPose.position, placementPose.rotation);
            }
            else if (lookedAt.name == "indicator") { Instantiate(ObjectToPlace, placementPose.position, placementPose.rotation); }
            else
            {

                Vector3 tempPos = new Vector3(placementPose.position.x, (placementPose.position.y) + 0.2f, placementPose.position.z);
                Instantiate(ObjectToPlace, tempPos, placementPose.rotation);

            }


    }


    }

    public void delete()
    {

        if (placementIsValid && lookedAt != null) {
            if (lookedAt.name != "indicator")
            {
                Destroy(lookedAt);
                lookedAt = null;
            }
        }


    }



        private void UpdatePlacementPose()
    {
      
       
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        m_RaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hitObject;
        if (Physics.Raycast(ray, out hitObject))
        {
            GameObject placementObject = hitObject.transform.gameObject;
            if (placementObject != null)
            {
                lookedAt = placementObject;
            }
            else { lookedAt = null; }
        }
        else { lookedAt = null; }
        placementIsValid = hits.Count > 0;
        if (placementIsValid)
        {
            placementPose = hits[0].pose;
            var camForward = Camera.current.transform.forward;
            Vector3 cameraBearing = new Vector3(camForward.x, 0, camForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);

        }


    }





    private void UpdatePlacementIndicator()
    {
        if (placementIsValid)
        {
            PlaceIndicator.SetActive(true);
            PlaceIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else {
            PlaceIndicator.SetActive(false);

        }
    }
}
