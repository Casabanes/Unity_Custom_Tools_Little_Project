using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
public class PathsCreatorWindow : EditorWindow
{
    public int _pointsLength;
    public bool _pointsBool;
    Vector3 _vectorZero = new Vector3(0, 0, 0);
    Quaternion _rZero = new Quaternion(0, 0, 0, 0);
    GameObject _gameObject;
    string _gameObjectName = "Path";
    bool _savePathButton;
    bool _savePath;
    WaypointsScriptable _path;
    Waypoints _wayPoints;
    string _assetPathAndName= "";
    string _assetName = "New Path";
    string _assetPath= "Assets/Paths/";
    bool _isAValidPath;
    //elegir esquinas para la ventana de ongui desde aquí xd

    [MenuItem("Window/PathsCreator")]
    public static void OpenWindow()
    {
        GetWindow<PathsCreatorWindow>();
    }
    private void OnEnable()
    {
    }
	public void OnGUI()
	{
		PathObjectCreator();
        _wayPoints._pointsColor = EditorGUILayout.ColorField("Color of the points:", _wayPoints._pointsColor);
        _wayPoints._selectedPointColor = EditorGUILayout.ColorField("Color selected point:", _wayPoints._selectedPointColor);

        _wayPoints._linesColor = EditorGUILayout.ColorField("Color of the lines:", _wayPoints._linesColor);

        if (_pointsLength < 0)
		{
			_pointsLength = 0;
		}
		_pointsBool = EditorGUILayout.Foldout(_pointsBool, "Points");
		if (_pointsBool)
		{

            _pointsLength = EditorGUILayout.IntField("Cuantity of Points", _wayPoints._points.Count);
			for (int count = 0; count < _pointsLength; count++)
			{ 
				if (_wayPoints._points.Count < _pointsLength)
					{
                    _wayPoints._points.Add(new Vector3(0,0,0));

                    }
                if (_wayPoints._points.Count > _pointsLength)
                {
                    _wayPoints._points.RemoveAt(_wayPoints._points.Count-1);

                }
                if (_wayPoints._points[count] == null)
			{
                    _wayPoints._points[count] = (new Vector3(0, 0, 0));
			}
                _wayPoints._points[count]=EditorGUILayout.Vector3Field("Point "+(count+1)+":", _wayPoints._points[count]);


            _path._points= _wayPoints._points;
            _wayPoints._wayPoints._points = _wayPoints._points;


            }
            }
        _wayPoints._extremoDerecho = EditorGUILayout.Toggle("Window in the rightside",_wayPoints._extremoDerecho);
        _wayPoints._extremoInferior = EditorGUILayout.Toggle("Window in the bottom", _wayPoints._extremoInferior);
        _wayPoints._xSize = EditorGUILayout.IntField("X size:", _wayPoints._xSize);
        _wayPoints._ySize = EditorGUILayout.IntField("Y size:", _wayPoints._ySize);
        GUILayout.Space(50);

        _assetName = EditorGUILayout.TextField("Name of the path:", _assetName);

        _savePathButton = GUILayout.Button("Save Path");
        if(_savePathButton)
        {
            _savePath = true;
            AssetDatabase.RenameAsset(_assetPathAndName, _assetName);
            _assetPathAndName = "Assets/" + _assetName + ".asset";
            _path.name = _assetName;
            AssetDatabase.Refresh();
        }
        if (_savePath)
        {
            EditorGUILayout.HelpBox("The path are sucefully saved",MessageType.Info); 
        }
	}
    public void PathObjectCreator()
    {
        if (_gameObject == null)
        {
            _gameObject = new GameObject();
            _gameObject.name = _gameObjectName;
            _gameObject.transform.position = _vectorZero;
            _gameObject.transform.rotation = _rZero;
            _gameObject.AddComponent<Waypoints>();
            _wayPoints=_gameObject.GetComponent<Waypoints>();
            ScriptablePathCreator();
        }
      
    }
    public void ScriptablePathCreator()
    {
        if (_path == null)
        {
            ScriptableObject _pathFile = ScriptableObject.CreateInstance<WaypointsScriptable>();
            _isAValidPath=AssetDatabase.IsValidFolder("Assets/Paths");
            if (_isAValidPath)
            {
                _assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(_assetPath + _assetName + ".asset");
            }
            else
            {
                AssetDatabase.CreateFolder("Assets", "Paths");
                _assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(_assetPath + _assetName + ".asset");

            }
            AssetDatabase.CreateAsset(_pathFile, _assetPathAndName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            _wayPoints._wayPoints = AssetDatabase.LoadAssetAtPath<WaypointsScriptable>(_assetPathAndName);
            _path = _wayPoints._wayPoints;
        }
        if (_gameObject != null && _path!=null)
        {
            Selection.activeObject = _gameObject;
        }
    }


   
    public void OnDestroy()
    {
        if (_gameObject != null)
        {
            DestroyImmediate(_gameObject);
            if(!_savePath)
            {
            AssetDatabase.DeleteAsset(_assetPathAndName);
            }
        }
    }
}
