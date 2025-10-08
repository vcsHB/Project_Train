using UnityEngine;

namespace Project_Train.RailSystem
{
    public interface ICar
    {
        public Transform transform { get; } // 인터페이스를 상속받은 클래스의 부모에서 이미해당 클래스를 구현했다면 해당 함수 선언은 구현된 것이다.
        public float CurrentSpeed { get; }
    }
}