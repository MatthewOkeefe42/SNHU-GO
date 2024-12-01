using UnityEngine;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;

public class PlayerCoordinateDebugger : MonoBehaviour
{
    ILocationProvider _locationProvider;
    AbstractMap _map;
    Vector3 _targetPosition;
    bool _isInitialized;

    void Start()
    {
        _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
        _map = LocationProviderFactory.Instance.mapManager;
        _map.OnInitialized += () => {
            Debug.Log("Map initialized.");
            _isInitialized = true;
        };
    }


    void Update()
    {
        if (!_isInitialized) return;  // Ensure map is initialized

        var realWorldPosition = _locationProvider.CurrentLocation.LatitudeLongitude;
        Debug.Log($"Real-world Lat/Lon: {realWorldPosition}");

        // Convert latitude and longitude to Unity world position
        _targetPosition = _map.GeoToWorldPosition(realWorldPosition);
        Debug.Log($"Converted World Position: {_targetPosition}");
    }

}
