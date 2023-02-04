using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level
{
    public class SectionCreated : MonoBehaviour
    {
        public List<LevelSection> _levelSectionsToChooseFrom;
        private List<GameObject> sectionPrefabs;
        public Transform CamerasPosition;

        public List<LevelSectionObject> sections;
        public LevelSectionObject latestCreated;

        public void Start()
        {
            NewSection();
            NewSection();
            NewSection();
        }

        private void Update()
        {
            for (int i = sections.Count -1; i >= 0; i--)
            {
                if (sections[i].GetPosition() > CamerasPosition.position.y + sections[i].GetHeight())
                {
                    //Remove this
                    LevelSectionObject section = sections[i];
                    sections.Remove(section);
                    Destroy(section.gameObject);
                    //Create new one thats under the last one
                    NewSection();
                    if(sections.Count < 5)
                        NewSection();
                }
            }
        }

        public void NewSection()
        {
            float spawnPosition = 0;
            if (latestCreated)
            {
                spawnPosition = latestCreated.GetPosition();
                spawnPosition -= latestCreated.reference.GetHeight() / 2;
            }
            
            int indexOfSection = 0;


            if (_levelSectionsToChooseFrom.Count > 1) 
            {
                do
                {
                    indexOfSection = Random.Range(0, _levelSectionsToChooseFrom.Count);
                } while (latestCreated && latestCreated.reference == _levelSectionsToChooseFrom[indexOfSection]);
            }

            spawnPosition -= _levelSectionsToChooseFrom[indexOfSection].GetHeight() / 2f;
            var g =Instantiate(_levelSectionsToChooseFrom[indexOfSection].prefab, new Vector3(0,spawnPosition,0), quaternion.identity, transform);
            latestCreated = g.GetComponent<LevelSectionObject>();
            latestCreated.reference = _levelSectionsToChooseFrom[indexOfSection];
            sections.Add(latestCreated);
        }

    
    }
}