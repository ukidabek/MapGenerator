using UnityEngine;
using UnityEngine.Events;

using System;
using System.Collections;
using System.Collections.Generic;

namespace MapGenetaroion.BaseGenerator
{
    using Object = UnityEngine.Object;

    public abstract class BaseDungeonGenerator : MonoBehaviour
    {
        public static BaseDungeonGenerator Instance { get; private set; }

        [SerializeField] protected GenerationState _state = GenerationState.Finished;
        public GenerationState State { get { return _state; } }

        [SerializeField] protected List<Object> _InitializationObjectList = new List<Object>();
        protected List<IGenerationInitalization> _generationInitializationList = new List<IGenerationInitalization>();

        [SerializeField] protected int _phaseIndex = 0;
        [SerializeField] protected List<Object> _phaseObjectList = new List<Object>();
        protected List<IGenerationPhase> _generationPhaseList = new List<IGenerationPhase>();

        private Coroutine _currentCoroutine = null;

        [Space]
        public UnityEvent GenerationStarted = new UnityEvent();
        public UnityEvent GenerationCanceled = new UnityEvent();
        public PhaseCompletedEvent PhaseCompleted = new PhaseCompletedEvent();
        public UnityEvent GenerationCompleted = new UnityEvent();

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

                    InitializePhase();
                    _state = GenerationState.Generation;
                    break;

                case GenerationState.Generation:
                    if (_generationPhaseList[_phaseIndex].IsDone)
                    {
                        PhaseCompleted.Invoke(_phaseIndex);
                        
                        if ((_generationPhaseList.Count - 1) == _phaseIndex)
                        {
                            _state = GenerationState.Finished;
                        }
                        else
                        {
                            if (_generationPhaseList[_phaseIndex].Pause)
                                PauseGeneration();
                            else
                                GoToNextPhase();
                        }
                    }
                    break;

                case GenerationState.Finished:
                    enabled = false;
                    StopCoroutine(_currentCoroutine);
                    _currentCoroutine = null;

                    GenerationCompleted.Invoke();
                    break;

                case GenerationState.Pause:
                    break;
            }
        }

        protected virtual void GoToNextPhase()
        {
            ++_phaseIndex;
            InitializePhase();
        }

        protected virtual void InitializeGenerator()
        {
            for (int i = 0; i < _phaseObjectList.Count; i++)
            {
                var phaseObject = _phaseObjectList[i];
                if (ValidatePhase(phaseObject))
                {
                    _generationPhaseList.Add(phaseObject as IGenerationPhase);
                    _generationPhaseList[_generationPhaseList.Count - 1].Generator = this;
                }
                else
                    Debug.LogErrorFormat("Selected object at index {0} is not a generation phase!", i);
            }

            for (int i = 0; i < _InitializationObjectList.Count; i++)
            {
                if (_InitializationObjectList[i] is IGenerationInitalization)
                    (_InitializationObjectList[i] as IGenerationInitalization).Initialize(this);
            }
        }

        private bool ValidatePhase(Object phaseObject)
        {
            return phaseObject is IGenerationPhase;
        }

        protected virtual void InitializePhase()
        {
            _generationPhaseList[_phaseIndex].Initialize();
            _currentCoroutine = StartCoroutine(_generationPhaseList[_phaseIndex].Generate());
        }

        public void StartGeneration()
        {
            _state = GenerationState.Start;
            enabled = true;

            GenerationStarted.Invoke();
        }

        public void CancelGeneration()
        {
            _state = GenerationState.Finished;
            enabled = false;

            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            GenerationCanceled.Invoke();
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

    [Serializable]
    public sealed class PhaseCompletedEvent : UnityEvent<int> {}

}
