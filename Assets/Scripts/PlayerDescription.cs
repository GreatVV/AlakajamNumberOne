using System;
using UnityEngine;

[Serializable]
public class PlayerDescription
{
    public string Name;
    public float Health = 10;

    public GameObject TargetGameObject;
    public Transform RespawnPosition;
    public BoxCollider GameZoneCollider;
    
    //weapon power
    public ConstantPowerDescription ConstantPowerDescription;
}