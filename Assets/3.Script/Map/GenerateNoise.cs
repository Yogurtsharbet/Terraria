using UnityEngine;
public static class GenerateNoise {
    public static float[] GenerateNoiseWave(int mapWidth, int seed = 1, int octaves = 2, float persistance = 1f, float lacunarity = 30f, float scale = 15f) {
        float[] noiseWave = new float[mapWidth];
        float[] octaveOffset = new float[octaves];
        System.Random rand = new System.Random(seed);
        for (int i = 0; i < octaves; i++) {
            float offsetX = rand.Next(-100000, 100000);
            octaveOffset[i] = offsetX;
        }
        if (scale <= 0) scale = 0.0001f;
        float maxNoise = float.MinValue;
        float minNoise = float.MaxValue;

        for (int x = 0; x < mapWidth; x++) {
            float noiseHeight = 0;
            float amplitude = 1;
            float frequency = 1;

            for (int i = 0; i < octaves; i++) {
                float posX = x / scale + octaveOffset[i];
                float perlinValue = Mathf.PerlinNoise(posX, 1) * 2 - 1;

                noiseHeight += perlinValue * amplitude;
                amplitude *= persistance;
                frequency *= lacunarity;
            }
            if (noiseHeight > maxNoise) maxNoise = noiseHeight;
            else if (noiseHeight < minNoise) minNoise = noiseHeight;
            noiseWave[x] = noiseHeight;
        }
        for (int x = 0; x < mapWidth; x++) {
            noiseWave[x] = Mathf.InverseLerp(minNoise, maxNoise, noiseWave[x]);
        }
        return noiseWave;
    }

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
        float[,] noiseMap = new float[mapWidth, mapHeight];
        System.Random rand = new System.Random(seed);
        Vector2[] octaveOffset = new Vector2[octaves];
        for(int i = 0; i < octaves; i++) {
            float offsetX = rand.Next(-100000, 100000) + offset.x;
            float offsetY = rand.Next(-100000, 100000) + offset.y;
            octaveOffset[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0) scale = 0.0001f;
        float maxNoise = float.MinValue;
        float minNoise = float.MaxValue;


        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {

                float noiseHeight = 0;
                float amplitude = 1;
                float frequency = 1;

                for (int i = 0; i < octaves; i++) {
                    float posY = y / scale * frequency + octaveOffset[i].x;
                    float posX = x / scale * frequency + octaveOffset[i].y;
                    float perlinValue = Mathf.PerlinNoise(posX, posY) * 2 - 1;

                    noiseHeight += perlinValue * amplitude;
                    amplitude *= persistance;
                    frequency *= lacunarity;

                }
                if (noiseHeight > maxNoise) maxNoise = noiseHeight;
                else if (noiseHeight < minNoise) minNoise = noiseHeight;
                noiseMap[x, y] = noiseHeight;
            }
        }
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                noiseMap[x, y] = Mathf.InverseLerp(minNoise, maxNoise, noiseMap[x, y]);
            }
        }
        return noiseMap;
    }
}
