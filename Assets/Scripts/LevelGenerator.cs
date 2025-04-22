using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    [Header("Prefabs")]
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject ceilingPrefab;
    [SerializeField] private GameObject pickup;

    [Header("Player Reference")]
    [SerializeField] private Player characterController;

    [Header("Parent Reference")]
    [SerializeField] private GameObject floorParent;
    [SerializeField] private GameObject wallsParent;

    [Header("Settings")]
    [SerializeField] private bool generateRoof = true;
    [SerializeField] private int tilesToRemove = 50;
    [SerializeField] private int floorTilesToRemove = 0;
    [SerializeField] private int mazeSize = 10;

	private bool characterPlaced = false;
	private bool[,] mapData;
	private int mazeX;
	private int mazeY;
	private int floorTilesConsumed;
	private bool generateFloorTile;

	void Start () {
		mapData = GenerateMazeData();
        floorTilesConsumed = 0;

        for (int z = 0; z < mazeSize; z++) {
			for (int x = 0; x < mazeSize; x++) {
				generateFloorTile = true;

                if (mapData[z, x]) {
					CreateChildPrefab(wallPrefab, wallsParent, x, 1, z);
					CreateChildPrefab(wallPrefab, wallsParent, x, 2, z);
					CreateChildPrefab(wallPrefab, wallsParent, x, 3, z);
				}
				else
				{
					if (!characterPlaced)
                    {
                        characterController.transform.SetPositionAndRotation(new Vector3(x, 1.5f, z), Quaternion.identity);
                        characterPlaced = true;
                    }
					else
					{
						SetGenerateFloorTile(x, z);
                    }
                }

				if (generateFloorTile)
				{
					CreateChildPrefab(floorPrefab, floorParent, x, 0, z);
				}

				if (generateRoof) {
					CreateChildPrefab(ceilingPrefab, wallsParent, x, 4, z);
				}
			}
		}

		var myPickup = Instantiate(pickup, new Vector3(mazeX, 1, mazeY), Quaternion.identity);
		myPickup.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
	}

	bool[,] GenerateMazeData() {
		bool[,] data = new bool[mazeSize, mazeSize];
		for (int y = 0; y < mazeSize; y++) {
			for (int x = 0; x < mazeSize; x++) {
				data[y, x] = true;
			}
		}

		int tilesConsumed = 0;
		while (tilesConsumed < tilesToRemove) {
			int xDirection = 0;
			int yDirection = 0;
			if (Random.value < 0.5) {
				xDirection = Random.value < 0.5f ? 1 : -1;
			} else {
				yDirection = Random.value < 0.5f ? 1 : -1;
			}

			int numSpacesMove = Random.Range(1, mazeSize - 1);
			for (int i = 0; i < numSpacesMove; i++) {
				mazeX = Mathf.Clamp(mazeX + xDirection, 1, mazeSize - 2);
				mazeY = Mathf.Clamp(mazeY + yDirection, 1, mazeSize - 2);
				if (data[mazeY, mazeX]) {
					data[mazeY, mazeX] = false;
					tilesConsumed++;
				}
			}
		}

		return data;
	}

	void CreateChildPrefab(GameObject prefab, GameObject parent, int x, int y, int z) {
		var myPrefab = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
		myPrefab.transform.parent = parent.transform;
	}

	void SetGenerateFloorTile(int x, int z)
	{
        if (((x > 3 && x != mazeX) && (z > 3 && z != mazeY)) && floorTilesConsumed < floorTilesToRemove && Random.value < 0.5f)
        {
            generateFloorTile = false;
            floorTilesConsumed++;
        }
    }
}
