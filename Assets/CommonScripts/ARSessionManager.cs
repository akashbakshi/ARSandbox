using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARSessionManager : MonoBehaviour
{
    public GameObject indicator;
    protected ARSessionOrigin sessionOrigin;
    private bool canPlace;
    protected bool isTrackingIndicator = true;
    protected Pose pose;
    private List<ARRaycastHit> hits;

    // Start is called before the first frame update
    void Start()
    {
        sessionOrigin = GetComponent<ARSessionOrigin>();
        hits = new List<ARRaycastHit>();
    }

    // Update is called once per frame
    void Update()
    {

      
        if (isTrackingIndicator)
        {
            LookForSurface();
            UpdateIndicatorPosition();
        }

        indicator.SetActive(isTrackingIndicator && canPlace);


    }

    void LookForSurface()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        sessionOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, TrackableType.Planes);

        canPlace = hits.Count > 0;

        if (canPlace)
        {
            pose = hits[0].pose;
        }

    }

    void UpdateIndicatorPosition()
    {
        if(canPlace && indicator != null)
            indicator.transform.SetPositionAndRotation(pose.position, pose.rotation);
    }
}
