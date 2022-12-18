using UnityEngine;

public class NeverBelowTerrain : MonoBehaviour
{
    // The maximum distance to check for the terrain using a raycast.
    private readonly float maxDistance = 100;

    // The amount of time to wait before destroying this script.
    private readonly float destroyDelay = 3f;

    // A reference to the nearest terrain component.
    private Terrain nearestTerrain;

    private void Awake()
    {
        // Create an array to store the results of the OverlapSphere call
        Collider[] hits = Physics.OverlapSphere(transform.position, maxDistance);

        // Set the initial nearestTerrain variable to null
        nearestTerrain = null;

        // Iterate through the array of colliders
        foreach (Collider collider in hits)
        {
            // Get the Terrain component of the collider
            Terrain terrain = collider.GetComponent<Terrain>();

            // Make sure the collider has a Terrain component
            if (terrain != null)
            {
                // Set the nearestTerrain variable to the hit terrain
                nearestTerrain = terrain;
            }
        }

        Destroy(gameObject, destroyDelay);
    }

    private void Update()
    {
        // Get the position of the player.
        Vector3 playerPos = transform.position;

        // Calculate the height of the terrain at the player's position.
        float terrainHeight = nearestTerrain.SampleHeight(playerPos);

        // Compare the height of the terrain to the player's height.
        if (playerPos.y < terrainHeight)
        {
            // If the distance between the player and the terrain is greater than a small threshold, push the player up.
            transform.position = new Vector3(transform.position.x, terrainHeight, transform.position.z);
        }
    }
}
