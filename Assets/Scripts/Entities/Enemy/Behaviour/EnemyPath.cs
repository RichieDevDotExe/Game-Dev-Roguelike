using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    //contains the locations for the enemy path
    [SerializeField]private List<Transform> waypoints = new List<Transform>();

    public List<Transform> Waypoints
    {
        get { return waypoints; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
