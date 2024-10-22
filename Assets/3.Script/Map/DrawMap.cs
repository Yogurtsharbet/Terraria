using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[System.Serializable]
public class TileArray {
    public string tileTag;
    public TileBase[] Tile;
}

public class DrawMap : MonoBehaviour {
    public Tilemap tileMap;
    [SerializeField] private TileBase[] waveTiles;
    [SerializeField] private TileArray[] grassTiles;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator playerAnimator;

    private HashSet<Vector2Int> generatedChunks = new HashSet<Vector2Int>();
    private GenerateMap generateMap;
    private float[] noiseWave;
    private TileBase[,] noiseTileMap;

    private float ChunkXOffset = 100, ChunkYOffset = 100;
    private float offsetX = 0, offsetY = 160;

    private void Awake() {
        TryGetComponent(out generateMap);
        noiseWave = generateMap.noiseWave;
        noiseTileMap = generateMap.noiseTileMap;
    }
    private void Start() {
        Vector2Int chunkCoord = new Vector2Int(); // = GetCurrentChunkCorrd(playerTransform.position);

        /*
        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++) {
                Vector2Int surroundChunk = chunkCoord + new Vector2Int(x, y);
                if (!generatedChunks.Contains(surroundChunk)) {
                    generatedChunks.Add(surroundChunk);
                    drawTileWave(surroundChunk);
                    drawTileMap(surroundChunk);
                }
            }
        */

        drawTileMap(chunkCoord);
        drawTileWave(chunkCoord);
        GenerateGrassTile(Vector3Int.zero);
    }

    private void FixedUpdate() {
        GenerateGrassTile(Vector3Int.FloorToInt(playerTransform.position));
    }
    /*
    private void FixedUpdate() {
        if (playerAnimator.GetBool("isMove")) {
            GenerateGrassTile(Vector3Int.FloorToInt(playerTransform.position));
            Vector2Int chunkCoord = GetCurrentChunkCorrd(playerTransform.position);

            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++) {
                    Vector2Int surroundChunk = chunkCoord + new Vector2Int(x, y);
                    if (!generatedChunks.Contains(surroundChunk)) {
                        generatedChunks.Add(surroundChunk);
                        drawTileWave(surroundChunk);
                        drawTileMap(surroundChunk);
                    }
                }
        }
    }
    */

    /*
    public Vector2Int GetCurrentChunkCorrd(Vector3 position) {
        int ChunkX = Mathf.FloorToInt((position.x - offsetX) / ChunkXOffset);
        int ChunkY = Mathf.FloorToInt((position.y - offsetY) / ChunkYOffset);
        return new Vector2Int(ChunkX, ChunkY);
    }
    */

