#if UNITY_ANY_INSTANCING_ENABLED
    StructuredBuffer<float4> _InstanceColorBuffer_Sphere;
    uint _InstanceIDOffset_Sphere;

    void GetInstanceColor_float(out float4 color)
    {
        color = _InstanceColorBuffer_Sphere
    [_InstanceIDOffset_Sphere + unity_InstanceID];
    }
#else

StructuredBuffer<float4> _InstanceColorBuffer_Sphere;
uint _InstanceIDOffset_Sphere;

void GetInstanceColor_float(out float4 color)
{
    color = float4(1, 1, 1, 1);
}
#endif