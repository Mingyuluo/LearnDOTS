using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lessons.WaveCubeWithEntities.Scripts.AuthoringAndComponents
{
    public struct RotateAndMoveSpeed : IComponentData
    {
        public float RotateSpeed;
        public float MoveSpeed;
    }

    public class RotateAndMoveSpeedAuthoring : MonoBehaviour
    {
        public float rotateSpeed;
        public float moveSpeed;

        public class RotateAndMoveSpeedBaker : Baker<RotateAndMoveSpeedAuthoring>
        {
            public override void Bake(RotateAndMoveSpeedAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new RotateAndMoveSpeed { RotateSpeed = authoring.rotateSpeed, MoveSpeed = authoring.moveSpeed });
            }
        }
    }
}
