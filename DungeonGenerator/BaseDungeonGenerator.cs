using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

namespace MapGenetaroion.Dungeon
{
    public abstract class BaseDungeonGenerator : MonoBehaviour
    {
        public static BaseDungeonGenerator Instance { get; private set; }

        [SerializeField]
        private GenerationState _state = GenerationState.Finished;
        public GenerationState State { get { return _state; } }

        [SerializeField]
        private int _phaseIndex = 0;
        protected List<IGenerationPhase> _generationPhaseList = new List<IGenerationPhase>();

        [SerializeField, Space]
        private bool _setSeed = false;

        [SerializeField]
        private int _seed = 0;

        [SerializeField, Space]
        protected Vector2Int _dungeonSize = new Vector2Int();

        private Coroutine _currentCorutine = null;

        public event Action GenerationStarted = null;
        public event Action GenerationCanceled = null;
        public event Action<int> PhaseCompleted = null;
        public event Action GenerationCompleted = null;

        protected virtual void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            InitializeGenerator();

            for (int i = 0; i < _generationPhaseList.Count; i++)
            {
                _generationPhaseList[i].Generator = this;
            }
        }

        protected virtual void Start()
        {
            enabled = false;
        }

        private void Update()
        {
            switch (_state)
            {
                case GenerationState.Start:
                    _phaseIndex = 0;
                    if (_setSeed)
                    {
                        UnityEngine.Random.InitState(_seed);
                    }
                    InitializePhase();
                    _state = GenerationState.Generation;
                    break;

                case GenerationState.Generation:
                    if (_generationPhaseList[_phaseIndex].IsDone)
                    {
                        if (PhaseCompleted != null)
                        {
                            PhaseCompleted(_phaseIndex);
                        }

                        if ((_generationPhaseList.Count - 1) == _phaseIndex)
                        {
                            _state = GenerationState.Finished;
                        }
                        else
                        {
                            _generationPhaseList[_phaseIndex + 1].RoomList = _generationPhaseList[_phaseIndex].RoomList;
                            if (_generationPhaseList[_phaseIndex].Pause)
                            {
                                PauseGeneration();
                            }
                            else
                            {
                                GoToNextPhase();
                            }
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

                case GenerationState.Pause:
                    break;
            }
        }

        private void GoToNextPhase()
        {
            ++_phaseIndex;
            InitializePhase();
        }

        public abstract IRoomInfo GetRoomInfo(Vector2 position);

        protected abstract void InitializeGenerator();

        protected void InitializePhase()
        {
            _generationPhaseList[_phaseIndex].DungeonSize = _dungeonSize;
            _generationPhaseList[_phaseIndex].Initialize();
            _currentCorutine = StartCoroutine(_generationPhaseList[_phaseIndex].Generate());
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

        public void PauseGeneration()
        {
            enabled = false;
            _state = GenerationState.Pause;
        }

        public void ResumeGeneration()
        {
            enabled = true;
            _state = GenerationState.Generation;
            GoToNextPhase();
        }
    }
}
