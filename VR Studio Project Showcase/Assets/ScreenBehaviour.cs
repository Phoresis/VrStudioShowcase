using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ScreenBehaviour : MonoBehaviour {

    public Texture2D Thumbnail;
    public float TimeToVideo = 5.0f;
    public float TimeBackToThumbnail = 5.0f;

    private bool _isGazedAt;
    private float _elapsedSeconds;

    void Start()
    {
        _isGazedAt = false;
        GetComponent<Renderer>().material.mainTexture = Thumbnail;
    }

    // Update is called once per frame
    void Update () {
		
        if (_elapsedSeconds >= TimeToVideo)
        {
            GetComponent<Renderer>().material.mainTexture = null;
        }

        if (_isGazedAt)
        {
            _elapsedSeconds += Time.deltaTime;
        }
	}

    public void SetGazedAt(bool gazedAt)
    {
        _isGazedAt = gazedAt;

        if (!gazedAt)
        {
            _elapsedSeconds = 0;
        }
    }
}
