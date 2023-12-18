using UnityEngine;

namespace Core.Track.Scripts
{
    public class TrackElement : MonoBehaviour
    {
        [SerializeField] private MeshRenderer renderer;

        public float SizeZ => renderer.bounds.size.z;
    }
}
