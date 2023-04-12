using StatSystem.Enum;

namespace StatSystem
{
    public interface IStatValueGiver
    {
        float GetStatValue(StatType statType);
    }
}