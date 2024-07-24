using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

public struct CubeGeneratorByPrefab : IComponentData
{
    public Entity CubePrototype;
    public int CubeCount;
}

public class CubeGeneratorByPrefabAuthoring : MonoBehaviour
{
    public GameObject cubePrototype;
    [Range(1,10)]public int cubeCount;

    public class CubeGeneratorByPrefabBaker : Baker<CubeGeneratorByPrefabAuthoring>
    {
        public override void Bake(CubeGeneratorByPrefabAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity,
                new CubeGeneratorByPrefab
                {
                    CubePrototype = GetEntity(authoring.cubePrototype, TransformUsageFlags.Dynamic),
                    CubeCount = authoring.cubeCount
                });
        }
    }
}