    public void GenerateGrassTile(Vector3Int offset) {
        BoundsInt bounds;
        if (offset.Equals(Vector3Int.zero)) bounds = tileMap.cellBounds;
        else {
            Vector3Int size = new Vector3Int(30, 10, 1);
            Vector3Int center = new Vector3Int(
                offset.x - size.x / 2, offset.y - size.y / 2, offset.z - size.z / 2);
            bounds = new BoundsInt(center, size);
        }

        for (int x = bounds.xMin; x < bounds.xMax; x++) {
            for (int y = bounds.yMin; y < bounds.yMax; y++) {
                for (int z = bounds.zMin; z < bounds.zMax; z++) {
                    Vector3Int pos = new Vector3Int(x, y, z);
                    TileBase tile = tileMap.GetTile(pos);

                    if (tile != null) {
                        if (tile.name[6].Equals('0') && pos.y > 80)
                            //if (tileMap.GetTile(pos + Vector3Int.down) != null &&
                                //tileMap.GetTile(pos + Vector3Int.down + Vector3Int.left) != null &&
                                //tileMap.GetTile(pos + Vector3Int.down + Vector3Int.right) != null)          // 아래 좌 중 우 다 있을때
                                if (tileMap.GetTile(pos + Vector3Int.up) == null) {     // 위 없음
                                    if (tileMap.GetTile(pos + Vector3Int.right) == null && tileMap.GetTile(pos + Vector3Int.left) != null) {          // 좌 있음
                                        tileMap.SetTile(pos, grassTiles[0].Tile[Random.Range(0, grassTiles[0].Tile.Length)]);
                                    }
                                    else if (tileMap.GetTile(pos + Vector3Int.right) != null && tileMap.GetTile(pos + Vector3Int.left) == null) {    // 우 있음
                                        tileMap.SetTile(pos, grassTiles[2].Tile[Random.Range(0, grassTiles[2].Tile.Length)]);
                                    }
                                    else if (tileMap.GetTile(pos + Vector3Int.right) != null && tileMap.GetTile(pos + Vector3Int.left) != null) {    // 좌 우 있음
                                        tileMap.SetTile(pos, grassTiles[1].Tile[Random.Range(0, grassTiles[1].Tile.Length)]);
                                    }
                                    else if (tileMap.GetTile(pos + Vector3Int.right) == null && tileMap.GetTile(pos + Vector3Int.left) == null) {  // 좌 우 없음
                                        tileMap.SetTile(pos, grassTiles[3].Tile[Random.Range(0, grassTiles[3].Tile.Length)]);
                                    }
                                }
                                else if (tileMap.GetTile(pos + Vector3Int.up) != null) {    // 위 있음
                                    if (tileMap.GetTile(pos + Vector3Int.right) != null && tileMap.GetTile(pos + Vector3Int.left) != null) {    // 좌 우 있음
                                        if (tileMap.GetTile(pos + Vector3Int.up + Vector3Int.right) == null && tileMap.GetTile(pos + Vector3Int.up + Vector3Int.left) == null)  // 좌상 우상 없음
                                            tileMap.SetTile(pos, grassTiles[4].Tile[Random.Range(0, grassTiles[4].Tile.Length)]);
                                    }
                                }
                    }
                }
            }
        }
    }

    public void drawTileWave(Vector2Int chunk) {
        System.Random random = new System.Random();
        int minX = (int)Mathf.Clamp(noiseWave.Length / 2 - (chunk.x * 2 + 1) * ChunkXOffset, 0, noiseWave.Length);
        int maxX = (int)Mathf.Clamp(noiseWave.Length / 2 + (chunk.x * 2 - 1) * ChunkXOffset, 0, noiseWave.Length);

        for (int i = 0; i < noiseWave.Length; i++) {
            int currentHeight = (int)(noiseWave[i] * 7f);
            if (currentHeight > 2) {
                for (int j = 0; j < currentHeight; j++)
                    tileMap.SetTile(new Vector3Int(i - noiseWave.Length / 2, j + 150, 0), waveTiles[random.Next(waveTiles.Length)]);
            }
            else {
                for (int j = -1; j > currentHeight; j--)
                    tileMap.SetTile(new Vector3Int(i - noiseWave.Length / 2, j + 150 - currentHeight, 0), null);
            }
        }
    }
    public void drawTileMap(Vector2Int chunk) {
        int width = noiseTileMap.GetLength(0);
        int height = noiseTileMap.GetLength(1);

        /*
        int minX = (int)Mathf.Clamp(width / 2 + (chunk.x * 2 - 1) * ChunkXOffset, 0, width);
        int maxX = (int)Mathf.Clamp(width / 2 + (chunk.x * 2 + 1) * ChunkXOffset, 0, width);
        int minY = (int)Mathf.Clamp(height / 2 + (chunk.y * 2 - 1) * ChunkYOffset, 0, height);
        int maxY = (int)Mathf.Clamp(height / 2 + (chunk.y * 2 + 1) * ChunkYOffset, 0, height);
        */


        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++)
                tileMap.SetTile(new Vector3Int(x - width / 2, 150 - y, 0), noiseTileMap[x, y]);
        }
    }
}