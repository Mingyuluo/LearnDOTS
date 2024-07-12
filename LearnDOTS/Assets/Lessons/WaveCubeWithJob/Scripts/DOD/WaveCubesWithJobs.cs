using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Jobs;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Jobs;

namespace  DOD
{
    [BurstCompile]
    struct WaveCubesJob : IJobParallelForTransform
    {
        public float DeltaTime;
        public void Execute(int index, TransformAccess transform)
        {
            var distance = Vector3.Distance(transform.position, Vector3.zero);
            transform.localPosition+=Vector3.up*Mathf.Sin(DeltaTime*3f+distance*0.2f);
        }
    }
    
    public class WaveCubesWithJobs : MonoBehaviour
    {
        private static readonly ProfilerMarker<int> ProfilerMarker =
            new ProfilerMarker<int>("WaveCubeWithJob UpdateTransform", "Object Count");
    
        [SerializeField] private GameObject cubePrototype;
        [SerializeField,Range(1,100)] private int xHalfCount = 50;
        [SerializeField,Range(1,100)] private int zHalfCount = 50;
        private TransformAccessArray _cubeTransforms;

        private void Start()
        {
            _cubeTransforms = new TransformAccessArray(4 * xHalfCount * zHalfCount);
            for (int x = -xHalfCount; x < xHalfCount; x++)
            {
                for (int z = -zHalfCount; z < zHalfCount; z++)
                {
                    var cube = Instantiate(cubePrototype);
                    cube.transform.position = new Vector3(x * 1.1f, 0, z * 1.1f);
                    _cubeTransforms.Add(cube.transform);
                }
            }
        }
        private void Update()
        {
            using (ProfilerMarker.Auto(_cubeTransforms.length))
            {
                var waveCubesJob = new WaveCubesJob()
                {
                    DeltaTime = Time.time
                };
                var waveCubesJobHanlde = waveCubesJob.Schedule(_cubeTransforms);
                waveCubesJobHanlde.Complete();
            }
        }

        private void OnDisable()
        {
            _cubeTransforms.Dispose();
        }
    }
}
