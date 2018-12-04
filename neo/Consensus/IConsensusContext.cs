using Neo.Cryptography.ECC;
using Neo.Network.P2P.Payloads;
using System;
using System.Collections.Generic;

namespace Neo.Consensus
{
    public interface IConsensusContext : IDisposable
    {
        //public const uint Version = 0;
        ConsensusState State { get; set; }
        UInt256 PrevHash { get; }
        uint BlockIndex { get; }
        byte ViewNumber { get; }
        ECPoint[] Validators { get; }
        int MyIndex { get; }
        uint PrimaryIndex { get; }
        uint Timestamp { get; set; }
        ulong Nonce { get; set; }
        UInt160 NextConsensus { get; set; }
        UInt256[] TransactionHashes { get; set; }
        Dictionary<UInt256, Transaction> Transactions { get; set; }
        byte[][] SignedPayloads { get; set; }
        byte[][] FinalSignatures { get; set; }
        byte[] ExpectedView { get; set; }

        int M { get; }

        Header PrevHeader { get; }

        bool ContainsTransaction(UInt256 hash);
        bool VerifyTransaction(Transaction tx);

        ConsensusPayload PreparePayload { get; set; }

        void ChangeView(byte view_number);

        Block CreateBlock();

        //void Dispose();

        uint GetPrimaryIndex(byte view_number);

        ConsensusPayload MakeChangeView();

        Block MakeHeader();

        ConsensusPayload MakePrepareRequest(byte[] prepReqSignature, byte[] finalSignature);

        ConsensusPayload MakePrepareResponse(byte[] signature);

        ConsensusPayload MakeCommitAgreement(byte[] finalSignature);

        ConsensusPayload MakeRegeneration();

        void UpdateSpeakerSignatureAtPreparePayload();

        byte[] SignBlock(Block block);

        byte[] SignPreparePayload();

        void SignPayload(ConsensusPayload payload);

        void Reset();

        void Fill();

        bool VerifyRequest();
    }
}
