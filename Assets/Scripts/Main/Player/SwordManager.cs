using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

    }
    public void CreateSword(int num)
    {

    }

    public void ReturnPool(GameObject sword, bool hit)
    {
        // hit이 true면 적중한거라 재사용 대기시간
        if (hit)
        {

        }
        // false면 타겟이 사라진거라 그냥 자식으로 풀에 추가후 위치 변경만
        else
        {

        }
    }
}
