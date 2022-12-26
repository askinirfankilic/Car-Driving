using UnityEngine;
using Zenject;

public class CarInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TriggerChecker2D _triggerChecker;
    [SerializeField]
    private GameObject _currentCarIndicator;

    //References
    private CarMovement _carMovement;
    private StageManager _stageManager;

    [Inject]
    private void Construct(StageManager stageManager)
    {
        _stageManager = stageManager;
    }

    private void Awake()
    {
        TryGetComponent(out _carMovement);

        _currentCarIndicator.SetActive(false);

        _triggerChecker.TriggerEntered += OnTriggerEntered;
        _carMovement.ActiveStateChanged += OnActiveStateChanged;
    }

    private void OnActiveStateChanged(bool state)
    {
        _currentCarIndicator.SetActive(state);
    }

    private void OnDestroy()
    {
        _triggerChecker.TriggerEntered -= OnTriggerEntered;
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
            var exit = other.GetComponent<ExitInteraction>();
            _carMovement.IsActive = false;

            if (_carMovement.IsCurrent && exit.IsCurrent)
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