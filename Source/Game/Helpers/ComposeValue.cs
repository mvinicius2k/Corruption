using System;
using System.Collections.Generic;
using DStruct.Heaps;
using DStruct.Queues;
using FlaxEngine;

namespace Game;

public struct FunctionNode<T> : IComparable<FunctionNode<T>>
{
    public int Order;
    public Func<T, T> Function;

    public int CompareTo(FunctionNode<T> other)
    {
        return Order.CompareTo(other.Order);
    }
}


/// <summary>
/// PipedValue Script.
/// </summary>
public class ComposeValue<T> where T : struct
{
    [ShowInEditor, ReadOnly]
    private T baseValue;
    [ShowInEditor, ReadOnly]
    private T value;

    [HideInEditor]
    public T BaseValue 
    {
        get => baseValue;
        set
        {
            baseValue = value;
            outdated = true;
        }
    }
    public T Value
    {
        get
        {
            if (outdated)
            {
                functions.Sort();
                var final = baseValue;
                
                for (int i = 0; i < functions.Count; i++)
                {
                    final = functions[i].Function.Invoke(final);
                }
                value = final;
                outdated = false;
            }
            return value;

            
        }
    }
    private bool outdated;

    
    private List<FunctionNode<T>> functions = new();
    public List<FunctionNode<T>> Functions
    {
        get
        {
            outdated = true;
            return functions;
        }
    }

    



}
