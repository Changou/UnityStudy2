using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmotionParticle : MonoBehaviour
{
    [Header("ƒ⁄ ¿Œµ¶Ω∫"), SerializeField] int[] _noseIndex;
    [Header("±Õ ¿Œµ¶Ω∫"), SerializeField] int[] _earIndex;
    [Header("¥´ ¿Œµ¶Ω∫"), SerializeField] int[] _eyesIndex;
    [Header("¿Ã∏∂ ¿Œµ¶Ω∫"), SerializeField] int[] _foreheadIndex;

    public List<int[]> _emotions = new List<int[]>();

    public int _IndexCnt
    {
        get
        {
            int cnt = 0;
            for(int i = 0; i < _emotions.Count; i++)
            {
                for(int j = 0; j < _emotions[i].Length; j++)
                {
                    cnt++;
                }
            }
            return cnt;
        }
    }

    public List<GameObject> _particleOBJ = new List<GameObject>(); 

    private void Awake()
    {
        _emotions.Add(_noseIndex.Concat(_earIndex).ToArray());
        _emotions.Add(_eyesIndex);
        _emotions.Add(_foreheadIndex);
    }

    public void AddParticleObj(GameObject gObj)
    {
        _particleOBJ.Add(gObj);
        gObj.SetActive(false);
    }

    public void EmotionParticleOn(EMOTION emotion)
    {
        int num = 0;
        for (int j = 1; j <= (int)emotion; j++)
        {
            for (int k = 0; k < _emotions[(int)emotion - j].Length; k++) { ++num; }
        }

        for(int i = 0; i < _emotions[(int)emotion].Length; i++)
        {
            _particleOBJ[num + i].SetActive(!_particleOBJ[num + i].activeSelf);
            _particleOBJ[num + i].transform.GetChild((int)emotion).gameObject.SetActive(true);

            if((emotion == EMOTION.ANGRY) && (i >= _noseIndex.Length))
            {
                _particleOBJ[i].transform.GetComponentInChildren<FlameDirectionControl>().SetDir(i - 1);
            }
        }
    }
}
