﻿using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace UnityToolkit
{
    public class ConcurrentPool<T>
    {
        readonly ConcurrentBag<T> objects = new ConcurrentBag<T>();

        readonly Func<T> objectGenerator;

        public ConcurrentPool(Func<T> objectGenerator, int initialCapacity)
        {
            this.objectGenerator = objectGenerator;

            // allocate an initial pool so we have fewer (if any)
            // allocations in the first few frames (or seconds).
            for (int i = 0; i < initialCapacity; ++i)
                objects.Add(objectGenerator());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get() => objects.TryTake(out T obj) ? obj : objectGenerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Return(T item) => objects.Add(item);

        public int Count => objects.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Release(T item) => objects.Add(item);
    }
}