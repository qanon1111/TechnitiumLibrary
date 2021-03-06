﻿/*
Technitium Library
Copyright (C) 2017  Shreyas Zare (shreyas@technitium.com)

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Collections.Generic;
using System.IO;

namespace TechnitiumLibrary.Net.Dns
{
    public class DnsUnknownRecord : DnsResourceRecordData
    {
        #region variables

        byte[] _data;

        #endregion

        #region constructor

        public DnsUnknownRecord(byte[] data)
        {
            _data = data;
        }

        public DnsUnknownRecord(Stream s)
            : base(s)
        { }

        #endregion

        #region protected

        protected override void Parse(Stream s)
        {
            _data = new byte[_length];

            if (s.Read(_data, 0, _length) != _length)
                throw new EndOfStreamException();
        }

        protected override void WriteRecordData(Stream s, List<DnsDomainOffset> domainEntries)
        {
            s.Write(_data, 0, _data.Length);
        }

        #endregion

        #region public

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            DnsUnknownRecord other = obj as DnsUnknownRecord;
            if (other == null)
                return false;

            if (this._data.Length != other._data.Length)
                return false;

            for (int i = 0; i < this._data.Length; i++)
            {
                if (this._data[i] != other._data[i])
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return _data.GetHashCode();
        }

        public override string ToString()
        {
            return Convert.ToBase64String(_data);
        }

        #endregion

        #region properties

        public byte[] DATA
        { get { return _data; } }

        #endregion
    }
}
