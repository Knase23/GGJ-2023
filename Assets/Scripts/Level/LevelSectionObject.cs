using UnityEngine;

namespace Level
{
    public class LevelSectionObject : MonoBehaviour
    {
        public LevelSection reference;

        public float GetPosition() => transform.position.y;
        public float GetHeight() => reference.GetHeight();
    }
}