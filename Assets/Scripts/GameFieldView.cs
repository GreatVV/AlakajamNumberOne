using UnityEngine;

public class GameFieldView : InjectableBehaviour
{
    public Terrain Terrain;
	
    protected override void OnInject()
    {
        _contexts.game.SetGameField((int)Terrain.terrainData.size.x, (int)Terrain.terrainData.size.z);
    }
}