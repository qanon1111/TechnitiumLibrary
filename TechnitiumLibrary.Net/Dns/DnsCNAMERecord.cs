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
    public class DnsCNAMERecord : DnsResourceRecordData
    {
        #region variables

        string _cnameDomainName;

        #endregion

        #region constructor

        public DnsCNAMERecord(string cnameDomainName)
        {
            _cnameDomainName = cnameDomainName;
        }

        public DnsCNAMERecord(Stream s)
            : base(s)
        { }

        #endregion

        #region protected

        protected override void Parse(Stream s)
        {
            _cnameDomainName = DnsDatagram.ConvertLabelToDomain(s);
        }

        protected override void WriteRecordData(Stream s, List<DnsDomainOffset> domainEntries)
        {
            DnsDatagram.ConvertDomainToLabel(_cnameDomainName, s, domainEntries);
        }

        #endregion

        #region public

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            DnsCNAMERecord other = obj as DnsCNAMERecord;
            if (other == null)
                return false;

            return this._cnameDomainName.Equals(other._cnameDomainName, System.StringComparison.CurrentCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return _cnameDomainName.GetHashCode();
        }

        public override string ToString()
        {
            return _cnameDomainName;
        }

        #endregion

        #region properties

        public string CNAMEDomainName
        { get { return _cnameDomainName; } }

        #endregion
    }
}
