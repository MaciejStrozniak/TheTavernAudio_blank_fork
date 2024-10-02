using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FMOD_Commands : MonoBehaviour
{
    #region EVENT EMITTER
    // EVENT EMITTER
    public FMODUnity.StudioEventEmitter tavernEmitter; // �cie�ka do event emittera na scenie
    #endregion

    #region EVENT
    // EVENT
    FMOD.Studio.EventInstance FootstepsSound; // klasa eventu / snapshotu
    public EventReference footstepsEvent; // �cie�ka dost�pu do eventu / snapshotu

    private void Footsteps()
    {
        // jednorazowe odtworzenie
        FMODUnity.RuntimeManager.PlayOneShot(footstepsEvent); // stworzenie klasy snapshotu ze �cie�k� do wybranego snapshotu i jednorazowe odtworzenie

        // podstawowe zarz�dzanie eventem
        FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent); // podanie klasie eventu �cie�ki do wybranego eventu / snapshotu
        FootstepsSound.setParameterByNameWithLabel("Footsteps_surface", "Stone"); // zmiana warto�ci parametru zadeklarowanego lub wykorzystanego w evencie
        FootstepsSound.start(); // odtworzenie eventu
        FootstepsSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // STOP bez fadeout
        FootstepsSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // STOP z fadeout
        FootstepsSound.release(); // zwolnienie pami�ci

        // zarz�dzanie eventem z przypi�ciem emittera do gameObjectu 
        FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
        FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform)); // !!!!11! przypi�cie emittera eventu do gameObjectu !!!11!!
        FootstepsSound.setParameterByNameWithLabel("Footsteps_surface", "Stone");
        FootstepsSound.start();
        FootstepsSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        FootstepsSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FootstepsSound.release();
    }
    #endregion

    #region SNAPSHOT
    // SNAPSHOT
    FMOD.Studio.EventInstance HealthSnap; // klasa eventu / snapshotu
    public EventReference healthSnapshot; // �cie�ka dost�pu do eventu / snapshotu

    private void StartSnapshot()
    {
        if (tavernEmitter != null && tavernEmitter.IsPlaying()) // sprawdzenie czy event emitter istnieje na scenie i czy jest aktywny
        {
            HealthSnap = FMODUnity.RuntimeManager.CreateInstance(healthSnapshot); // podanie klasie snapshotu �cie�ki do wybranego eventu / snapshotu
            HealthSnap.start(); // w��czenie snapshotu
        }
        else if (tavernEmitter != null && tavernEmitter.IsPlaying())
        {
            HealthSnap.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // STOP bez fadeout
            HealthSnap.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // STOP z fadeout
            HealthSnap.release(); // zwolnienie pami�ci
        }
    }
    #endregion

    #region VCA
    // VCA
    FMOD.Studio.VCA GlobalVCA; // klasa VCA

    private void VCA()
    {
        GlobalVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Mute"); // podanie klasie VCA �cie�ki do wybranego eventu / snapshotu
        GlobalVCA.setVolume(DecibelToLinear(0)); // ustawienie maksymalnej g�o�no�ci z przeliczeniem na decybele [dB]
        GlobalVCA.setVolume(DecibelToLinear(-100)); // obni�enie g�o�no�ci z przeliczeniem na decybele [dB]
    }

    private float DecibelToLinear(float dB) // dodatkowa funkacja spoza FMOD przeliczaj�ca zwyk�e warto�ci liczbowe na decyble [dB]
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }
    #endregion

    #region EVENT / EMITTER Z MUZYK�
    // EVENT / EMITTER Z MUZYK�
    FMOD.Studio.EventInstance Music; // klasa eventu / snapshotu
    public FMODUnity.StudioEventEmitter tavernEmitter_Music; // �cie�ka do event emittera na scenie

    private void MusicSwtich()
    {
        // EVENT
        FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent); // podanie klasie eventu �cie�ki do wybranego eventu / snapshotu
        Music.setParameterByNameWithLabel("Switch_parts", "Part 2"); // zmiana warto�ci parametr�w typu labeled dla event�w
        Music.start(); // odtworzenie eventu
        Music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // STOP bez fadeout
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // STOP z fadeout
        Music.release(); // zwolnienie pami�ci


        // EMITTER
        tavernEmitter_Music.SetParameter("Switch_parts", 0); // zmiana warto�ci parametr�w typu labeled dla event emitter�w
        tavernEmitter_Music.Play(); // w��czenie odtwarzania na emitterze
        tavernEmitter_Music.Stop(); // wy��czenie odtwarzania na emitterze
    }
    #endregion
}
