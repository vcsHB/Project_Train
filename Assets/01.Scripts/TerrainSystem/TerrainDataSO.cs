using UnityEngine;
namespace Project_Train.TerrainSystem
{
    [CreateAssetMenu(menuName = "SO/TerrainData")]
    public class TerrainDataSO : ScriptableObject
    {
        [Header("General Setting")]
        public string terrainName = "TERRAIN_";
        [TextArea] public string terrainDescription;
        
        // 
        [Header("Environment Setting")]
        [Range(0.1f, 10f)] public float visibility = 1f;
        public float temperature = 20f;
        [Range(0f, 1f)] public float lightIntensity = 0.5f;

        // TODO
        [Header("Terrain Benefit Setting")]
        public float rangeBenefitByheight = 1f;
        [Range(-10f, 10f)] public float rangeBenefitRandomize;



    }
}