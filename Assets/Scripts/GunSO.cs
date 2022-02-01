using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun", fileName = "New Gun")]
public class GunSO : ScriptableObject
{
    public GameObject gun;
    public Transform gunPoint;
}
