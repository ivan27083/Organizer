using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_test.Classes
{
    public class Node<T> // объявление обобщенного класса
    {
        public T data { get; private set; }
        public Node<T> parent { get; private set; }
        public List<Node<T>> children { get; private set; } // создается лист детей типа Node<T>
        public Node(T data)
        {
            this.data = data;
            this.children = new List<Node<T>>();
        }
        public override string ToString()
        {
            return data.ToString();
        }
        public void AddChild(Node<T> data)
        {
            data.parent = this;
            this.children.Add(data); // добавление элемента в конец List<T>
        }
        public void AddAllChildren(List<Node<T>> children)
        {
            foreach (Node<T> child in children)
                child.parent = this;
            this.children.AddRange(children);
        }
        public bool IsLeaf()
        {
            return this.children == null || this.children.Count == 0;
        }
        public List<T> PreOrder()
        {
            List<T> list = new List<T>();
            list.Add(data);
            foreach (Node<T> child in children)
                list.AddRange(child.PreOrder());
            return list;
        }
        public List<T> PostOrder()
        {
            List<T> list = new List<T>();
            foreach (Node<T> child in children)
                list.AddRange(child.PreOrder());
            list.Add(data);
            return list;
        }

        public int Depth()
        {
            int deepest = 0;
            foreach (Node<T> child in children)
            {
                int depth = 1 + child.Depth();
                if (deepest < depth)
                    deepest = depth;
            }
            return deepest;
        }
    }
}
