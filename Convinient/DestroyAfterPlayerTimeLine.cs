using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DestroyAfterPlayerTimeLine : MonoBehaviour
{
    PlayableDirector playableDirector;

    private void Awake()
    {
        // Get the PlayableDirector component attached to this GameObject
        playableDirector = GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        // Subscribe to the stopped event
        playableDirector.stopped += OnPlayableDirectorStopped;
    }

    private void OnDisable()
    {
        // Unsubscribe from the stopped event
        playableDirector.stopped -= OnPlayableDirectorStopped;
    }

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        // Check if the director that stopped is the one we are interested in
        if (director == playableDirector)
        {
            // Destroy the GameObject this script is attached to
            Destroy(gameObject);
        }
    }
}
