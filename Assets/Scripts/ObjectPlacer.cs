using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private Transform _objectPlace;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _container;

    private ARRaycastManager _arRaycastManager;
    private GameObject _installedObject;
    private List<ARRaycastHit> _hits;
    private Coroutine _runCoroutine;

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _hits = new List<ARRaycastHit>();
    }

    private IEnumerator Run()
    {
        bool isWorking = true;

        while (isWorking)
        {
            UpdatePlacementPose();

            if(Input.touchCount == 2)
            {
                SetObject();

                isWorking = false;
                _runCoroutine = null;
            }

            yield return null;
        }
    }

    private void UpdatePlacementPose()
    {
        Vector2 screenCenter = _camera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        var ray = _camera.ScreenPointToRay(screenCenter);

        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            SetObjectPosition(raycastHit.point);
        }
        else if (_arRaycastManager.Raycast(screenCenter, _hits, TrackableType.PlaneWithinPolygon))
        {
            SetObjectPosition(_hits[0].pose.position);
        }
    }

    private void SetObjectPosition(Vector3 position)
    {
        _objectPlace.position = position;
        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRotation = new Vector3(cameraForward.x, 0, cameraForward.z);
        _objectPlace.rotation = Quaternion.Euler(cameraRotation);
    }

    private void SetObject()
    {
        _installedObject.GetComponent<Collider>().enabled = true;
        _installedObject.transform.parent = _container.transform;
        _installedObject = null;
    }

    public void SetInstalledObject(ItemData itemData, out GameObject installedObject)
    {
        if(_installedObject != null)
        {
            Destroy(_installedObject);
        }

        ItemData item = itemData;
        _installedObject = Instantiate(item.Prefab, _objectPlace);
        installedObject = _installedObject;
        _installedObject.GetComponent<Collider>().enabled = false;

        if(_runCoroutine != null)
        {
            StopCoroutine(_runCoroutine);
            _runCoroutine = null;
        }

        _runCoroutine = StartCoroutine(Run());
    }
}