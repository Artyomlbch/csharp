namespace AircraftIndustry.Interfaces;

// парк техники, доступный только для чтения
// ковариантный интерфейс (out T): T только возвращается из его членов, поэтому
// IReadOnlyFleet<CargoAirplane> можно использовать как IReadOnlyFleet<Aircraft>
public interface IReadOnlyFleet<out T> : IEnumerable<T>
{
    // количество аппаратов
    int Count { get; }

    // аппарат по индексу
    T this[int index] { get; }
}
