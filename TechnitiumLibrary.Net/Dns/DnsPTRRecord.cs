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

using System.Collections.Generic;
using System.IO;

namespace TechnitiumLibrary.Net.Dns
{
    public class DnsPTRRecord : DnsResourceRecordData
    {
        #region variables

        string _ptrDomainName;

        #endregion

        #region constructor

        public DnsPTRRecord(string ptrDomainName)
        {
            _ptrDomainName = ptrDomainName;
        }

        public DnsPTRRecord(Stream s)
            : base(s)
        { }

        #endregion

        #region protected

        protected override void Parse(Stream s)
        {
            _ptrDomainName = DnsDatagram.ConvertLabelToDomain(s);
        }

        protected override void WriteRecordData(Stream s, List<DnsDomainOffset> domainEntries)
        {
            DnsDatagram.ConvertDomainToLabel(_ptrDomainName, s, domainEntries);
        }

        #endregion

        #region public

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            DnsPTRRecord other = obj as DnsPTRRecord;
            if (other == null)
                return false;

            return this._ptrDomainName.Equals(other._ptrDomainName, System.StringComparison.CurrentCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return _ptrDomainName.GetHashCode();
        }

        public override string ToString()
        {
            return _ptrDomainName;
        }

        #endregion

        #region properties

        public string PTRDomainName
        { get { return _ptrDomainName; } }

        #endregion
    }
}
