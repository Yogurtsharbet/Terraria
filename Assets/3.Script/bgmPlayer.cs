using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmPlayer : MonoBehaviour {
    [SerializeField] AudioClip bgmClip;
    private AudioSource bgmAudio;

    private void Awake() {
        TryGetComponent(out bgmAudio);
    }

    void Start() {
        bgmAudio.clip = bgmClip;
        bgmAudio.Play();
    }
}