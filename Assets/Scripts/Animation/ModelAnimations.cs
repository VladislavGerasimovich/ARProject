using UnityEngine;

public class ModelAnimations : MonoBehaviour
{
    private Animator _animator;
    private AnimationClip[] _clips;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        if(_animator.runtimeAnimatorController != null)
        {
            _clips = _animator.runtimeAnimatorController.animationClips;
        }
    }

    public int GetCount()
    {
        int count = 0;

        if(_clips == null)
        {
            return count;
        }

        return _clips.Length;
    }

    public void SetAnimation(int index)
    {
        _animator.Play(_clips[index].name);
    }

    public string GetNameOfAnimation(int index)
    {
        return _clips[index].name;
    }
}