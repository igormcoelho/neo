﻿using Neo.IO;
using Neo.IO.Json;
using Neo.SmartContract;
using Neo.VM;
using System.IO;

namespace Neo.Network.P2P.Payloads
{
    public class Witness : ISerializable
    {
        public WitnessScope WitnessScope;
        public byte[] InvocationScript;
        public byte[] VerificationScript;

        private UInt160 _scriptHash;
        public virtual UInt160 ScriptHash
        {
            get
            {
                if (_scriptHash == null)
                {
                    _scriptHash = VerificationScript.ToScriptHash();
                }
                return _scriptHash;
            }
        }

        public int Size => WitnessScope.Size + InvocationScript.GetVarSize() + VerificationScript.GetVarSize();

        void ISerializable.Deserialize(BinaryReader reader)
        {
            WitnessScope = reader.ReadSerializable<WitnessScope>();
            InvocationScript = reader.ReadVarBytes(65536);
            VerificationScript = reader.ReadVarBytes(65536);
        }

        void ISerializable.Serialize(BinaryWriter writer)
        {
            writer.Write(WitnessScope);
            writer.WriteVarBytes(InvocationScript);
            writer.WriteVarBytes(VerificationScript);
        }

        public JObject ToJson()
        {
            JObject json = new JObject();
            json["scope"] = WitnessScope.ToJson();
            json["invocation"] = InvocationScript.ToHexString();
            json["verification"] = VerificationScript.ToHexString();
            return json;
        }
    }
}
