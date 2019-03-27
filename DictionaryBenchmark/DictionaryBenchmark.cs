using System;
using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using Microsoft.Collections.Extensions;

namespace DictionaryBenchmark
{
    /// <summary>
    /// https://github.com/dotnet/corefxlab/tree/master/src/Microsoft.Experimental.Collections
    /// </summary>
    public class DictionaryBenchmark
    {
        static string[] data = { "aaa", "bbb", "ccc", "ddd", "eee", "sdf", "fds", "asd", "cvb" };
        static string value = "ddd";

        Dictionary<string, bool> dictionary;
        DictionarySlim<string, bool> dictionarySlim;
        HashSet<string> hashset;
        object v = 10;
        object[] array = new object[10];

        [GlobalSetup]
        public void Setup()
        {
            dictionary = new Dictionary<string, bool>(data.Length);
            foreach (var item in data) dictionary.Add(item, true);

            dictionarySlim = new DictionarySlim<string, bool>(data.Length);
            foreach (var item in data) dictionarySlim.GetOrAddValueRef(item) = true;

            hashset = new HashSet<string>();
            foreach (var item in data) hashset.Add(item);

            array[8] = 444;
        }

        [Benchmark(Baseline = true)]
        public void DictionaryRead() => dictionary.TryGetValue(value, out bool r);

        [Benchmark]
        public void DictionaryContainsKey() => dictionary.ContainsKey(value);

        [Benchmark]
        public void HashSetContains() => hashset.Contains(value);

        [Benchmark]
        public void DictionarySlimRead() => dictionarySlim.TryGetValue(value, out bool r);

        [Benchmark]
        public void DictionarySlimContainsKey() => dictionarySlim.ContainsKey(value);

        //[Benchmark]
        //public object Boxing1()
        //{
        //    object o = 5;
        //    return o;
        //}

        //[Benchmark]
        //public int UnBoxing1()
        //{
        //    return (int)v;
        //}

        //public void Save2Array<T>(T value)
        //{
        //    array[5] = value;
        //}

        //public T ReadFromArray<T>()
        //{
        //    return (T)array[8];
        //}

        //[Benchmark]
        //public void Boxing2() => Save2Array(123);

        //[Benchmark]
        //public int UnBoxing2() => ReadFromArray<int>();

        //[Benchmark]
        //public object ObjectAllocation() => new object();
    }
}
