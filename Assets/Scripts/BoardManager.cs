using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; private set; }

    [Header("----- Board Settings -----")]
    [SerializeField] private int width = 15;
    [SerializeField] private int height = 15;

    [Space(10)]

    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private RectTransform winLinePrefab;
    [SerializeField] private Transform winLineHolder;
    [SerializeField] private Transform boardTransform;

    private Cell[,] cells;

    private CellState currentState = CellState.Cross;
    public bool isPausedGame = false;
    public bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        cells = new Cell[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject obj = Instantiate(cellPrefab, boardTransform);
                Cell cell = obj.GetComponent<Cell>();
                cell.Init(x, y, this);
                cells[x, y] = cell;
            }
        }
    }

    public void OnCellClicked(int x, int y)
    {
        if (isPausedGame || isGameOver) return;

        Cell cell = cells[x, y];
        if (!cell.IsEmpty()) return;

        cell.SetState(currentState);
        SoundManager.Instance.PlaySound(SoundManager.Instance.click);

        // Check win here
        if (CheckWin(x, y, currentState))
        {
            isGameOver = true;
        }


        currentState = currentState == CellState.Cross ? CellState.Circle : CellState.Cross;
    }

    private bool CheckWin(int x, int y, CellState state)
    {
        var directions = new Vector2Int[]
        {
            new Vector2Int(1, 0), // Horizontal
            new Vector2Int(0, 1), // Vertical
            new Vector2Int(1, 1), // Diagonal /
            new Vector2Int(1, -1) // Diagonal \
        };

        foreach (var dir in directions)
        {
            int count = 1;
            Vector2Int start = new Vector2Int(x, y);
            Vector2Int end = new Vector2Int(x, y);

            CountDirection(x, y, dir, state, ref count, ref end);
            CountDirection(x, y, -dir, state, ref count, ref start);

            if (count >= 5)
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.win);
                DrawLine(start, end);
                return true;
            }
        }

        return false;
    }

    private void CountDirection(int startX, int startY, Vector2Int dir, CellState state, ref int count, ref Vector2Int bound)
    {
        int x = startX + dir.x;
        int y = startY + dir.y;

        while (x >= 0 && x < width && y >= 0 && y < height && cells[x, y].GetState() == state)
        {
            count++;
            bound = new Vector2Int(x, y);
            x += dir.x;
            y += dir.y;
        }
    }

    private void DrawLine(Vector2Int start, Vector2Int end)
    {
        int startX = start.y * 69 + 32;
        int startY = -(start.x * 69 + 32);

        int endX = end.y * 69 + 32;
        int endY = -(end.x * 69 + 32);

        Vector2 midPoint = new Vector2((startX + endX) / 2f, (startY + endY) / 2f);

        var winLine = Instantiate(winLinePrefab, winLineHolder);
        winLine.anchoredPosition = new Vector2(midPoint.x - 510, midPoint.y + 510);

        Vector2 dir = new Vector2(endX - startX, endY - startY);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        winLine.rotation = Quaternion.Euler(0, 0, angle);
    }
}
