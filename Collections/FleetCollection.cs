using System.Collections;
using AircraftIndustry.Domain;
using AircraftIndustry.Interfaces;

namespace AircraftIndustry.Collections;

// собственная обобщённая коллекция летательных аппаратов
// ограничение where T : Aircraft: в коллекции можно хранить только летательные аппараты,
// например FleetCollection<string> не скомпилируется
public class FleetCollection<T> : IEnumerable<T>, IReadOnlyFleet<T> where T : Aircraft
{
    // внутренний массив элементов, увеличивается вдвое при заполнении
    private T[] _items = new T[4];

    // количество занятых ячеек массива
    private int _count;

    // количество аппаратов
    public int Count => _count;

    // аппарат по индексу
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return _items[index];
        }
    }

    // добавляет аппарат, при нехватке места увеличивает массив вдвое
    public void Add(T item)
    {
        if (_count == _items.Length)
        {
            Array.Resize(ref _items, _items.Length * 2);
        }

        _items[_count] = item;
        _count++;
    }

    // удаляет аппарат, возвращает false, если его не было в коллекции
    public bool Remove(T item)
    {
        int index = Array.IndexOf(_items, item, 0, _count);

        if (index < 0)
        {
            return false;
        }

        // сдвигаем следующие элементы на одну позицию влево
        for (int i = index; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }

        _count--;

        // очищаем освободившуюся ячейку
        _items[_count] = default!;
        return true;
    }

    // сортирует коллекцию пузырьком по условию, которое передаётся извне делегатом
    public void Sort(AircraftComparison<T> comparison)
    {
        for (int i = 0; i < _count - 1; i++)
        {
            for (int j = 0; j < _count - 1 - i; j++)
            {
                // если соседние аппараты стоят в неправильном порядке - меняем их местами
                if (comparison(_items[j], _items[j + 1]) > 0)
                {
                    T temp = _items[j];
                    _items[j] = _items[j + 1];
                    _items[j + 1] = temp;
                }
            }
        }
    }

    // возвращает аппарат с наибольшим значением признака,
    // признак вычисляет функция selector, переданная извне
    public T? FindMax(Func<T, double> selector)
    {
        if (_count == 0)
        {
            return null;
        }

        T max = _items[0];

        for (int i = 1; i < _count; i++)
        {
            if (selector(_items[i]) > selector(max))
            {
                max = _items[i];
            }
        }

        return max;
    }

    // выполняет действие action для каждого аппарата
    public void ForEach(Action<T> action)
    {
        for (int i = 0; i < _count; i++)
        {
            action(_items[i]);
        }
    }

    // реализация IEnumerable<T>: позволяет обходить коллекцию в foreach и использовать LINQ
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _items[i];
        }
    }

    // необобщённая версия, которую требует IEnumerable
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
