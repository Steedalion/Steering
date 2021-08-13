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
        public void OneSegmentFortyFiveDeg()
        {
            point = Vector3.one;
            point = point.RadialRound(2f, 1);
            Assert.AreEqual(2,point.x);
            Assert.AreEqual(0,point.z);
        }

        [Test]
        public void OnAxis1()
        {
            Vector3 point30 = new Vector3(2, 0, 1);
            point = point30.RadialRound(1.5f, 2);
            Assert.AreEqual(1.5f, point.magnitude,0.1f);
            Assert.AreEqual(0f, point.z);
        }
        
            [Test]
        public void OnAxisNegative()
        {
            Vector3 point30 = new Vector3(-2, 0, 2);
            point = point30.RadialRound(1.5f, 2);
            Assert.AreEqual(1.5f, point.magnitude,0.1f);
            Assert.AreEqual(0f, point.z);
        }
    }
}