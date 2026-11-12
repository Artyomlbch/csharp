namespace AircraftIndustry.Collections;

// делегат - условие сравнения двух аппаратов
// возвращает отрицательное число, если first должен стоять раньше second,
// 0 - если они равны, положительное число - если first должен стоять позже
public delegate int AircraftComparison<T>(T first, T second);
