using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioSource clickAudio, wrongGoalShapeAudio, spikeHitAudio, mainGameBackgroundMusic, playerMovementAudio, winAudio;

    // Start is called before the first frame update
    void Start()
    {
        var audioSources = GetComponents<AudioSource>();
        clickAudio = audioSources[0];
        wrongGoalShapeAudio = audioSources[1];
        spikeHitAudio = audioSources[2];
        mainGameBackgroundMusic = audioSources[3];
        playerMovementAudio = audioSources[4];
        winAudio = audioSources[5];
    }
}
