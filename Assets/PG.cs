using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DockMe
{
    public class PG : MonoBehaviour
    {
        public Transform target;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 vectorOne = target.eulerAngles;
            Vector3 vectorTwo = transform.eulerAngles;

            float diffX = Mathf.DeltaAngle(vectorOne.x, vectorTwo.x);
            float diffY = Mathf.DeltaAngle(vectorOne.y, vectorTwo.y);
            float diffZ = Mathf.DeltaAngle(vectorOne.z, vectorTwo.z);

            Debug.Log("x:" + diffX + " y:" + diffY + " z:" + diffZ);
        }
    }
}
