using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using MapGenetaroion.BaseGenerator;

namespace MapGenetaroion.DungeonGenerator
{
    public class DunegonEncouterPlacer : MonoBehaviour, IGenerationPhase
    {
        public BaseDungeonGenerator Generator { get; set; }

        public List<IRoomInfo> RoomList { get; set; }

        public Vector2Int DungeonSize { get; set; }

        [SerializeField] private bool _isDone = false;
        public bool IsDone { get { return _isDone; } }

        public bool Pause { get { return false; } }

        [SerializeField, Range(0, 1f)] private float _encounterProbability = .5f;

        [SerializeField] private List<GameObject> _aiManagersPrefabList = new List<GameObject>();
        [SerializeField] private List<EncounterScenario> encounterScenariosList = new List<EncounterScenario>();

        public IEnumerator Generate()
        {
            for (int i = 0; i < RoomList.Count; i++)
            {
                if(Random.Range(0f, 1f) <= _encounterProbability)
                {
                    AIManager aIManager = CreateAIManager();
                    aIManager.Scenario = GetScenario();
                    DungeonRoomInfo info = RoomList[i] as DungeonRoomInfo;

                    if(info.RoomObject != null)
                    {
                        aIManager.gameObject.transform.SetParent(info.RoomObject.transform);
                        aIManager.gameObject.transform.localPosition = Vector3.zero;
                        aIManager.gameObject.transform.localRotation = Quaternion.identity;
                    }
                }
                yield return new PauseYield(Generator);
            }
        }

        public void Initialize() {}

        private AIManager CreateAIManager()
        {
            GameObject instance = Instantiate(_aiManagersPrefabList[Random.Range(0, _aiManagersPrefabList.Count)]);
            return instance.GetComponent<AIManager>();
        }

        private EncounterScenario GetScenario()
        {
            return encounterScenariosList[Random.Range(0, encounterScenariosList.Count)];
        }
    }
}