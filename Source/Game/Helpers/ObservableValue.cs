using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game;

public interface IObservable<T>
{
    public event Action<T> OnChange;
    public T Value { get; set; }
}

public class Observable<T> : IObservable<T> where T : struct
{

    private T value;




    public T Value
    {
        get => value;
        set
        {
            if (this.value.Equals(value))
            {
                return;
            }
            this.value = value;
            OnChange?.Invoke(value);

        }
    }

    public event Action<T> OnChange;
}