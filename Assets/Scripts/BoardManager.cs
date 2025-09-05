using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("----- Board Settings -----")]
    [SerializeField] private int width = 15;
    [SerializeField] private int height = 15;

    [Space(10)]

    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform boardTransform;

    private Cell[,] cells;

    private void Start()
    {
        //GenerateBoard();
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
                cell.Init(x, y);
                cells[x, y] = cell;
            }
        }
    }
}
