using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private Transform _visuals = null;

    [SerializeField] private float _flipTime = 1f;

    public void SnapToRotation(float angle) 
    {
        TweenKiller();
        _visuals.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void TriggerFlip(bool flipRight, bool fromRooted) 
    {
        //DebugFlip();
        //return;
        TweenKiller();
        Vector3 flipRotation;
        if (flipRight)
        {
            if (fromRooted)
            {
                flipRotation = _visuals.rotation.eulerAngles + new Vector3(0f,0f, -270f);
                _visuals.DORotate(flipRotation, _flipTime, RotateMode.FastBeyond360).SetEase(Ease.OutCubic).SetId(this);
            }
            else
            {
                flipRotation = _visuals.rotation.eulerAngles + new Vector3(0f, 0f, -360f);
                _visuals.DORotate(flipRotation, _flipTime, RotateMode.FastBeyond360).SetEase(Ease.OutCubic).SetId(this);
            }
        }
        else
        {
            if (fromRooted)
            {
                flipRotation = _visuals.rotation.eulerAngles + new Vector3(0f, 0f, 270f);
                _visuals.DORotate(flipRotation, _flipTime, RotateMode.FastBeyond360).SetEase(Ease.OutCubic).SetId(this);
            }
            else
            {
                flipRotation = _visuals.rotation.eulerAngles + new Vector3(0f, 0f, 360f);
                _visuals.DORotate(flipRotation, _flipTime, RotateMode.FastBeyond360).SetEase(Ease.OutCubic).SetId(this);
            }
        }
    }

    private void TweenKiller()
    {
        DOTween.Kill(this);
    }

    private void OnDestroy()
    {
        TweenKiller();
    }
}

