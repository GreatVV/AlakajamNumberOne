using System;
using System.Linq;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public class IngredientViewFactory : InjectableBehaviour
{
    [Serializable]
    public class IngredientDesc
    {
        public IngredientsType Type;
        public ColorType ColorType;
        public GameObject View;
        public Sprite Icon;
    }

    public IngredientDesc[] Descs;
    
    public IngredientDesc ById(IngredientsType type, ColorType colorType)
    {
        return Descs.First(x => x.Type == type && x.ColorType == colorType);
    }
    
    protected override void OnInject()
    {
        _contexts.game.SetIngredientViewFactory(this);
    }

    [ContextMenu("Fill with all values")]
    public void Fill()
    {
        var ingredientsTypes = (IngredientsType[]) (Enum.GetValues(typeof(IngredientsType)));
        var colors = (ColorType[]) Enum.GetValues(typeof(ColorType));
        Descs = new IngredientDesc[ingredientsTypes.Length * colors.Length];
        for (int i = 0; i < ingredientsTypes.Length; i++)
        {
            for (int j = 0; j < colors.Length; j++)
            {
                Descs[i * colors.Length + j] = new IngredientDesc()
                {
                    Type = ingredientsTypes[i],
                    ColorType = colors[j]
                };                
            }
        }
    }
}