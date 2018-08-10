using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace ETModel
{
    public class Bullet :MonoBehaviour
    {

        public Vector3 FirstDirSpeed = Vector3.zero  ;
        public bool ShowTime = false;

        public float DeathTime = 20.0f;
        private float CurElapseTime = 0;

        private Rigidbody body;
        private void Start()
        {
            body = GetComponent<Rigidbody>();
            body.velocity = FirstDirSpeed;
            CurElapseTime = 0;
        }
        private void FixedUpdate()
        {
            if (ShowTime)
            {
                body.velocity = FirstDirSpeed;
                ShowTime = false;
            }

            CurElapseTime += Time.deltaTime;
            if(CurElapseTime > DeathTime)
            {
                Destroy();
            }
        }
        private void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"OnTriggerEnter: Colloder {other.name}");
        }
        private void Destroy()
        {
            GameObject.DestroyImmediate(gameObject);
        }
    }
}
