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
    private BoardManager boardManager;

    private void Start()
    {
        cellImage = this.GetComponent<Image>();
    }

    public void Init(int x, int y, BoardManager boardManager)
    {
        this.x = x;
        this.y = y;
        state = CellState.Empty;
        this.boardManager = boardManager;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        boardManager.OnCellClicked(x, y);
    }

    public void SetState(CellState newState)
    {
        state = newState;
        switch (state)
        {
            case CellState.Cross:
                cellImage.sprite = crossPrefab;
                break;
            case CellState.Circle:
                cellImage.sprite = circlePrefab;
                break;

        }
    }

    public CellState GetState()
    {
        return state;
    }

    public bool IsEmpty()
    {
        return state == CellState.Empty;
    }
}
