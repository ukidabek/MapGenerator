using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System;

namespace MapGenetaroion.Dungeon
{
    public abstract class DungeonGenerator : MonoBehaviour
    {
        [SerializeField]
        private GenerationState _state = GenerationState.Finished;
        public GenerationState State { get { return _state; } }

        [SerializeField]
        private ILayoutGenerator _layoutGenerator = null;

        [SerializeField]
        private ILayoutConstructor _layoutConstructor = null;

        private Coroutine _currentCorutine = null;

        public event Action GenerationStarted = null;
        public event Action GenerationCanceled = null;
        public event Action GenerationCompleted = null;

        private void Awake()
        {
            enabled = false;
            _layoutGenerator = GetComponent<ILayoutGenerator>();
            _layoutConstructor = GetComponent<ILayoutConstructor>();
        }

        private void Update()
        {
            switch (_state)
            {
                case GenerationState.LayoutGenerationInitialization:
                    _currentCorutine = StartCoroutine(_layoutGenerator.GenerateLayout());
                    ++_state;
                    break;

                case GenerationState.LayoutGeneration:
                    if (_layoutGenerator.IsDone)
                    {
                        ++_state;
                    }
                    break;

                case GenerationState.LayoutConstructingInitialization:
                    _layoutConstructor.RoomList = _layoutGenerator.RoomList;
                    _currentCorutine = StartCoroutine(_layoutConstructor.BuildLayout());
                    ++_state;
                    break;

                case GenerationState.LayoutConstructing:
                    if (_layoutConstructor.IsDone)
                    {
                        ++_state;
                    }
                    break;

                case GenerationState.Finished:
                    enabled = false;
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
            _state = GenerationState.LayoutGenerationInitialization;
            enabled = true;

            if(GenerationStarted != null)
            {
                GenerationStarted();
            }
        }

        public void CancelGeneration()
        {
            _state = GenerationState.Finished;
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
