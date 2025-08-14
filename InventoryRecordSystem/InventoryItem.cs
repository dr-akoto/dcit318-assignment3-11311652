using System;

namespace FinanceSystem.InventoryRecordSystem
{
    public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity;
}
