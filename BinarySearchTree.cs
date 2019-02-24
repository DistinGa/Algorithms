using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    static class BinarySearchTree
    {
        /// <summary>
        /// Рекурсивное создание сбалансированного бинарного дерева поиска из сортированного массива.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="midIndex"></param>
        /// <param name="leftIndex"></param>
        /// <param name="rightIndex"></param>
        /// <returns></returns>
        public static TreeNode CreateBalansedTree(int[] array, int midIndex, int leftIndex, int rightIndex)
        {
            if (leftIndex > rightIndex)
                return null;    // Дошли до края интервала, узлов дальше нет.

            TreeNode _root = new TreeNode(array[midIndex], CreateBalansedTree(array, (midIndex + leftIndex - 1) / 2, leftIndex, midIndex - 1), CreateBalansedTree(array, (midIndex + rightIndex + 1) / 2, midIndex + 1, rightIndex));

            return _root;
        }

        /// <summary>
        /// Рекурсивный поиск по дереву.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TreeNode Find(TreeNode root, int data)
        {
            if (root == null)
                return null;

            if (root.Data == data)
                return root;
            else if (data < root.Data)
                return Find(root.LeftNode, data);
            else
                return Find(root.RightNode, data);
        }

    }

    /// <summary>
    /// Узел дерева.
    /// </summary>
    public class TreeNode
    {
        public int Data;
        public TreeNode LeftNode, RightNode, Parent;

        public TreeNode(int data, TreeNode leftNode, TreeNode rightNode, TreeNode parent = null)
        {
            Data = data;
            LeftNode = leftNode;
            RightNode = rightNode;
            Parent = parent;

            if(LeftNode != null)
                LeftNode.Parent = this;
            if(RightNode != null)
                RightNode.Parent = this;
        }

        /// <summary>
        /// КЛП
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TreeNode> PreOrder()
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(this);

            TreeNode tn;
            while (stack.Count > 0)
            {
                tn = stack.Pop();
                yield return tn;

                if (tn.RightNode != null)
                {
                    stack.Push(tn.RightNode);
                }

                if (tn.LeftNode != null)
                {
                    stack.Push(tn.LeftNode);
                }
            }
        }

        /// <summary>
        /// ЛКП
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TreeNode> InOrder()
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode tn = this;

            stack.Push(tn);
            while (tn.LeftNode != null)
            {
                tn = tn.LeftNode;
                stack.Push(tn);
            }

            while (stack.Count > 0)
            {
                tn = stack.Pop();
                // В стеке элементы, для которых левое крыло уже обработано (либо без левого).
                yield return tn;

                if (tn.RightNode != null)
                {
                    tn = tn.RightNode;
                    stack.Push(tn);
                    while (tn.LeftNode != null)
                    {
                        tn = tn.LeftNode;
                        stack.Push(tn);
                    }
                }
            }
        }

        /// <summary>
        /// ЛПК
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TreeNode> PostOrder()
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode tn, prevnode;

            tn = this;
            prevnode = this;
            stack.Push(tn);

            while (stack.Count > 0)
            {
                tn = stack.Pop();

                if (tn.RightNode == prevnode || tn.RightNode == null)
                {
                    yield return tn;
                }
                else
                {
                   stack.Push(tn);

                   if (tn.RightNode != null)
                    {
                        stack.Push(tn.RightNode);
                    }

                    if (tn.LeftNode != null)
                    {
                        stack.Push(tn.LeftNode);
                    }
                }

                prevnode = tn;
            }
        }

        public override string ToString()
        {
            string res;

            res = Data.ToString();
            if (LeftNode != null || RightNode != null)
            {
                res += $"({LeftNode?.ToString() ?? "<null>"}, {RightNode?.ToString() ?? "<null>"})";
            }

            return res;
        }
    }
}
