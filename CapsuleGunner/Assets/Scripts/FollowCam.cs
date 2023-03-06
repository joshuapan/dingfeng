using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 跟随目标平移的摄像机
public class FollowCam : MonoBehaviour
{
    // 在编辑器里指定摄像机的追踪目标
    public Transform target;

    // 从摄像机位置到目标的向量
    Vector3 offset;

    void Start()
    {
        // 一开始，将摄像机与目标位置之间的偏移（差异）向量保存下来
        offset = transform.position - target.position;
    }

    // 这里使用LateUpdate比Update更好，因为可以确保在目标移动之后，摄像机再移动
    void LateUpdate()
    {
        // 将摄像机位置更新为，从目标位置加上偏移得到的新位置
        transform.position = target.position + offset;
    }
}
