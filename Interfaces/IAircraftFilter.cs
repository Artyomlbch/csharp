namespace AircraftIndustry.Interfaces;

// условие отбора летательных аппаратов
// контравариантный интерфейс (in T): T только передаётся в его методы, поэтому
// IAircraftFilter<Aircraft> можно использовать как IAircraftFilter<CargoAirplane>
public interface IAircraftFilter<in T>
{
    // проверяет, подходит ли аппарат под условие
    bool IsMatch(T aircraft);
}
