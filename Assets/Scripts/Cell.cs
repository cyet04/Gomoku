using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CellState
{
    Cross, 
    Circle,
    Empty,
}

public class Cell : MonoBehaviour, IPointerClickHandler
{
    private int x, y;
    private CellState state = CellState.Empty;
    [SerializeField] private Sprite crossPrefab;
    [SerializeField] private Sprite circlePrefab;

    private Image cellImage;

    private void Start()
    {
        cellImage = this.GetComponent<Image>();
    }

    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
        state = CellState.Empty;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click on cell");
        cellImage.sprite = crossPrefab;
    }
}
