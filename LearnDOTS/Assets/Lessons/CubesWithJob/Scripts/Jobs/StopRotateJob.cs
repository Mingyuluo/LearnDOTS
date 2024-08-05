using Lessons.CubesWithJob.Scripts.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Lessons.CubesWithJob.Scripts.Jobs
{
    public partial struct StopRotateJob : IJobEntity
    {
        public float DeltaTime;
        public float ElapsedTime;
        public float2 LeftRightBound;
        [NativeDisableParallelForRestriction]public ComponentLookup<RotateSpeed> RotateSpeedLookUp;

        public void Execute(Entity entity,ref LocalTransform transform)
        {
            bool enabled = RotateSpeedLookUp.IsComponentEnabled(entity);
            if(enabled)
            {
                if (transform.Position.x > LeftRightBound.x &&
                    transform.Position.x < LeftRightBound.y)
                {
                    RotateSpeedLookUp.SetComponentEnabled(entity, false);
                }
                else
                {
                    var speed = RotateSpeedLookUp.GetRefRO(entity).ValueRO.Speed;
                    transform = transform.RotateY(speed * DeltaTime);
                }
            }
            else
            {
                if (transform.Position.x < LeftRightBound.x ||
                    transform.Position.x > LeftRightBound.y)
                {
                    RotateSpeedLookUp.SetComponentEnabled(entity, true);
                    transform.Scale = 1.0f;
                }
                else
                {
                    transform.Scale = math.sin(ElapsedTime*4)*0.3f + 1.0f;
                }
            }

        }
    }
}
