using System.Collections.Generic;
using Core;
using UnityEngine;
using Zenject;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private List<Stage> _stages;

    private Stage _currentStage;
    private int _currentStageIndex = 0;

    private PlayerInput _playerInput;
    private EventManager _eventManager;

    [Inject]
    private void Construct(PlayerInput playerInput, EventManager eventManager)
    {
        _playerInput = playerInput;
        _eventManager = eventManager;
    }

    private void Start()
    {
        DisableStages(0);
        InitializeFirstStage();
    }

    private void InitializeFirstStage()
    {
        _currentStageIndex = 0;
        _currentStage = _stages[0];
        _currentStage.Current = true;
        _currentStage.SetCarCurrent(true);
        _currentStage.Initialize();

        SubscribeFirstInput();
    }

    private void SubscribeFirstInput()
    {
        _playerInput.OnMouseButtonDown += OnMouseButtonDownHandler;
    }

    /// <summary>
    /// Checks first player input at start of every stage.
    /// </summary>
    private void OnMouseButtonDownHandler(Vector2 position)
    {
        foreach (var stage in _stages)
        {
            if (stage.isActiveAndEnabled)
            {
                stage.SetCarActive(true);
            }
        }

        _playerInput.OnMouseButtonDown -= OnMouseButtonDownHandler;
    }

    private void DisableStages(int exceptionIndex)
    {
        for (int i = 0; i < _stages.Count; i++)
        {
            if (i == exceptionIndex) continue;
            _stages[i].gameObject.SetActive(false);
        }
    }

    public void LoadNextStage()
    {
        if (_currentStageIndex == _stages.Count - 1)
        {
            //Next Level
            _eventManager.InvokeGameStateChanged(GameState.Finish);
        }
        else
        {
            // Set current player controlled stage.
            _currentStageIndex++;
            _currentStage = _stages[_currentStageIndex];

            _currentStage.Current = true;
            _currentStage.SetCarCurrent(true);
            _currentStage.gameObject.SetActive(true);
            _currentStage.Initialize();

            // Set previous stages as auto controlled.
            for (int i = 0; i < _currentStageIndex; i++)
            {
                _stages[i].Current = false;
                _stages[i].InitializeAutoControlledCar();
                _stages[i].SetCarCurrent(false);
                _stages[i].Initialize();
                _stages[i].gameObject.SetActive(true);
            }

            SubscribeFirstInput();
        }
    }

    public void ReloadStage()
    {
        for (int i = 0; i < _currentStageIndex; i++)
        {
            _stages[i].Current = false;
            _stages[i].InitializeAutoControlledCar();
            _stages[i].SetCarCurrent(false);
            _stages[i].SetCarActive(false);
            _stages[i].Initialize();
            _stages[i].gameObject.SetActive(true);
        }

        _currentStage.Current = true;
        _currentStage.SetCarCurrent(true);
        _currentStage.SetCarActive(false);
        _currentStage.Initialize();

        SubscribeFirstInput();
    }
}