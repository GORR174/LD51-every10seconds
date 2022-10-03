using System;
using UnityEngine;

namespace AI
{
    public class RadiusTrigger : MonoBehaviour
    {
        private CircleCollider2D circleCollider;

        public event Action<Collider2D> onTriggerEnter;
        
        public event Action<Collider2D> onTriggerExit; 

        private void Start()
        {
            circleCollider = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            onTriggerEnter?.Invoke(col);
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            onTriggerExit?.Invoke(col);
        }
    }
}