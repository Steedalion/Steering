using NUnit.Framework;
using UnityEngine;

namespace Tests
{
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