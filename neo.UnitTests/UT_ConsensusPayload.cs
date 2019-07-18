using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo.IO.Json;
using Neo.IO;
using Neo.Network.P2P.Payloads;
using System.IO;
using System.Text;

namespace Neo.UnitTests
{
    [TestClass]
    public class UT_ConsensusPayload
    {
        ConsensusPayload uut;

        [TestInitialize]
        public void TestSetup()
        {
            uut = new ConsensusPayload();
            uut.Witness = new Witness();
            uut.Witness.InvocationScript = new byte[0];
            uut.Witness.VerificationScript = new byte[0];
            uut.PrevHash = new UInt256();
            uut.Data = new byte[0];
        }

        [TestMethod]
        public void Test_Size()
        {
            //public uint Version;
            //public UInt256 PrevHash;
            //public uint BlockIndex;
            //public ushort ValidatorIndex;
            //public byte[] Data;
            //public Witness Witness;

            uut.PrevHash.Size.Should().Be(32);
            uut.Data.GetVarSize().Should().Be(1);
            uut.Witness.Size.Should().Be(2); // +1 (implicit array)

            uut.Size.Should().Be(4 + 32 + 4 + 2 + 1 + 1+2);
        }
    }
}
