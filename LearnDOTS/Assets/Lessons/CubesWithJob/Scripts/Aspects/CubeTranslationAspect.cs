using Lessons.CubesWithJob.Scripts.Components;
using Lessons.GenerateAndDestroyCubeWithEcb.Scripts.AuthoringAndComponents;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Lessons.CubesWithJob.Scripts.Aspects
{
    public readonly partial struct CubeTranslationAspect : IAspect
    {
        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRO<MoveSpeed> _moveSpeed;
        private readonly RefRO<RandomTargetPos> _targetPos;

        public bool IsNeedDestroy()
        {
            var distance = math.distance(_transform.ValueRO.Position, _targetPos.ValueRO.TargetPos);
            if (distance < 0.02f)
                return true;
            else
                return false;
        }

        public void MoveCube(float deltaTime)
        {
            var direction = math.normalize(_targetPos.ValueRO.TargetPos - _transform.ValueRO.Position);
            _transform.ValueRW.Position += direction * deltaTime * _moveSpeed.ValueRO.Speed;
        }
    }
}
