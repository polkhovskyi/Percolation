using System.IO;
using NUnit.Framework;

namespace Percolation
{
    internal class Tests
    {
        [Test]
        public void TestInputs()
        {
            for (var i = 1; i <= 4; i++)
            {
                var inputData = File.ReadAllText(string.Format("TestData/input{0}.txt", i));
                var inputArray = inputData.Split(' ');
                var size = int.Parse(inputArray[0]);
                var percolation = new Percolation(size);
                Assert.IsFalse(percolation.CheckIfPercolates());
                for (var index = 1; index < inputArray.Length; index += 2)
                {
                    var rowValue = int.Parse(inputArray[index]);
                    var columnValue = int.Parse(inputArray[index + 1]);
                    Assert.IsFalse(percolation.IsOpen(rowValue, columnValue));
                    percolation.OpenByIndex(rowValue, columnValue);
                    Assert.IsTrue(percolation.IsOpen(rowValue, columnValue));
                }

                Assert.IsTrue(percolation.CheckIfPercolates());
            }
        }

        [Test]
        public void TestInputNo()
        {
            for (var i = 1; i <= 2; i++)
            {
                var inputData = File.ReadAllText(string.Format("TestData/input{0}-no.txt", i));
                var inputArray = inputData.Split(' ');
                var size = int.Parse(inputArray[0]);
                var percolation = new Percolation(size);
                Assert.IsFalse(percolation.CheckIfPercolates());
                if (inputArray.Length > 1)
                {
                    for (var index = 1; index < inputArray.Length; index += 2)
                    {
                        var rowValue = int.Parse(inputArray[index]);
                        var columnValue = int.Parse(inputArray[index + 1]);
                        Assert.IsFalse(percolation.IsOpen(rowValue, columnValue));
                        percolation.OpenByIndex(rowValue, columnValue);
                        Assert.IsTrue(percolation.IsOpen(rowValue, columnValue));
                    }
                }

                Assert.IsFalse(percolation.CheckIfPercolates());
            }
        }
    }
}