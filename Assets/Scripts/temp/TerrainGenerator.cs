using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TerrainGenerator  : MonoBehaviour {
    public int gridSize = 50;  // Размер сетки (меньше = грубее)
    public float scale = 20f; // Масштаб шума
    public float heightMultiplier = 10f; // Максимальная высота неровностей
    public Vector2 mountainCenter = new Vector2(0.5f, 0.5f); // Центр горы (в координатах 0-1)
    public float mountainRadius = 0.2f; // Радиус зоны для горы

    void Start() {
        GenerateTerrain();
    }

    void GenerateTerrain() {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];
        int[] triangles = new int[gridSize * gridSize * 6];

        // Генерация вершин
        for (int z = 0, i = 0; z <= gridSize; z++) {
            for (int x = 0; x <= gridSize; x++, i++) {
                float xCoord = (float)x / gridSize;
                float zCoord = (float)z / gridSize;

                // Основной шум для ровной поверхности
                float baseHeight = Mathf.PerlinNoise(xCoord * scale, zCoord * scale) * 0.5f;

                // Дополнительная гора в определенной зоне
                float distanceToCenter = Vector2.Distance(new Vector2(xCoord, zCoord), mountainCenter);
                float mountainHeight = Mathf.Max(0, 1 - (distanceToCenter / mountainRadius)) * heightMultiplier;

                vertices[i] = new Vector3(x, baseHeight * heightMultiplier + mountainHeight, z);
            }
        }

        // Генерация треугольников
        for (int z = 0, t = 0, v = 0; z < gridSize; z++, v++) {
            for (int x = 0; x < gridSize; x++, t += 6, v++) {
                triangles[t] = v;
                triangles[t + 1] = v + gridSize + 1;
                triangles[t + 2] = v + 1;

                triangles[t + 3] = v + 1;
                triangles[t + 4] = v + gridSize + 1;
                triangles[t + 5] = v + gridSize + 2;
            }
        }

        // Применение данных к Mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        // Присвоение Mesh объекту
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
