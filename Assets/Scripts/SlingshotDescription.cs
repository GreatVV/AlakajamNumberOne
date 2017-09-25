using System;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
[Serializable]
public class SlingshotDescription
{
    public float MinPower = 1;
    public float MaxPower = 100;
    public float MaxPowerTime = 2f;
}