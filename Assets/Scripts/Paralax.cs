using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Transform[] backgrounds; // Array of all the backgrounds to be parallaxed
    public float[] parallaxScales; // The proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f; // How smooth the parallax effect should be. Make sure to set this above 0.

    private Vector3 previousPlayerPosition; // The position of the player in the previous frame

    void Start()
    {
        previousPlayerPosition = player.position;
    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // The parallax is the opposite of the camera movement because the previous frame multiplied by the scale
            float parallax = (previousPlayerPosition.y - player.position.y) * parallaxScales[i];

            // Set a target y position which is the current position plus the parallax
            float backgroundTargetPosY = backgrounds[i].position.y + parallax;

            

            // Create a target position which is the background's current position with its target x position
            Vector3 backgroundTargetPosition = new Vector3(backgrounds[i].position.x, backgroundTargetPosY, backgrounds[i].position.z);

            // Fade between current position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPosition, smoothing * Time.deltaTime);
        }

        // Set the previousPlayerPosition to the player's position at the end of the frame
        previousPlayerPosition = player.position;
    }
}
