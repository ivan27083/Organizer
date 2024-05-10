using System;
using System.Collections.Generic;
using System.Text;
using Xamarin_test.Models;

namespace Xamarin_test.Classes
{
    public class Node<abstract_Item> // объявление обобщенного класса
    {
        public abstract_Item data { get; private set; }
        public Node<abstract_Item> parent { get; private set; }
        public List<Node<abstract_Item>> children { get; private set; }
        public Circle circle { get; private set; }
        public int? children_group;
        public int? parent_group;
        public Node(abstract_Item data)
        {
            this.data = data;
            this.children = new List<Node<abstract_Item>>();
            if (data is Purpose purpose)
            {
                children_group = purpose.Children;
                parent_group = purpose.Parent;
            }
            else if (data is Mission mission)
            {
                parent_group = mission.Parent;
                children_group = null;
            }
            else children_group = parent_group = null;
        }
        public override string ToString()
        {
            return data.ToString();
        }
        public void AddChild(Node<abstract_Item> data)
        {
            data.parent = this;
            this.children.Add(data); // добавление элемента в конец List<T>
        }
        public void AddAllChildren(List<Node<abstract_Item>> children)
        {
            foreach (Node<abstract_Item> child in children)
                child.parent = this;
            this.children.AddRange(children);
        }
        public void Insert(abstract_Item new_data)
        {
            int? group;
            if (data is Purpose purpose)
            {
                group = purpose.Group;
            }
            else if (data is Mission mission)
            {
                group = mission.Group;
            }
            else group = null;

            if (data == null) data = new_data;
            else if (children_group != null && children_group == group)
            {

            }
        }
        public bool IsLeaf()
        {
            return this.children == null || this.children.Count == 0;
        }
        public List<abstract_Item> PreOrder()
        {
            List<abstract_Item> list = new List<abstract_Item>();
            list.Add(data);
            foreach (Node<abstract_Item> child in children)
                list.AddRange(child.PreOrder());
            return list;
        }
        public List<abstract_Item> PostOrder()
        {
            List<abstract_Item> list = new List<abstract_Item>();
            foreach (Node<abstract_Item> child in children)
                list.AddRange(child.PreOrder());
            list.Add(data);
            return list;
        }

        public int Depth()
        {
            int deepest = 0;
            foreach (Node<abstract_Item> child in children)
            {
                int depth = 1 + child.Depth();
                if (deepest < depth)
                    deepest = depth;
            }
            return deepest;
        }
    }
}
