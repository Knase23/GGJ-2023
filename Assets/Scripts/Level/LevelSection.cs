using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    [CreateAssetMenu(fileName = "LevelSection")]
    public class LevelSection : ScriptableObject
    {
        [SerializeField]
        private float sectionHeight;

        public GameObject prefab;
        public float GetHeight() => sectionHeight;
    }
}