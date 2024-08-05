using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lessons.CubesWithJob.Scripts.Components
{
    public struct MoveSpeed : IComponentData
    {
        public float Speed;
    }
}
