using System.Collections.Generic;
using UnityEngine;

namespace Core.Player.Scripts
{
    public class DestroyedCarController : MonoBehaviour
    {
        [SerializeField] private List<Rigidbody> rigidbodies;
        [SerializeField] private float force;

        private void Start()
        {
            foreach (var rigidbody in rigidbodies)
                rigidbody.AddForce(Vector3.right * force, ForceMode.Impulse);
        }
    }
}
