using System;
using System.Resources;
using UnityEngine;

[Serializable]
public class Boss
{
    public string BossName;
    public RecipeCollection PossibleRecipes;
    public GenerationDescription GenerationDescription;
    public float Health = 3;
    public float TimeBeforeSpawn = 30;
    public GameObject Prefab;

    public override string ToString()
    {
        return string.Format("Boss {0} health: {1}", BossName, Health);
    }
}