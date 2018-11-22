using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo.IO.Json;
using Neo.IO.Caching;
using Neo.IO.Wrappers;

using Neo.Cryptography;
using Neo.Cryptography.ECC;
using Neo.IO;
using Neo.Ledger;
using Neo.Network.P2P.Payloads;
using Neo.Persistence;
using Neo.Plugins;
using Neo.SmartContract;
using Neo.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;

using Akka.Actor;
using Akka.Configuration;

using Moq;

namespace Neo.Consensus
{
    internal class TestConsensusContext : IConsensusContext
    {
        private ConsensusState _State;
        public ConsensusState State
        {
            get => _State;
            set => _State = value;
        }
        private UInt256 _PrevHash;
        public UInt256 PrevHash
        {
            get => _PrevHash;
            set => _PrevHash = value;
        }
        private uint _BlockIndex;
        public uint BlockIndex
        {
            get => _BlockIndex;
            set => _BlockIndex = value;
        }
        private byte _ViewNumber;
        public byte ViewNumber
        {
            get => _ViewNumber;
            set => _ViewNumber = value;
        }
        private ECPoint[] _Validators;
        public ECPoint[] Validators
        {
            get => _Validators;
            set => _Validators = value;
        }
        private int _MyIndex;
        public int MyIndex
        {
            get => _MyIndex;
            set => _MyIndex = value;
        }
        private uint _PrimaryIndex;
        public uint PrimaryIndex
        {
            get => _PrimaryIndex;
            set => _PrimaryIndex = value;
        }
        private uint _Timestamp;
        public uint Timestamp
        {
            get => _Timestamp;
            set => _Timestamp = value;
        }
        private ulong _Nonce;
        public ulong Nonce
        {
            get => _Nonce;
            set => _Nonce = value;
        }
        private UInt160 _NextConsensus;
        public UInt160 NextConsensus
        {
            get => _NextConsensus;
            set => _NextConsensus = value;
        }
        private UInt256[] _TransactionHashes;
        public UInt256[] TransactionHashes
        {
            get => _TransactionHashes;
            set => _TransactionHashes = value;
        }
        private Dictionary<UInt256, Transaction> _Transactions;
        public Dictionary<UInt256, Transaction> Transactions
        {
            get => _Transactions;
            set => _Transactions = value;
        }
        private byte[][] _Signatures;
        public byte[][] Signatures
        {
            get => _Signatures;
            set => _Signatures = value;
        }
        private byte[] _ExpectedView;
        public byte[] ExpectedView
        {
            get => _ExpectedView;
            set => _ExpectedView = value;
        }

        public int M => Validators.Length - (Validators.Length - 1) / 3;

        public TestConsensusContext()
        {
        }

        public uint SnapshotHeight
        {
            get
            {
                return 0;
            }
        }

        public Header SnapshotHeader
        {
            get
            {
                return null;
            }
        }

        public bool RejectTx(Transaction tx, bool verify)
        {
            return false;
        }

        public void ChangeView(byte view_number)
        {
        }

        public Block CreateBlock()
        {
            return null;
        }

        public void Dispose()
        {
        }

        public uint GetPrimaryIndex(byte view_number)
        {
            int p = ((int)BlockIndex - view_number) % Validators.Length;
            return p >= 0 ? (uint)p : (uint)(p + Validators.Length);
        }

        public ConsensusPayload MakeChangeView()
        {
            return null;
        }

        public Block MakeHeader()
        {
            return null;
        }

        public void SignHeader()
        {
        }

        public ConsensusPayload MakePrepareRequest()
        {
            return null;
        }

        public ConsensusPayload MakePrepareResponse(byte[] signature)
        {
            return null;
        }

        public void Reset()
        {
        }

        public void Fill(uint currentTimestamp)
        {
        }

        public bool VerifyRequest()
        {
            return true;
        }
    }

    [TestClass]
    public class UT_ConsensusService
    {
        //ConsensusService uut;
        IActorRef uut;

        [TestInitialize]
        public void TestSetup()
        {
            //Store store = new TestStore();
            NeoSystem system = new NeoSystem(null);
            IConsensusContext context = new TestConsensusContext();
            //uut = ActorSystem.ActorOf(ConsensusService.Props(system, context));
            uut = system.ActorSystem.ActorOf(Akka.Actor.Props.Create(() => new ConsensusService(system, context)).WithMailbox("consensus-service-mailbox"));

            var mockConsensus = new Mock<ConsensusService>();
            //Consensus.Tell(new ConsensusService.Start());
            //uut = new ConsensusService(system, context);
        }

        [TestMethod]
        public void ConsensusStart()
        {
            uut.Tell(new ConsensusService.Start());
        }
    }
}
