using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    BGM,
    Effect,    
    Effect1,    
    Effect2,    
    Effect3,    
    Effect4,    
    Effect5,    
}
public class SoundManager : Singleton<SoundManager>
{
    AudioSource[] _audioSources = new AudioSource[7];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();    
    public void Init()
    {
        string[] soundNames = System.Enum.GetNames(typeof(SoundType));
        for(int i=0; i<soundNames.Length;i++)
        {
            GameObject go = new GameObject(soundNames[i]);
            _audioSources[i] = go.AddComponent<AudioSource>();
            go.transform.parent = transform;
        }        
        _audioSources[(int)SoundType.BGM].loop = true;

        for(int i=0;i<6;i++)
        {
            _audioSources[(int)SoundType.Effect+i].spatialBlend = 1;
            _audioSources[(int)SoundType.Effect+i].maxDistance = 35;
            _audioSources[(int)SoundType.Effect+i].rolloffMode = AudioRolloffMode.Linear;
        }
    }    
    public void PlayEffect(string _path,Vector3 point,float pitch=1.0f)
    {
        if (Vector3.Distance(point, Camera.main.transform.position) > 40.0f)
            return;

        var audioClip = GetOrAddAudioClip(_path, SoundType.Effect);
        AudioSource source=null;                        

        for(int i=0;i<6;++i)
        {
            if (_audioSources[(int)SoundType.Effect + i].isPlaying == false)
                source = _audioSources[(int)SoundType.Effect + i];
        }

        if (source == null )
            return;        

        source.pitch = pitch;        
        source.PlayOneShot(audioClip);
        source.gameObject.transform.position = point;        
    }    
    
    public void Play(AudioClip audio,SoundType type,float pitch=1.0f)
    {
        if (audio == null)
            return;
        if(type== SoundType.BGM)
        {
            AudioSource audioSource = _audioSources[(int)SoundType.BGM];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audio;
            audioSource.Play();
        }
        else if(type== SoundType.Effect)
        {
            var source = _audioSources[(int)SoundType.Effect];
            source.pitch = pitch;
            source.PlayOneShot(audio);
        }
    }
    public void Play(string _path,SoundType type,float pitch=1.0f)
    {
        var audioClip = ResUtil.Load<AudioClip>(_path);
        Play(audioClip, type, pitch);
    }
    AudioClip GetOrAddAudioClip(string _path,SoundType type)
    {
        if (_path.Contains("Sound/") == false)
            _path = $"Sound/{_path}";

        AudioClip audioClip = null;

        if(type==SoundType.BGM)
        {
            audioClip = ResUtil.Load<AudioClip>(_path);
        }
        else if(type==SoundType.Effect)
        {
            if(_audioClips.TryGetValue(_path,out audioClip)==false)
            {
                audioClip = ResUtil.Load<AudioClip>(_path);
                _audioClips.Add(_path, audioClip);
            }
        }
        if (audioClip == null)
            return null;
        return audioClip;
    }
}
