using System.Collections.Generic;
using UnityEngine;

public class ParticleAnimationTrigger : MonoBehaviour
{
    public List<ParticleSystem> fxList = new List<ParticleSystem>();

    public void PlayFX(int fxIndex)
    {
        if (fxIndex >= 0 && fxIndex < fxList.Count)
        {
            if (fxList[fxIndex] != null)
            {
                fxList[fxIndex].Play();
            }
        }
        else
        {
            Debug.LogWarning("Could not find this FxIndex: " + fxIndex);
        }
    }

    public void StopFX(int fxIndex)
    {
        if (fxIndex >= 0 && fxIndex < fxList.Count)
        {
            if (fxList[fxIndex] != null)
            {
                fxList[fxIndex].Stop();
            }
        }
    }
}