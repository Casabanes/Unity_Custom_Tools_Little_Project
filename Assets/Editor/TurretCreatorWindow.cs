using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class TurretCreatorWindow : EditorWindow
{
    [MenuItem("Window/TurretCreatorWindow")]
    public static void OpenWindow()
    {
        GetWindow<TurretCreatorWindow>();
    }
    /*  objetivo: poder hacer torretas 2d y 3d 
     *  guardar los prefabs
     *  poder elegir varios componentes para armar las torretas
     *  guardar las variables principales en scriptable objects
     * 
     */
    bool _2d;
    public void OnGUI()
    {
        _2d = EditorGUILayout.Toggle("2D", _2d);
        if (_2d)
        {
            TwoDimensionsGui();
        }
        else
        {
            ThreeDimensionsGui();
        }
    }
    Sprite _sprite;
    bool _srBool;
    Rigidbody2D _rb2d;
    bool _rb;
    Animator _animator;
    bool _animatorBool;
    bool _box;
    bool _circle;
    bool _capsule;
    bool _triger;
    bool _as;



    List<Object> _extraComponents = new List<Object>();
    int _aux;

    enum _typeOfCollider
    {
       Box,Circle,Capsule
    };
    _typeOfCollider _colliderType=_typeOfCollider.Box;

    public void TwoDimensionsGui()
    {
        _srBool = EditorGUILayout.Toggle("Have Sprite Renderer", _srBool);
        if (_srBool)
        {
            _sprite = EditorGUILayout.ObjectField("Sprite:", _sprite, typeof(Sprite), true) as Sprite;
        }

        _colliderType = (_typeOfCollider)EditorGUILayout.EnumPopup("Type of Collider", _colliderType);
        if (_colliderType == _typeOfCollider.Box)
        {
            _box = true;
            _circle = false;
            _capsule = false;
        }
        if (_colliderType == _typeOfCollider.Circle)
        {
            _box = false;
            _circle = true;
            _capsule = false;
        }
        if (_colliderType == _typeOfCollider.Capsule)
        {
            _box = false;
            _circle = false;
            _capsule = true;
        }
        Commons();


    }
    bool _sphere;
    enum _3dColliderType
    {
        Box=0,Sphere=1,Capsule=2
    };
    _3dColliderType _typeOf3DCollider = _3dColliderType.Box;
    public void ThreeDimensionsGui()
    {



        _typeOf3DCollider = (_3dColliderType)EditorGUILayout.EnumPopup("Type of Collider", _typeOf3DCollider);
        if (_typeOf3DCollider == _3dColliderType.Box)
        {
            _box = true;
            _sphere = false;
            _capsule = false;
        }
        if (_typeOf3DCollider == _3dColliderType.Sphere)
        {
            _box = false;
            _sphere = true;
            _capsule = false;
        }
        if (_typeOf3DCollider == _3dColliderType.Capsule)
        {
            _box = false;
            _sphere = false;
            _capsule = true;
        }
        Commons();

    }
    string _turretName;
    bool _saveTurret;
    string _defaultTurretName = "New Turret";
    string _turrettPath = "Assets/Turrets/";
    string _turretPathAndName="";
    bool _isAValidPath;
    int _damage;
    float _attackSpeed;
    float _range;
    public void Commons()
    {
        _rb = EditorGUILayout.Toggle("Have Rigid Body", _rb);
        _as = EditorGUILayout.Toggle("Have Audio Source", _as);

        _animatorBool = EditorGUILayout.Toggle("Have Animator", _animatorBool);
        
        _triger = EditorGUILayout.Toggle("Is trigger", _triger);
        
          _aux = EditorGUILayout.IntField("Extra Elements:", _aux);
          if (_aux < 0)
          {
              _aux = 0;
          }
          for (int count = 0; count < _aux; count++)
          {
              if (_extraComponents.Count < _aux)
              {
                  _extraComponents.Add(new Object());
              }
              if (_extraComponents.Count > _aux)
              {
                  _extraComponents.RemoveAt(_extraComponents.Count - 1);
              }

              _extraComponents[count] = EditorGUILayout.ObjectField("Extra elements:", _extraComponents[count],
                  typeof(Object), true) as Object;
          }
        _damage = EditorGUILayout.IntField("Turret Damage", _damage);
        _attackSpeed = EditorGUILayout.FloatField("Turret AttackSpeed", _attackSpeed);
        _range = EditorGUILayout.FloatField("Turret Range", _range);
        GUILayout.Space(20);


        Save();
    }
    string _scriptableName = "TurretStats";
    string _scriptablePath = "Assets/Turret Stats/";
    string _scriptablePathAndName = "";
    bool _validTurretStatsPath;

    public void Save()
    {
        _turretName = EditorGUILayout.TextField("Turret Name", _turretName);

        _saveTurret = GUILayout.Button("Save Turret");
        if (_saveTurret)
        {
            _isAValidPath = AssetDatabase.IsValidFolder("Assets/Turrets");
            if (_isAValidPath)
            {
                _turretPathAndName = AssetDatabase.GenerateUniqueAssetPath(_turrettPath + _turretName + ".prefab");
            }
            else
            {
                AssetDatabase.CreateFolder("Assets", "Turrets");
                _turretPathAndName = AssetDatabase.GenerateUniqueAssetPath(_turrettPath + _turretName + ".prefab");
            }
            GameObject _gameObject = new GameObject();
            if (_2d)
            {
                if (_srBool)
                {
                    _gameObject.AddComponent<SpriteRenderer>();
                    if (_sprite != null)
                    {
                        _gameObject.GetComponent<SpriteRenderer>().sprite = _sprite;
                    }
                }
                if (_rb)
                {
                    _gameObject.AddComponent<Rigidbody2D>();
                }
                if (_animatorBool)
                {
                    _gameObject.AddComponent<Animator>();
                }
                if (_box)
                {
                    _gameObject.AddComponent<BoxCollider2D>();
                    if (_triger)
                    {
                        _gameObject.GetComponent<BoxCollider2D>().isTrigger = _triger;
                    }
                }
                if (_circle)
                {
                    _gameObject.AddComponent<CircleCollider2D>();
                    _gameObject.GetComponent<CircleCollider2D>().isTrigger = _triger;

                }
                if (_capsule)
                {
                    _gameObject.AddComponent<CapsuleCollider2D>();
                    _gameObject.GetComponent<CapsuleCollider2D>().isTrigger = _triger;

                }
                if (_as)
                {
                    _gameObject.AddComponent<AudioSource>();
                }
                /* for (int count = 0; count < _extraComponents.Count - 1; count++)
                 {
                     if (_extraComponents[count] != null)
                     {

                     }

                 }
             }
             else
             {

                */
            }
            else
            {
                if (_rb)
                {
                    _gameObject.AddComponent<Rigidbody>();
                }
                if (_animatorBool)
                {
                    _gameObject.AddComponent<Animator>();
                }
                if (_box)
                {
                    _gameObject.AddComponent<BoxCollider>();
                    if (_triger)
                    {
                        _gameObject.GetComponent<BoxCollider>().isTrigger = _triger;
                    }
                }
                if (_sphere)
                {
                    _gameObject.AddComponent<SphereCollider>();
                    _gameObject.GetComponent<SphereCollider>().isTrigger = _triger;

                }
                if (_capsule)
                {
                    _gameObject.AddComponent<CapsuleCollider>();
                    _gameObject.GetComponent<CapsuleCollider>().isTrigger = _triger;

                }
                if (_as)
                {
                    _gameObject.AddComponent<AudioSource>();
                }
            }
            ScriptableObject _scriptable = ScriptableObject.CreateInstance<TurretsStats>();
            _validTurretStatsPath = AssetDatabase.IsValidFolder("Assets/Turret Stats"); 
            if (_validTurretStatsPath)
            {
                _scriptablePathAndName = AssetDatabase.GenerateUniqueAssetPath(_scriptablePath + _scriptableName + ".asset");
            }
            else
            {
                AssetDatabase.CreateFolder("Assets", "Turret Stats");
                _scriptablePathAndName = AssetDatabase.GenerateUniqueAssetPath(_scriptablePath + _scriptableName + ".asset");
            }

            AssetDatabase.CreateAsset(_scriptable, _scriptablePathAndName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            TurretsStats _turretStats = AssetDatabase.LoadAssetAtPath<TurretsStats>(_scriptablePathAndName);
            _turretStats._damage = _damage;
            _turretStats._attackSpeed = _attackSpeed;
            _turretStats._range = _range;

            _gameObject.name = _turretName;
            PrefabUtility.SaveAsPrefabAsset(_gameObject, _turretPathAndName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
       
    }
