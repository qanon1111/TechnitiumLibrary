﻿/*
Technitium Library
Copyright (C) 2018  Shreyas Zare (shreyas@technitium.com)

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
    public class DnsSRVRecord : DnsResourceRecordData
    {
        #region variables

        ushort _priority;
        ushort _weight;
        ushort _port;
        string _target;

        #endregion

        #region constructor

        public DnsSRVRecord(ushort priority, ushort weight, ushort port, string target)
        {
            _priority = priority;
            _weight = weight;
            _port = port;
            _target = target;
        }

        public DnsSRVRecord(Stream s)
            : base(s)
        { }

        #endregion

        #region protected

        protected override void Parse(Stream s)
        {
            _priority = DnsDatagram.ReadUInt16NetworkOrder(s);
            _weight = DnsDatagram.ReadUInt16NetworkOrder(s);
            _port = DnsDatagram.ReadUInt16NetworkOrder(s);
            _target = DnsDatagram.ConvertLabelToDomain(s);
        }

        protected override void WriteRecordData(Stream s, List<DnsDomainOffset> domainEntries)
        {
            DnsDatagram.WriteUInt16NetworkOrder(_priority, s);
            DnsDatagram.WriteUInt16NetworkOrder(_weight, s);
            DnsDatagram.WriteUInt16NetworkOrder(_port, s);
            DnsDatagram.ConvertDomainToLabel(_target, s, null); //no compression for domain name as per RFC
        }

        #endregion

        #region public

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            DnsSRVRecord other = obj as DnsSRVRecord;
            if (other == null)
                return false;

            if (_port != other._port)
                return false;

            if (!_target.Equals(other._target, StringComparison.CurrentCultureIgnoreCase))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return _target.GetHashCode();
        }

        public override string ToString()
        {
            return "{PRIORITY: " + _priority + "; WEIGHT: " + _weight + "; PORT: " + _port + "; TARGET: " + _target + ";}";
        }

        #endregion

        #region properties

        public ushort Priority
        { get { return _priority; } }

        public ushort Weight
        { get { return _weight; } }

        public ushort Port
        { get { return _port; } }

        public string Target
        { get { return _target; } }

        #endregion
    }
}
