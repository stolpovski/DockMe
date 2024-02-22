using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyDocker
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

        [SerializeField] private float _dot;

        [SerializeField] private float xD;
        [SerializeField] private float yD;
        [SerializeField] private float zD;

        [SerializeField] private Vector3 relativePosition;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (_target)
            {
                /*Vector3 forward = transform.TransformDirection(Vector3.forward);
                Vector3 toOther = _target.transform.position - transform.position;

                _dot = Vector3.Dot(forward, toOther);

                if (Vector3.Dot(forward, toOther) < 0)
                {
                    print("The other transform is behind me!");
                }*/

                //relativePosition = transform.InverseTransformPoint(_target.transform.position);

                Vector3 target_local = _target.transform.InverseTransformPoint(transform.position);
                Vector3 this_local = transform.InverseTransformPoint(_target.transform.position);
                xD = target_local.x - this_local.x;
                yD = target_local.y - this_local.y;
                zD = target_local.z - this_local.z;
            }
        }
    }
}
