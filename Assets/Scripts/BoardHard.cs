using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class BoardHard : MonoBehaviour {
    public Tilemap tilemap {get; private set; }
    public PieceHard activePiece { get; private set; }
    public TetrominoData[] tetrominoes;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10, 20);

    // Add reference to preview Tilemap
    public Tilemap previewTilemap;
    private TetrominoData nextPieceData;
    private int level = 0;
    private int linesCleared = 0;

    public RectInt Bounds {
        get 
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    public void Awake() {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<PieceHard>();

        for (int i = 0; i < this.tetrominoes.Length; i++) {
            this.tetrominoes[i].Initialize();
        }

        // Initialize the next piece
        SetNextPiece();
    }

    private void Start() {
        SpawnPiece();
    }

    private void SetNextPiece() {
        int random = Random.Range(0, this.tetrominoes.Length);
        nextPieceData = this.tetrominoes[random];
        UpdatePreview();
    }

    public void SpawnPiece() {
        // Use the next piece data for the current piece
        TetrominoData data = nextPieceData;

        // Initialize the next piece immediately
        SetNextPiece();

        this.activePiece.Initialize(this, this.spawnPosition, data);

        if (IsValidPosition(this.activePiece, this.spawnPosition)) {
            Set(this.activePiece);
        } else {
            GameOver();
        }
    }

    private void UpdatePreview() {
        previewTilemap.ClearAllTiles();

        // Set the preview piece position and rotation
        Vector3Int previewPosition = new Vector3Int(0, 0, 0);
        foreach (Vector2Int cell in nextPieceData.cells) {
            Vector3Int tilePosition = (Vector3Int)cell + previewPosition;
            previewTilemap.SetTile(tilePosition, nextPieceData.tile);
        }
    }

    private void GameOver() {
        PlayerPrefs.SetInt("GameOver", 1); // Set the parameter to indicate game over
        SceneManager.LoadScene("Menu"); // Load the Menu scene
        this.tilemap.ClearAllTiles();
    }

    public void Set(PieceHard piece) {
        for (int i = 0; i < piece.cells.Length; i++) {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    public void Clear(PieceHard piece) {
        for (int i = 0; i < piece.cells.Length; i++) {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(PieceHard piece, Vector3Int position) {
        RectInt bounds = this.Bounds;

        for (int i = 0; i < piece.cells.Length; i++) {
            Vector3Int tilePosition = piece.cells[i] + position;

            if (!bounds.Contains((Vector2Int)tilePosition)) {
                return false;
            }
            
            if (this.tilemap.HasTile(tilePosition)) {
                return false;
            }
        }

        return true;
    }

    public void ClearLines() {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;

        while (row < bounds.yMax) {
            if (IsLineFull(row)) {
                LineClear(row);
                linesCleared++;
                if (linesCleared % 10 == 0) {
                    level++;
                }
            } else {
                row++;
            }
        }
    }

    private bool IsLineFull(int row) {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++) {
            Vector3Int position = new Vector3Int(col, row, 0);

            if (!this.tilemap.HasTile(position)) {
                return false;
            }
        }

        return true;
    }

    private void LineClear(int row) {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++) {
            Vector3Int position = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(position, null);
        }

        while (row < bounds.yMax) {
            for (int col = bounds.xMin; col < bounds.xMax; col++) {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = this.tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, above);
            }
            row++;
        }
    }

    public int GetLevel() {
        return level;
    }
}
