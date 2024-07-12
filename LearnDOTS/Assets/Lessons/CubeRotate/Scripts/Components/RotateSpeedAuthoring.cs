using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lesson2.Scripts.Components
{
    public struct RotateSpeed : IComponentData
    {
        public float Speed;
    }

    public class RotateSpeedAuthoring : MonoBehaviour
    {
        public float rotateSpeed;
    }

    public class RotateSpeedBaker : Baker<RotateSpeedAuthoring>
    {
        public override void Bake(RotateSpeedAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotateSpeed { Speed = math.radians(authoring.rotateSpeed) });
        }
    }
}