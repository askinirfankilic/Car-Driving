using UnityEngine;

public class EntranceInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject _canvasObject;

    public void ShowEntrance(bool state)
    {
        _canvasObject.SetActive(state);
    }
}