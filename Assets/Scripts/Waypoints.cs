using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public WaypointsScriptable _wayPoints;
    public Color32 _pointsColor=Color.red;
    public Color32 _selectedPointColor = Color.blue;

    public Color32 _linesColor = Color.green;
    public Vector3 _rotationV3;

    public List<Vector3> _points=new List<Vector3>();
    public bool _extremoDerecho;
    public bool _extremoInferior;
    public int _xSize=300;
    public int _ySize=200;


}
