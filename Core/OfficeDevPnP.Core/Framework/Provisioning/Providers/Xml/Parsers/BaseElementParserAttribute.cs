﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Parsers
{
    public class BaseElementParserAttribute : Attribute
    {
        public XMLPnPSchemaVersion SupportedSchemas { get; set; }
        public int Sequence { get; set; }
    }
}
