using System.Collections.Generic;
using UnityEngine;

namespace Core.Track.Scripts
{
    public class TrackController : MonoBehaviour
    {
        [SerializeField] private GameObject trackElementPrefab;
        [SerializeField] private int trackSize;

        private readonly List<GameObject> _trackElements = new();

        public void GenerateTrack()
        {
            for (var i = 0; i < trackSize; i++)
            {
                var trackElement = Instantiate(trackElementPrefab, transform);
                var offsetZ = i * trackElement.GetComponent<TrackElement>().GetSizeZ();
                trackElement.transform.position = new Vector3(0f, 0f, offsetZ);
                _trackElements.Add(trackElement);
            }
        }
    }
}
