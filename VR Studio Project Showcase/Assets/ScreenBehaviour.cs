using System.Collections;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(VideoPlayer))]
public class ScreenBehaviour : MonoBehaviour {

    public Texture2D Thumbnail;
    public float TimeToVideo = 5.0f;
    public float TimeBackToThumbnail = 5.0f;

    private bool _isGazedAt;
    private float _elapsedSeconds;
    private Renderer _renderer;
    private VideoPlayer _videoPlayer;

    void Start()
    {
        _isGazedAt = false;
        _renderer = GetComponent<Renderer>();
        _videoPlayer = GetComponent<VideoPlayer>();
        _renderer.material.mainTexture = Thumbnail;
    }

    private IEnumerator PlayVideo()
    {
        if (_videoPlayer.isPlaying)
        {
            yield return null;
        }

        _videoPlayer.Prepare();

        //Wait until video is prepared
        while (!_videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            yield return null;
        }

        Debug.Log("Done Preparing Video");

        _renderer.material.mainTexture = _videoPlayer.texture;

        _videoPlayer.Play();
    }

    private void StopVideo()
    {
        if (_videoPlayer.isPlaying)
        {
            _renderer.material.mainTexture = Thumbnail;
            _videoPlayer.Stop();
        }
    }

    // Update is called once per frame
    void Update () {       

        if (_isGazedAt && _elapsedSeconds >= TimeToVideo)
        {
            StartCoroutine(PlayVideo());
            _elapsedSeconds = 0;
        }

        if (!_isGazedAt && _elapsedSeconds >= TimeBackToThumbnail)
        {
            StopVideo();
            _elapsedSeconds = 0;
        }

        _elapsedSeconds += Time.deltaTime;
	}

    public void SetGazedAt(bool gazedAt)
    {
        _isGazedAt = gazedAt;
        _elapsedSeconds = 0;
    }
}
