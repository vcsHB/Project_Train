using System;
using UnityEngine;
namespace Project_Train.DataManage.CoreDataBaseSystem
{
    [Flags]
    public enum AttackAreaType
    {
        Ground = 1 << 0,
        Air = 1 << 1
    }
    public class FunctionDetailSO : DataDetailSO
    {
        public override DataDetailType DetailType => DataDetailType.Function;
        public float range;
        [Range(0f, 50f)] public float randomizeErrorAngle;
        public float fireCooltime;
        public AttackAreaType attackArea;



    }
}