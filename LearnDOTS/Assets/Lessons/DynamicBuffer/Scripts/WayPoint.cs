using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lessons.DynamicBuffer.Scripts
{
    [InternalBufferCapacity(8)]
    struct WayPoint : IBufferElementData
    {
        public float3 Point;
    }
    public class WayPointAuthoring : MonoBehaviour
    {
        public List<Vector3> wayPoints;

        private class WayPointBaker : Baker<WayPointAuthoring>
        {
            public override void Bake(WayPointAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                DynamicBuffer<WayPoint> waypoints = AddBuffer<WayPoint>(entity);
                waypoints.Length = authoring.wayPoints.Count;
                for (int i = 0; i < authoring.wayPoints.Count; i++)
                {
                    waypoints[i] = new WayPoint
                    {
                        Point = new float3(authoring.wayPoints[i])
                    };
                }
            }
        }   
    }
}
