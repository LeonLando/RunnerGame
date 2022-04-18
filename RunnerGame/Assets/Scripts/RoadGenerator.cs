using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _roadPref;
    private List<GameObject> _roads = new List<GameObject>();
    public float MaxSpeed = 10;
    public float Speed = 0;
    public int MaxRoadCount = 5;

    void Start()
    {
        ResetLevel();
        //StartLevel();
    }

    void Update()
    {
        if (Speed == 0)
        {

        }
        else
        {
            foreach (var road in _roads)
            {
                road.transform.position -= new Vector3(0,0,Speed * Time.deltaTime); 
            }
        }
        if (_roads[0].transform.position.z < -15)
        {
            Destroy(_roads[0]);
            _roads.RemoveAt(0);
            CreateNextRoad();
        }
    }
    public void StartLevel()
    {
        Speed = MaxSpeed;
        SwipeManager.instance.enabled = true;
    }
    public void CreateNextRoad()
    {
        Vector3 pos = Vector3.zero;
        if (_roads.Count > 0)
        {
            pos = _roads[_roads.Count - 1].transform.position + new Vector3(0,0,15);
        }
        
        GameObject go = Instantiate(_roadPref, pos, Quaternion.identity); 
        go.transform.SetParent(transform);
        _roads.Add(go);

    }

    public void ResetLevel()
    { 
    Speed = 0;
        while (_roads.Count > 0)
        {
            Destroy(_roads[0]);
            _roads.RemoveAt(0); 
        }
        for (int i = 0; i < MaxRoadCount; i++)
        {
            CreateNextRoad();
        }
        SwipeManager.instance.enabled = false;

    }
}
