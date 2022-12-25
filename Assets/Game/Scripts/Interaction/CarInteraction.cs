using UnityEngine;
using Zenject;

public class CarInteraction : MonoBehaviour
{
    [SerializeField]
    private TriggerChecker2D _triggerChecker;

    private CarMovement _carMovement;

    private StageManager _stageManager;
    
    [SerializeField] private GameObject _currentCarIndicator;

    [Inject]
    private void Construct(StageManager stageManager)
    {
        _stageManager = stageManager;
    }

    private void Awake()
    {
        TryGetComponent(out _carMovement);
        
        _currentCarIndicator.SetActive(false);
        
        _triggerChecker.OnTrigerEntered += OnTriggerEntered;
        _carMovement.ActiveStateChanged += OnActiveStateChanged;
    }

    private void OnActiveStateChanged(bool state)
    {
        _currentCarIndicator.SetActive(state);
    }


    private void OnDestroy()
    {
        _triggerChecker.OnTrigerEntered -= OnTriggerEntered;
        _carMovement.ActiveStateChanged -= OnActiveStateChanged;
    }
    
    private void OnTriggerEntered(Collider2D other)
    {
        if (other.CompareTag(Tags.Obstacle))
        {
            Debug.Log("Obstacle!", other);
            _carMovement.ClearInputData();
            _carMovement.IsActive = false;
            _stageManager.ReloadStage();
        }
        else if (other.CompareTag(Tags.Exit))
        {
            Debug.Log("Exit!", other);
            _carMovement.IsActive = false;
            
            if (_carMovement.IsCurrent)
            {
                _stageManager.LoadNextStage();
            }
        }
        else if (other.CompareTag(Tags.Car))
        {
            if (_carMovement.IsCurrent)
            {
                Debug.Log("Car!", other);
                _carMovement.ClearInputData();
                _carMovement.IsActive = false;
                _stageManager.ReloadStage();
            }
        }
    }
}