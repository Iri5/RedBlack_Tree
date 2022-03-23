using System;

namespace Red_BlackTree
{
    public class Node
    {
        public Color color = Color.Black;
        public Node left;
        public Node right;
        public Node parent;
        public int data;
        public bool isLeaf = false;
        public Node(bool isLeafM) { this.isLeaf = isLeafM;}
        public Node(int data) { this.data = data;}
    }
    public class RB
    {
        public Node root;
        public Node GetRoot()
        {
            return this.root;
        }
        public RB() { }
        private void LeftRotate(Node X)
        {
            Node Y = X.right; // set Y
            X.right = Y.left;//turn Y's left subtree into X's right subtree
            if (Y.left != null)
            {
                Y.left.parent = X;
            }
            Y.parent = X.parent;
            if (X.parent == null)
            {
                this.root = Y;
            }
            else
            {
                if (X == X.parent.left)
                {
                    X.parent.left = Y;
                }
                else
                {
                    X.parent.right = Y;
                }
            }
            Y.left = X; //put X on Y's left
            X.parent = Y;
        }
        private void RightRotate(Node X)
        {
            Node Y = X.left;
            X.left = Y.right;//turn Y's left subtree into X's right subtree
            if (Y.right != null)
            {
                Y.right.parent = X;
            }
            Y.parent = X.parent;

            if (X.parent == null)
            {
                this.root = Y;
            }
            else
            {
                if (X == X.parent.right)
                {
                    X.parent.right = Y;
                }
                else
                {
                    X.parent.left = Y;
                }
            }
            Y.right = X; //put X on Y's left
            X.parent = Y;
        }
        public void Insert(int value)
        {
            Node newNode = new Node(value);
            if (root == null)
            {
                root = newNode;
                root.color = Color.Black;
                root.left = new Node(true);
                root.right = new Node(true);
                return;
            }
            Node node1 = root;
            Node node2 = new Node(true);
            while (node1.isLeaf == false)
            {
                node2 = node1;
                if (newNode.data < node1.data)
                {
                    node1 = node1.left;
                }
                else 
                { 
                    node1 = node1.right; 
                }
                if (node1 == null)
                {
                    break;
                }
            }
            newNode.parent = node2;
            if ((node2.isLeaf) || (node2 == null))
            {
                root = newNode;
            }
            else if (newNode.data < node2.data)
            {
                node2.left = newNode;
            }
            else
            {
                node2.right = newNode;
            }
            newNode.left = new Node(true);
            newNode.right = new Node(true);
            newNode.color = Color.Red;
            InsertFixUp(newNode);
        }
        private void InsertFixUp(Node item)
        {
            while (item != root && item.parent.color == Color.Red)
            {
                if (item.parent == item.parent.parent.left)//dad on the left
                {
                    Node U = item.parent.parent.right;//uncle on the right
                    if (U != null && U.color == Color.Red)//Case 1: uncle is Red 
                    {
                        item.parent.color = Color.Black;
                        U.color = Color.Black;
                        item.parent.parent.color = Color.Red;
                        item = item.parent.parent;
                    }
                    else //Case 2: uncle is Black
                    {
                        if (item == item.parent.right)//dad and grandfather in different directions
                        {
                            item = item.parent;
                            this.LeftRotate(item);
                        }
                        //Case 3: recolor & rotate dad and grandfather are on the same side
                        item.parent.color = Color.Black;
                        item.parent.parent.color = Color.Red;
                        this.RightRotate(item.parent.parent);
                    }
                }
                else //dad on the right
                {
                    Node U = item.parent.parent.left;//uncle on the left
                    if (U != null && U.color == Color.Red)//Case 1 uncle is Red 
                    {
                        item.parent.color = Color.Black;
                        U.color = Color.Black;
                        item.parent.parent.color = Color.Red;
                        item = item.parent.parent;
                    }
                    else //Case 2 uncle is Black
                    {
                        if (item == item.parent.left)
                        {
                            item = item.parent;
                            this.RightRotate(item);
                        }
                        item.parent.color = Color.Black;
                        item.parent.parent.color = Color.Red;
                        this.LeftRotate(item.parent.parent);
                    }
                }
            }
            this.root.color = Color.Black;//re-color the root Black as necessary
        }
        public void Print()
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }
            if (root != null)
            {
                Print(root, 4);
            }
            Console.ResetColor();
        }
        public void Print(Node p, int padding)
        {
            if (p != null)
            {
                if (!p.right.isLeaf)
                {
                    if (p.right.color == Color.Red)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Print(p.right, padding + 4);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Print(p.right, padding + 4);
                    }
                }
                if (padding > 0)
                {
                    Console.Write(" ".PadLeft(padding));
                }
                if (!p.right.isLeaf)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("/\n");
                    Console.Write(" ".PadLeft(padding));
                }
                if (p.color == Color.Red)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(p.data.ToString() + "\n ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(p.data.ToString() + "\n ");
                }
                if (!p.left.isLeaf)
                {
                    if (p.left.color == Color.Red)
                    {
                        Console.Write(" ".PadLeft(padding) + "\\\n");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Print(p.left, padding + 4);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" ".PadLeft(padding) + "\\\n");
                        Print(p.left, padding + 4);
                    }
                }
            }
        }
        public Node TreeSearch(int key, Node node)
        {
            if (!node.isLeaf)
            {
                if (key == node.data)
                {
                    return node;
                }
                if (key < node.data)
                {
                    return TreeSearch(key, node.left);
                }
                else
                {
                    return TreeSearch(key, node.right);
                }
            }
            else
            {
                Console.WriteLine("Такой элемент не найден!");
                return null;
            }
        }
        public Node TreeMinimum(Node node)
        {
            if (node.left != null)
            {
                while (!node.left.isLeaf) node = node.left;
            }
            return node;
        }
        public static void Transplant(RB tree, Node node1, Node node2)
        {
            if ((node1.parent == null) || (node1.parent.isLeaf))
            {
                tree.root = node2;
            }
            else if (node1 == node1.parent.left)
            {
                node1.parent.left = node2;
            }
            else
            {
                node1.parent.right = node2;
            }
            node2.parent = node1.parent;
        }
        public void Delete(RB tree, int value)
        {
            Node deletingNode = TreeSearch(value, tree.root);
            if (deletingNode == null) return;
            else
            {
                Node node2 = deletingNode;
                Color node2ColorOriginal = node2.color;
                Node node1 = new Node(false);
                if ((deletingNode.left.isLeaf) || (deletingNode.left == null) && (deletingNode.right != null)) 
                {
                    node1 = deletingNode.right;
                    Transplant(tree, deletingNode, deletingNode.right);
                }
                else if ((deletingNode.right.isLeaf) || (deletingNode.right == null) && (deletingNode.left != null))
                {
                    node1 = deletingNode.left;
                    Transplant(tree, deletingNode, deletingNode.left);
                }
                else
                {
                    node2 = TreeMinimum(deletingNode.right);
                    node2ColorOriginal = node2.color;
                    node1 = node2.right;
                    if (node2.parent == deletingNode)
                    {
                        node1.parent = node2;
                    }
                    else
                    {
                        Transplant(tree, node2, node2.right);
                        node2.right = deletingNode.right;
                        node2.right.parent = node2;
                    }
                    Transplant(tree, deletingNode, node2);
                    node2.left = deletingNode.left;
                    node2.left.parent = node2;
                    node2.color = deletingNode.color;
                }
                if (node2ColorOriginal == Color.Black)
                {
                    DeleteFixUp(tree, node1);
                }
            }
        }
        public void DeleteFixUp(RB tree, Node node)
        {
            while ((node != tree.root) && (node.color == Color.Black))
            {
                if (node == node.parent.left)
                {
                    Node node1 = node.parent.right;
                    if (node1.color == Color.Red)
                    {
                        node1.color = Color.Black;
                        node.parent.color = Color.Red;
                        LeftRotate(node.parent);
                        node1 = node.parent.right;
                    }
                    if ((node1.left.color == Color.Black) && (node1.right.color == Color.Black))
                    {
                        node1.color = Color.Red;
                        node = node.parent;
                    }
                    else
                    {
                        if (node1.right.color == Color.Black)
                        {
                            node1.left.color = Color.Black;
                            node1.color |= Color.Red;
                            RightRotate(node1);
                            node1 = node.parent.right;
                        }
                        node1.color = node.parent.color;
                        node.parent.color = Color.Black;
                        node1.right.color = Color.Black;
                        LeftRotate(node.parent);
                        node = tree.root;
                    }
                }
                else
                {
                    Node node1 = node.parent.left;
                    if (node1.color == Color.Red)
                    {
                        node1.color = Color.Black;
                        node.parent.color = Color.Red;
                        RightRotate(node.parent);
                        node1 = node.parent.left;
                    }
                    if ((node1.right.color == Color.Black) && (node1.right.color == Color.Black))
                    {
                        node1.color = Color.Red;
                        node = node.parent;
                    }
                    else
                    {
                        if (node1.left.color == Color.Black)
                        {
                            node1.right.color = Color.Black;
                            node1.color = Color.Red;
                            LeftRotate(node1);
                            node1 = node.parent.left;
                        }
                        node1.color = node.parent.color;
                        node.parent.color = Color.Black;
                        node1.left.color = Color.Black;
                        RightRotate(node.parent);
                        node = tree.root;
                    }
                }
            }
            node.color = Color.Black;
        }
    }
}
