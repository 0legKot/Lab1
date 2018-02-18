using System;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;

namespace Lab1
{
    // TODO: Create class with Dictionary, Visual list of items, List Of Selectors DONE
    class MyDictionary<V, T> : Dictionary<string, T> where V : IComparable, ICloneable, IConvertible, IComparable<String>, IEnumerable<char>, IEquatable<String>
    {
         Dictionary<string, T> CurrentDictionary { get; set; }
         List<Selector> Dependencies { get; set; }
        //int key=0;
        public MyDictionary()
        {
            CurrentDictionary = new Dictionary<string, T>();
            Dependencies = new List<Selector>();
        }
        public void AddElement(T element,string name)
        {
            try
            {
                CurrentDictionary.Add(name, element);
                foreach (Selector dependency in Dependencies)
                    if (!dependency.Items.Contains(name))
                    {
                        dependency.Items.Add(name);
                       // dependency.Items.Refresh();
                    }
            }
            catch { throw new Exception($"Element {name} already exists"); }
        }
        public void RemoveElement(string name)
        {
            try
            {
                CurrentDictionary.Remove(name);
                foreach (Selector dependency in Dependencies)
                {
                    if (dependency.Items.Contains(name)) dependency.Items.Remove(name);
                    //dependency.Items.Refresh();
                }
            }
            catch { throw new Exception($"Element {name} doesn't exist"); }
        }
        public void EditElement(T element, string name)
        {
            CurrentDictionary[name]=element;
        }

        public T GetElement(string name, string err="")
        {
            try
            {
                return CurrentDictionary[name];
            }
            catch { throw new Exception($"No {err} selected"); }
        }
        public void AddDependency(Selector selector)
        {
            Dependencies.Add(selector);
        }
        public void RemoveDependency() {
            throw new NotImplementedException();
        }

    }
}
