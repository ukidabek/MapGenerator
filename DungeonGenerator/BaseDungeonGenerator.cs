using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System;

namespace MapGenetaroion.Dungeon
{
    public abstract class BaseDungeonGenerator : MonoBehaviour
    {
        [SerializeField]
        private GenerationState _state = GenerationState.Finished;
        public GenerationState State { get { return _state; } }

        [SerializeField]
        private int _phaseIndex = 0;
        protected List<IGenerationPhase> _generationPhaseList = new List<IGenerationPhase>();

        [SerializeField, Space]
        private Vector2Int _dungeonSize = new Vector2Int();

        private Coroutine _currentCorutine = null;

        public event Action GenerationStarted = null;
        public event Action GenerationCanceled = null;
        public event Action GenerationCompleted = null;

        private void Awake()
        {
            enabled = false;
            InitializeGenerator();
        }

        protected abstract void InitializeGenerator();

        protected void InitializePhase()
        {
            _generationPhaseList[_phaseIndex].DungeonSize = _dungeonSize;
            _generationPhaseList[_phaseIndex].Initialize();
            _currentCorutine = StartCoroutine(_generationPhaseList[_phaseIndex].Generate());
        }

        private void Update()
        {
            switch (_state)
            {
                case GenerationState.Start:
                    _phaseIndex = 0;
                    InitializePhase();
                    _state = GenerationState.Generation;
                    break;

                case GenerationState.Generation:
                    if(_generationPhaseList[_phaseIndex].IsDone)
                    {
                        if((_generationPhaseList.Count - 1) == _phaseIndex)
                        {
                            _state = GenerationState.Finished;
                        }
                        else
                        {
                            if(_phaseIndex + 1 < _generationPhaseList.Count)
                            {
                                _generationPhaseList[_phaseIndex + 1].RoomList = _generationPhaseList[_phaseIndex].RoomList;
                            }
                            ++_phaseIndex;
                            InitializePhase();
                        }
                    }
                    break;

                case GenerationState.Finished:
                    enabled = false;
                    StopCoroutine(_currentCorutine);
                    _currentCorutine = null;

                    if (GenerationCompleted != null)
                    {
                        GenerationCompleted();
                    }
                    break;
            }
        }

        public void StartGeneration()
        {
            _state = GenerationState.Start;
            enabled = true;

            if(GenerationStarted != null)
            {
                GenerationStarted();
            }
        }

        public void CancelGeneration()
        {
            _state = GenerationState.Finished;
            enabled = false;

            if (_currentCorutine != null)
            {
                StopCoroutine(_currentCorutine);
            }

            if (GenerationCanceled != null)
            {
                GenerationCanceled();
            }
        }
    }
}
