using Lessons.GenerateAndDestroyCubeWithEcb.Scripts.AuthoringAndComponents;
using Lessons.WaveCubeWithEntities.Scripts.AuthoringAndComponents;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Lessons.WaveCubeWithEntities.Scripts.Aspects
{
    public readonly partial struct RotateAndMoveAspect : IAspect
    {
        private readonly RefRO<RotateAndMoveSpeed> _rotateAndMoveSpeed;
        private readonly RefRW<LocalTransform> _localTransform;
        private readonly RefRO<TargetPos> _targetPos;

        public void RotateAndMove(float elapsedTime, float deltaTime)
        {
            float3 dir = math.normalize(_targetPos.ValueRO.TargetPosValue - _localTransform.ValueRO.Position);
            _localTransform.ValueRW.Position += dir * deltaTime * _rotateAndMoveSpeed.ValueRO.MoveSpeed;
            _localTransform.ValueRW = _localTransform.ValueRW.RotateY(deltaTime * _rotateAndMoveSpeed.ValueRO.RotateSpeed);
        }

        public void SetPosition(float3 position)
        {
            _localTransform.ValueRW.Position = position;
        }
    }
}
