using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class TestRadialRound
    {
        private Vector3 point;

        [SetUp]
        public void SetUp()
        {
            point = Vector3.one;
        }
        [Test]
        public void RadialRoundZero()
        {
             point = Vector3.one;
            Assert.AreEqual(Vector3.zero,point.RadialRound(0f, 0));
        }
        [Test]
        public void ZeroRadius()
        {
            point = Vector3.one;
            Assert.AreEqual(Vector3.zero,point.RadialRound(0f, 1));
        }
        [Test]
        public void SmallRadius()
        {
            point = Vector3.one;
            point = point.RadialRound(0.1f, 0);
            
            Assert.AreEqual(point.magnitude,0.1f);
        }
        
        [Test]
        public void ZeroAngleChange()
        {
            point = Vector3.one;
            point = point.RadialRound(2f, 0);
            Assert.AreEqual(point.x,point.y);
            Assert.AreEqual(point.x,point.z);
        }
        
         [Test]
        public void OneSegment()
        {
            point = Vector3.one;
            point = point.RadialRound(2f, 1);
            Assert.AreEqual(point.x,point.y);
            Assert.AreEqual(point.x,point.z);
        }
        
        
    }
    public class TestRounding
    {
        private float increment = 1;

        [Test]
        public void SnapVector3SimplePasses()
        {
            for (int i = 0; i < 100; i++)
            {
                float randy = Random.Range(-100f, 100f);
                Assert.AreEqual(Mathf.Round(randy), Snapper.Round(randy, 1), "Not rounded correct " + randy);
            }
        }

        [Test]
        public void PositiveDecimal()
        {
            float randy = 0.1f;
            Assert.AreEqual(0, Snapper.Round(randy, 0.5f), "Not rounded correct " + randy);
            randy = 0.6f;
            Assert.AreEqual(0.5f, Snapper.Round(randy, 0.5f), "Not rounded correct " + randy);
        }

        [Test]
        public void NegativeDecimal()
        {
            float randy = -0.1f;
            Assert.AreEqual(0, Snapper.Round(randy, 0.5f), "Not rounded correct " + randy);
            randy = -0.6f;
            Assert.AreEqual(-0.5f, Snapper.Round(randy, 0.5f), "Not rounded correct " + randy);
        }

        [Test]
        public void LargerThanOne()
        {
            float randy = 0.1f;
            Assert.AreEqual(0, Snapper.Round(randy, 2f), "Not rounded correct " + randy);
            randy = 1.2f;
            Assert.AreEqual(2f, Snapper.Round(randy, 2f), "Not rounded correct " + randy);
        }

        [Test]
        public void ZeroSnappsToZero()
        {
            for (int i = 0; i < 100; i++)
            {
                float randy = Random.Range(-100f, 100f);
                Assert.AreEqual(0, Snapper.Round(randy, 0), "Should be zero " + randy);
            }
        }
    }
}