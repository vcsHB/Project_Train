using System;
namespace Project_Train.ResourceSystem
{
    [Serializable]
    public class ResourceEntry
    {
        public int amount;
        public event Action<int> OnValueChangedEvent;


        public void SetAmount(int newAmount)
        {
            this.amount = newAmount;
            InvokeResourceValueChanged();
        }
        public bool IsEnough(int amount) => this.amount >= amount;

        public void Add(int amount)
        {
            if (amount == 0) return;
            this.amount += amount;
            InvokeResourceValueChanged();
        }

        public bool TrySubtract(int amount)
        {
            if (amount == 0) return true;
            if (this.amount < amount) return false;

            this.amount -= amount;
            InvokeResourceValueChanged();
            return true;
        }

        public void ClearEvent()
        {
            OnValueChangedEvent = null;
        }

        private void InvokeResourceValueChanged()
        {
            OnValueChangedEvent?.Invoke(amount);
        }
    }
}