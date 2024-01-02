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

public class Observable<T> : IObservable<T>
{

    private T value;




    public T Value
    {
        get => value;
        set
        {
            if ((this.value is null && value is not null) || (this.value is not null && !this.value.Equals(value)))
            {
                //Debug.Log(".");
                this.value = value;
                OnChange?.Invoke(value);
            }


        }
    }

    public Observable() { }
    public Observable(T startWith)
    {
        value = startWith;
    }

    public event Action<T> OnChange;
}