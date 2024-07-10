using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

namespace OOD
{
    public class WaveCubes : MonoBehaviour
    {
        private static readonly ProfilerMarker<int> ProfilerMarker =
            new ProfilerMarker<int>("WaveCube UpdateTransform", "Object Count");
    
        [SerializeField] private GameObject cubePrototype;
        [SerializeField,Range(1,100)] private int xHalfCount = 50;
        [SerializeField,Range(1,100)] private int zHalfCount = 50;
        private List<Transform> _cubeTransforms;

        private void Start()
        {
            _cubeTransforms = new List<Transform>();
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
            using (ProfilerMarker.Auto(_cubeTransforms.Count))
            {
                foreach (var t in _cubeTransforms)
                {
                    var distance = Vector3.Distance(t.position, Vector3.zero);
                    t.localPosition+=Vector3.up*Mathf.Sin(Time.time*3f+distance*0.2f);
                }
            }
        }
    }
}
