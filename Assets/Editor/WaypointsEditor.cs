using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Waypoints))]
public class WaypointsEditor : Editor
{
    Waypoints _target;
    float _distanceToNext = 1;
    public int _selectedPoint=0;
    int _x;
    int _y;
    private void OnEnable()
    {
        _target = (Waypoints)target;
        if (_target._points.Count == 0)
        {
            _target._points.Add(_target.transform.position);
            _selectedPoint = _target._points.Count-1;
            ActualiceVP();
        }
    }
    private void OnSceneGUI()
    {
        PositionDrawerAndLines();
        Handles.BeginGUI();

        SceneDraws();

        Handles.EndGUI();

    }
    public void PositionDrawerAndLines()
    {
        if (_target._points == null)
            return;

        for (int count=0;count< _target._points.Count; count++)
        {
            _target._points[count] = Handles.PositionHandle(_target._points[count], _target.transform.rotation);
            if(count< _target._points.Count - 1)
            {
                Handles.color = _target._linesColor;
                Handles.DrawLine(_target._points[count], _target._points[count+1]);
            }
            if (count == _selectedPoint)
            {
                Handles.color = _target._selectedPointColor;
            }
            else
            {
            Handles.color = _target._pointsColor;
            }
            Handles.DrawSolidDisc(_target._points[count],Camera.current.transform.position,
                0.01f*Vector3.Distance(_target._points[count], Camera.current.transform.position));
        }
    }
    public void SceneDraws()
    {
        if (!_target._extremoInferior)
        {
            _x = 0;
        }
        else
        {
            _x=(int)EditorWindow.GetWindow<SceneView>().camera.pixelRect.width-_target._xSize;
        }
        if (!_target._extremoDerecho)
        {
            _y = 0;
        }
        else
        {
            _y = (int)EditorWindow.GetWindow<SceneView>().camera.pixelRect.height - _target._ySize;
        }
        GUILayout.BeginArea(new Rect(_x, _y, _target._xSize, _target._ySize));
        GUI.color = Color.white;
        GUI.Box(new Rect(_x, _y, _target._xSize, _target._ySize), GUIContent.none);
        EditorGUILayout.BeginVertical();
    

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUI.color = Color.red;
        GUI.Box(new Rect(_x, _y, _target._xSize, 20), GUIContent.none);
        GUILayout.Label("Path Creator");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        CratePoints(_distanceToNext);
        GUILayout.FlexibleSpace();
        GUILayout.FlexibleSpace();
        SelectPoints();
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();
    }


   
    public void CratePoints(float _distancetonext)
    {
        _target._wayPoints._points = _target._points;
        GUILayout.FlexibleSpace();
        _distanceToNext = EditorGUILayout.FloatField("Distance to Next Point", _distanceToNext);
        GUILayout.FlexibleSpace();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add forward"))
        {
            if (_target._points.Count == 0)
            {
                _target._points.Add(_target.transform.position);
            }
            else
            {
            _target._points.Add(new Vector3(_target._points[_selectedPoint].x,
                _target._points[_selectedPoint].y,
                _target._points[_selectedPoint].z+_distancetonext));
            }
            FocusOnTheNextPoint();
        }
        if (GUILayout.Button("Add Up"))
        {
            if (_target._points.Count == 0)
            {
                _target._points.Add(_target.transform.position);
            }
            else
            {
                _target._points.Add(new Vector3(_target._points[_selectedPoint].x,
                    _target._points[_selectedPoint].y + _distancetonext,
                    _target._points[_selectedPoint].z));
            }
            FocusOnTheNextPoint();
        }
        if (GUILayout.Button("Add Left"))
        {
            if (_target._points.Count == 0)
            {
                _target._points.Add(_target.transform.position);
            }
            else
            {
                _target._points.Add(new Vector3(_target._points[_selectedPoint].x - _distancetonext,
                    _target._points[_selectedPoint].y,
                    _target._points[_selectedPoint].z));
            }
            FocusOnTheNextPoint();
        }
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Right"))
        {
            if (_target._points.Count == 0)
            {
                _target._points.Add(_target.transform.position);
            }
            else
            {
                _target._points.Add(new Vector3(_target._points[_selectedPoint].x + _distancetonext,
                    _target._points[_selectedPoint].y,
                    _target._points[_selectedPoint].z));
            }
            FocusOnTheNextPoint();
        }
        if (GUILayout.Button("Add Down"))
        {
            if (_target._points.Count == 0)
            {
                _target._points.Add(_target.transform.position);
            }
            else
            {
                _target._points.Add(new Vector3(_target._points[_selectedPoint].x,
                    _target._points[_selectedPoint].y - _distancetonext,
                    _target._points[_selectedPoint].z));
            }
            FocusOnTheNextPoint();
        }
        if (GUILayout.Button("Add backward"))
        {
            if (_target._points.Count == 0)
            {
                _target._points.Add(_target.transform.position);
            }
            else
            {
                _target._points.Add(new Vector3(_target._points[_selectedPoint].x,
                    _target._points[_selectedPoint].y,
                    _target._points[_selectedPoint].z - _distancetonext));

                FocusOnTheNextPoint();
            }
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

    }
    public void FocusOnTheNextPoint()
    {
        if (_selectedPoint < _target._points.Count-1)
        {
            _selectedPoint = _target._points.Count-1;
        }
        ActualiceVP();
    }
    public void SelectPoints()
    {
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Go to the first Point"))
        {
            if (_target._points.Count == 0)
            {
                Debug.LogError("There are no points");
                return;
            }
            _selectedPoint = 0;
            ActualiceVP();
        }
        if (GUILayout.Button("Previous Point"))
        {
            if (_target._points.Count == 0)
            {
                Debug.LogError("There are no points");
                return;
            }
            if (_selectedPoint > 0)
            {
            _selectedPoint--;
            }
            else
            {
                Debug.LogError("This is the first point");
            }
            ActualiceVP();
        }
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Next Point"))
        {
            if (_target._points.Count == 0)
            {
                Debug.LogError("There are no points");
                return;
            }
            if (_selectedPoint < _target._points.Count - 1)
            {
                _selectedPoint++;
            }
            else
            {
                Debug.LogError("This is the last point");
            }
            ActualiceVP();
        }
        if (GUILayout.Button("Go to the last Point"))
        {
            if (_target._points.Count == 0)
            {
                Debug.LogError("There are no points");
                return;
            }
            _selectedPoint = _target._points.Count;
            ActualiceVP();
        }
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Delete Point"))
        {
            if (_target._points.Count == 0)
            {
                return;
            }
            _target._points.RemoveAt(_selectedPoint);
            if (_selectedPoint > _target._points.Count - 1)
            {
                _selectedPoint = _target._points.Count -1;
            }
            ActualiceVP();
        }
            GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.FlexibleSpace();
    }
    public void ActualiceVP()
    {
        if (_target._points.Count == 0)
        {
            return;
        }
        SceneView.lastActiveSceneView.pivot = _target._points[_selectedPoint];
        SceneView.lastActiveSceneView.Repaint();
    }
}
