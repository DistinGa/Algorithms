using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Graph
    {
        public int[,] LinksMatrix;

        public GraphNode DeepWalk()
        {
            return DeepWalk((int[,])LinksMatrix.Clone());
        }

        public void WideWalk()
        {
            WideWalk((int[,])LinksMatrix.Clone());
        }

        /// <summary>
        /// Рекурсивный обход графа в глубину.
        /// </summary>
        /// <returns></returns>
        private GraphNode DeepWalk(int[,] cloneLinksMatrix, int row = 0)
        {
            if (cloneLinksMatrix.GetLength(0) != cloneLinksMatrix.GetLength(1))
                throw new Exception("Incorrect links matrix had passed.");

            GraphNode walkTree = new GraphNode(row);

            for (int i = 0; i < cloneLinksMatrix.GetLength(1); i++)
            {
                if (cloneLinksMatrix[row, i] != 0)
                {
                    cloneLinksMatrix[row, i] = 0;
                    walkTree.Children.Add(DeepWalk(cloneLinksMatrix, i));
                }
            }

            return walkTree;
        }

        /// <summary>
        /// Обход графа в ширину.
        /// </summary>
        /// <returns></returns>
        private void WideWalk(int[,] linksMatrix)
        {
            if (linksMatrix.GetLength(0) != linksMatrix.GetLength(1))
                throw new Exception("Incorrect links matrix had passed.");

            // Словарь пройденных вершин.
            Dictionary<int, bool> markedVertexes = new Dictionary<int, bool>();
            for (int i = 0; i < linksMatrix.GetLength(0); i++)
            {
                markedVertexes[i] = false;
            }

            // Фронт волны (содержит индексы вершин).
            List<int> Vertexes = new List<int>();
            Vertexes.Add(0);

            while (Vertexes.Count > 0)
            {
                // Если вершина не обработана,
                if (!markedVertexes[Vertexes[0]])
                {
                    // загружаем во фронт все смежные ей вершины,
                    for (int i = 0; i < linksMatrix.GetLength(1); i++)
                    {
                        // если они не обработаны.
                        if (linksMatrix[Vertexes[0], i] != 0 && !markedVertexes[i])
                        {
                            Vertexes.Add(i);
                        }
                    }

                    // Выполняем необходимые действия с вершиной.
                    // ...............

                    // А саму вершину помечаем обработанной.
                    markedVertexes[Vertexes[0]] = true;
                }

                Vertexes.RemoveAt(0);
            }
        }
    }

    // Узел сильноветвистого дерева.
    public class GraphNode
    {
        public int Data;
        public List<GraphNode> Children;

        public GraphNode(int data = 0)
        {
            Data = data;
            Children = new List<GraphNode>();
        }

        public override string ToString()
        {
            string res = "";

            for (int i = 0; i < Children.Count; i++)
            {
                res += res == ""? "": ", " + Children[i].ToString();
            }

            if (!string.IsNullOrEmpty(res))
                res = $"{Data.ToString()}({res})";
            else
                res = Data.ToString();

            return res;
        }
    }
}
