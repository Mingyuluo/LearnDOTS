using Unity.Entities;
using UnityEngine;

public struct RedCubeTag : IComponentData
{
    
}

public class RedCubeTagAuthoring : MonoBehaviour
{
    public class RedCubeTagBaker : Baker<RedCubeTagAuthoring>
    {
        public override void Bake(RedCubeTagAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<RedCubeTag>(entity);
        }
    }
}
