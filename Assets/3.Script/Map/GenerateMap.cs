using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour {
    [SerializeField] private AnimationCurve x_animationCurve;
    private DrawMap drawMap;

    public int mapWidth, mapHeight, octaves, seed;
    public float noiseScale, persistance, lacunarity;
    public TerrainType[] regions;
    public Vector2 offset;

    public float[] noiseWave;
    public TileBase[,] noiseTileMap;

    private void Awake() {
        drawMap = GetComponent<DrawMap>();
        GenerateNoiseTileMap();
        GenerateNoiseTileWave();
    }

    private void Update() {
        // For Debugging Noise Wave. Not performance.
        float[] noiseWave = GenerateNoise.GenerateNoiseWave(mapWidth, seed, octaves);
        Keyframe[] keyframes = new Keyframe[noiseWave.Length];
        for (int i = 0; i < noiseWave.Length; i++)
            keyframes[i] = new Keyframe(i, noiseWave[i] * 15f);
        x_animationCurve = new AnimationCurve(keyframes);
        for (float t = 0; t < noiseWave.Length; t += 0.1f) {
            x_animationCurve.Evaluate(t);
        }
    }


    public void GenerateNoiseTileWave() {
        noiseWave = GenerateNoise.GenerateNoiseWave(mapWidth, seed, octaves);
    }

    public void GenerateNoiseTileMap() {
        float[,] noiseMap = GenerateNoise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        noiseTileMap = new TileBase[mapWidth, mapHeight];
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++) {
                    if (currentHeight <= regions[i].height) {
                        noiseTileMap[x, y] = regions[i].tile;
                        break;
                    }
                }
            }
        }
    }

    [System.Serializable]
    public struct TerrainType {
        public string name;
        public float height;
        public TileBase tile;
    }
}