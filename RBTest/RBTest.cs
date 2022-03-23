using Microsoft.VisualStudio.TestTools.UnitTesting;
using Red_BlackTree;


namespace RBTest
{
    public enum NodesData { FIRST = 5, SECOND = 3, THIRD = 7 }
    [TestClass]
    public class RBTest
    {
        [TestMethod]
        public void checkInsert()
        {
            RB treeKnown = new RB();
            treeKnown.root = new Node((int)NodesData.FIRST);
            treeKnown.root.left = new Node((int)NodesData.SECOND);
            treeKnown.root.right = new Node((int)NodesData.THIRD);
            RB treeTest = new RB();
            treeTest.Insert((int)NodesData.FIRST);
            treeTest.Insert((int)NodesData.SECOND);
            treeTest.Insert((int)NodesData.THIRD);
            Assert.AreEqual(treeTest.root.data, treeKnown.root.data);
            Assert.AreEqual(treeTest.root.left.data, treeKnown.root.left.data);
            Assert.AreEqual(treeTest.root.right.data, treeKnown.root.right.data);
        }

        [TestMethod]
        public void checkDelete()
        {
            RB treeTest = new RB();
            treeTest.Insert((int)NodesData.FIRST);
            treeTest.Insert((int)NodesData.SECOND);
            treeTest.Insert((int)NodesData.THIRD);
            treeTest.Delete(treeTest, (int)NodesData.SECOND);
            Assert.IsTrue(treeTest.root.left.isLeaf);
        }

        [TestMethod]
        public void checkMin()
        {
            RB treeTest = new RB();
            treeTest.Insert((int)NodesData.FIRST);
            treeTest.Insert((int)NodesData.SECOND);
            treeTest.Insert((int)NodesData.THIRD);
            Node minNode = treeTest.TreeMinimum(treeTest.root);
            Assert.AreEqual(minNode.data, (int)NodesData.SECOND);
        }


    }
}
