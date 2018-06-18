using MapGenetaroion.BaseGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenetaroion.DungeonGenerator
{
    public class DebugCameraInitialization : MonoBehaviour, IGenerationInitalization
    {
        [SerializeField] private Camera _debugCamera = null;
        [SerializeField] private KeyCode showdDebugCamera = KeyCode.F12;

        private void Awake()
        {
            _debugCamera.enabled = false;
        }

        public void Initialize(LevelGenerator generator, object[] generationData)
        {
            var settings = LevelGenerator.GetMetaDataObject<GenerationSettings>(generationData);

            Vector3 position = new Vector3(((settings.Size.x - 1) * settings.RoomSize.x) / 2, 100, ((settings.Size.y - 1) * settings.RoomSize.y) / 2);

            _debugCamera.orthographicSize = (settings.RoomSize.x * settings.Size.x) / 2;
            _debugCamera.transform.position = position;
        }

        private void Update()
        {
            bool buttonPressed = Input.GetKey(showdDebugCamera);
            if (_debugCamera.enabled != buttonPressed)
                _debugCamera.enabled = buttonPressed;            
        }
    }
}