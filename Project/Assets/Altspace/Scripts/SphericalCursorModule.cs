using UnityEngine;

public class SphericalCursorModule : MonoBehaviour {
	public GameObject[] fish;
	public AnimationClip[] clips;

	// This is a sensitivity parameter that should adjust how sensitive the mouse control is.
	public float Sensitivity;

	// This is a scale factor that determines how much to scale down the cursor based on its collision distance.
	public float DistanceScaleFactor;
	
	// This is the layer mask to use when performing the ray cast for the objects.
	// The furniture in the room is in layer 8, everything else is not.
	private const int ColliderMask = (1 << 8);

	// This is the Cursor game object. Your job is to update its transform on each frame.
	private GameObject Cursor;

	// This is the Cursor mesh. (The sphere.)
	private MeshRenderer CursorMeshRenderer;

	// This is the scale to set the cursor to if no ray hit is found.
	// Updated the value to be higher, since sphere was too small. Might be result of math-error
	private Vector3 DefaultCursorScale = new Vector3(500.0f, 500.0f, 500.0f);

	// Maximum distance to ray cast.
	private const float MaxDistance = 100.0f;

	// Sphere radius to project cursor onto if no raycast hit.
	private const float SphereRadius = 1000.0f;

	// ENHANCEMENT: Keeps track of default cursor color to enable changing back
	private Color default_color;


	// Boolean to ensure we scare the fish only once
	private bool is_swimming = true;

	void Awake() {
		Cursor = transform.Find("Cursor").gameObject;
		CursorMeshRenderer = Cursor.transform.GetComponentInChildren<MeshRenderer>();
        CursorMeshRenderer.renderer.material.color = new Color(0.0f, 0.8f, 1.0f);
		default_color = CursorMeshRenderer.material.GetColor ("_Color");
	}	

	// ENHANCEMENT: Handles 'tapping' the aquarium, which scares all the fish away.
	// Is there a way to access animations directly from the component?
	void HandleAquarium() {
		if (!is_swimming) return; //Can only scare fish once

		CursorMeshRenderer.material.SetColor ("_Color", Color.red);
		if(Input.GetMouseButtonDown(0)) {
			for(int i = 0; i < clips.Length; i ++) {
				fish[i].animation.clip = clips[i];
				fish[i].animation.Play();
			}
			is_swimming = false;
		}
	}
	
	void Update()
	{
		CursorMeshRenderer.material.SetColor ("_Color", default_color);

		// Perform raycast and set cursorHit's parameters
		Ray fwd_ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		var cursorHit = new RaycastHit();
		Physics.Raycast (fwd_ray, out cursorHit, MaxDistance, ColliderMask);


		// If raycast collides, use provided algorithm to update object's transform
		if (cursorHit.collider != null)
		{
			if(cursorHit.collider.gameObject.name == "Aquarium") HandleAquarium();

			Selectable.CurrentSelection = cursorHit.collider.gameObject;
	
			Cursor.transform.position = cursorHit.point;
			float factor = (cursorHit.distance * DistanceScaleFactor + 1.0f) / 2.0f;
			Cursor.transform.localScale = new Vector3(factor, factor, factor);
		}
		// If raycast doesn't collide, calculate intersection between ray and 'virtual' sphere
		else
		{
			Selectable.CurrentSelection = null;

			Cursor.transform.localScale = DefaultCursorScale;
			Vector3 intersection_point = fwd_ray.origin + SphereRadius * fwd_ray.direction;
			Cursor.transform.position = intersection_point;
		}
	}
}
