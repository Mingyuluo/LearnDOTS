using Unity.Entities;
using UnityEngine;

public struct BlueCubeTag : IComponentData
{
    
}

public class BlueCubeTagAuthoring : MonoBehaviour
{
    public class BlueCubeTagBaker : Baker<BlueCubeTagAuthoring>
    {
        public override void Bake(BlueCubeTagAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<BlueCubeTag>(entity);
        }
    }
}
