using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "New Turret", menuName = "TurretStats")]
public class TurretsStats : ScriptableObject
{
    public int _damage;
    public float _attackSpeed;
    public float _range;
}
