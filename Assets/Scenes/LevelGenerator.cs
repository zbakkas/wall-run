
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public platfforrm[] chunckprefabs,platStarte;
    Vector3 chunkpositionn;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        chunkpositionn = transform.position;
        for (int i = 0; i < 3; i++)
        {
            platfforrm chunktocreate = platStarte[Random.Range(0, platStarte.Length)];

            if (i > 0)
                chunkpositionn.z += chunktocreate.Gettenght() / 2;
            platfforrm chunkinstance = Instantiate(chunktocreate, chunkpositionn, Quaternion.identity, transform);
            chunkpositionn.z += chunkinstance.Gettenght() / 2;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z>= (chunkpositionn.z-60))
        {
            for (int i = 0; i < 3; i++)
            {
                platfforrm chunktocreate = chunckprefabs[Random.Range(0, chunckprefabs.Length)];

                
                    chunkpositionn.z += chunktocreate.Gettenght() / 2;
                platfforrm chunkinstance = Instantiate(chunktocreate, chunkpositionn, Quaternion.identity, transform);
                chunkpositionn.z += chunkinstance.Gettenght() / 2;

            }
        }
    }
}