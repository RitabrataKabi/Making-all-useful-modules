using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	[SerializeField] private Texture2D mapTex;
	[SerializeField] private ColorToPrefab[] toPrefabs;

	public float offsetXMultiplier, offsetYMultiplier;
	public Vector2 mapOffset = new Vector2(-50f, 0f);

	public int pixelCount;

	public Transform[] objectSeperators;

	/// <summary>
	/// Generates the map by iterating over each pixel of the map texture. 
	/// Creates corresponding game objects based on the color-to-prefab mapping 
	/// and organizes them into predefined categories such as Miscellaneous, 
	/// Checkpoints, Collectables, Stages, Obstacles, and Stationary.
	/// </summary>

	public void GenerateMap()
	{
		objectSeperators = new Transform[]
		{
			new GameObject("Miscellaneous Objects").transform,
			new GameObject("Checkpoints").transform,
			new GameObject("Collectables").transform,
			new GameObject("Stages").transform,
			new GameObject("Obstacles").transform,
			new GameObject("Stationary").transform

		};

		objectSeperators[0].parent = transform;
		objectSeperators[1].parent = transform;
		objectSeperators[2].parent = transform;
		objectSeperators[3].parent = transform;
		objectSeperators[4].parent = transform;
		objectSeperators[5].parent = transform;

		for (int x = 0; x < mapTex.width; x++)
		{
			for (int y = 0; y < mapTex.height; y++)
			{
				CreateMap(x, y);
			}
		}
	}

	void CreateMap(int x, int y)
	{
		Color pixelColor = mapTex.GetPixel(x, y);

		if (pixelColor.a == 0)
		{
			return;
		}

		foreach (ColorToPrefab prefabColor in toPrefabs)
		{
			if (prefabColor.color == pixelColor)
			{
				//Vector2 randomness = new Vector2(Random.Range(prefabColor.randomOffsetXMinAmnt, prefabColor.randomOffsetXMaxAmnt), Random.Range(prefabColor.randomOffsetYMinAmnt, prefabColor.randomOffsetYMaxAmnt)) * prefabColor.randomOffsetMultiplier;

				Vector2 position = new Vector2(x * offsetXMultiplier, y * offsetYMultiplier) + prefabColor.offset + mapOffset;

				GameObject newObject = Instantiate(prefabColor.prefab, position, Quaternion.identity, transform);

				newObject.transform.localScale = (newObject.transform.localScale + (Vector3)prefabColor.size) * prefabColor.scaleMultiplier;


				switch (prefabColor.type)
				{
					case SpecialType.Default:
						newObject.transform.parent = objectSeperators[0].transform;
						break;
					case SpecialType.Checkpoint:
						newObject.transform.parent = objectSeperators[1].transform;
						break;
					case SpecialType.Stages:
						newObject.transform.parent = objectSeperators[3].transform;
						break;
					case SpecialType.Collectables:
						newObject.transform.parent = objectSeperators[2].transform;
						break;
					case SpecialType.Obstacles:
						newObject.transform.parent = objectSeperators[4].transform;
						break;
					case SpecialType.Stationary:
						newObject.transform.parent = objectSeperators[5].transform;
						break;
					default:
						break;
				}
			}
		}
	}

	public void Reset()
	{
		for (int i = transform.childCount - 1; i > -1; i--)
		{
			DestroyImmediate(transform.GetChild(i).gameObject);
		}
	}
}
