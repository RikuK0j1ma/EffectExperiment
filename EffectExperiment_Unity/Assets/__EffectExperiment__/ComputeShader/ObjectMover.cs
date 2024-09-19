using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace EffectExperiment.ComputeShader
{
    public class ObjectMover : MonoBehaviour
    {
        [SerializeField] private UnityEngine.ComputeShader _computeShader;
        [SerializeField] Transform _moveTarget;
        
        private ComputeBuffer _buffer;
        private Vector3 center = Vector3.zero;

        private void Start()
        {
            
            _buffer = new ComputeBuffer(1, Marshal.SizeOf(typeof(Vector2))); 
            _computeShader.SetBuffer(_computeShader.FindKernel("CSMain"), "ResultBuffer", _buffer);
        }

        private void Update()
        {
            _computeShader.SetFloats("position", center.x, center.y); 
            _computeShader.SetFloat("time", Time.time);
            _computeShader.Dispatch(0, 8, 8, 1);

            var data = new float[2];
            _buffer.GetData(data);

            Vector3 pos = _moveTarget.transform.localPosition;
            pos.x = data[0];
            pos.y = data[1];
            _moveTarget.transform.localPosition = pos;
        }

        private void OnDestroy()
        {
            _buffer.Release();
        }
    }
}
