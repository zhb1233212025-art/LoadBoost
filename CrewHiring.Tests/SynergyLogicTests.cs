using Xunit;
using CrewHiring;

namespace CrewHiring.Tests
{
    public class SynergyLogicTests
    {
        [Theory]
        [InlineData("Engineer", "Mechanic")]
        [InlineData("Engineer", "Technician")]
        [InlineData("Engineer", "Miner")]
        [InlineData("Scientist", "Biologist")]
        [InlineData("Scientist", "Geologist")]
        [InlineData("Scientist", "Farmer")]
        [InlineData("Scientist", "Medic")]
        [InlineData("Pilot", "Scout")]
        public void IsAliasOf_映射命中返回true(string required, string actual)
        {
            Assert.True(SynergyLogic.IsAliasOf(required, actual));
        }

        [Theory]
        [InlineData("Engineer", "Engineer")]   // 同名走原版逻辑，不经别名表
        [InlineData("Engineer", "Medic")]      // 反向不视同
        [InlineData("Mechanic", "Engineer")]   // 单向
        [InlineData("Pilot", "Pilot")]
        [InlineData("Scientist", "Kolonist")]
        [InlineData("Engineer", "Scout")]
        public void IsAliasOf_未命中返回false(string required, string actual)
        {
            Assert.False(SynergyLogic.IsAliasOf(required, actual));
        }

        [Theory]
        [InlineData(0, 0.25, 1.0)]
        [InlineData(3, 0.25, 1.75)]
        [InlineData(5, 0.25, 2.25)]
        [InlineData(-1, 0.25, 1.0)]  // 负等级钳到 0
        public void CureFactor_按等级线性加成(int level, double perLevel, double expected)
        {
            Assert.Equal(expected, SynergyLogic.CureFactor(level, perLevel), 6);
        }

        [Theory]
        [InlineData(0, 0.06, 1.0)]
        [InlineData(5, 0.06, 0.7)]
        [InlineData(5, 0.5, 0.0)]   // 减免过头钳到 0（不反转成回血）
        public void RadiationFactor_减免且不为负(int level, double perLevel, double expected)
        {
            Assert.Equal(expected, SynergyLogic.RadiationFactor(level, perLevel), 6);
        }
    }
}
