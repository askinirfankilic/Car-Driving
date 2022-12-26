using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Stage : MonoBehaviour
{
    public bool Current
    {
        get => _current;
        set
        {
            _current = value;
            ShowCurrentExitIndicator(_current);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        void DrawBezierStage(Vector3 stagePos, Vector3 targetPos)
        {
            float halfHeight = (stagePos.y - targetPos.y) * 0.5f;
            Vector3 offset = Vector3.up * halfHeight;

            Handles.DrawBezier(
                stagePos,
                targetPos,
                stagePos - offset,
                targetPos + offset,
                Color.yellow,
                EditorGUIUtility.whiteTexture, 1);
        }

        Vector3 stagePos = transform.position;

        Vector3 carPos = _car.transform.position;
        Vector3 exitPos = _exit.transform.position;
        Vector3 entrancePos = _entrance.position;

        DrawBezierStage(stagePos, carPos);
        DrawBezierStage(stagePos, exitPos);
        DrawBezierStage(stagePos, entrancePos);
    }
#endif


    [SerializeField]
    private CarInteraction _car;
    [SerializeField]
    private CarMovement _carMovement;
    [SerializeField]
    private Transform _entrance;
    [SerializeField]
    private ExitInteraction _exit;
    [SerializeField]
    private bool _current = false;

    private Vector3 _carStartingPosition;
    private Vector3 _carStartingRotation;

    private void Awake()
    {
        _carStartingPosition = _car.transform.position;
        _carStartingRotation = _car.transform.eulerAngles;
    }

    public void Initialize()
    {
        _car.transform.position = _carStartingPosition;
        _car.transform.eulerAngles = _carStartingRotation;
    }

    public void SetCarActive(bool state)
    {
        _carMovement.IsActive = state;
    }

    public void SetCarCurrent(bool state)
    {
        _carMovement.IsCurrent = state;
        _exit.IsCurrent = state;
    }

    public void InitializeAutoControlledCar()
    {
        _carMovement.InitializeAutoControlled();
    }

    private void ShowCurrentExitIndicator(bool state)
    {
        _exit.ShowCurrentExitIndicator(state);
    }
}