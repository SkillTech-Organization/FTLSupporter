﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMap.Common.Attrib
{
    //this attribute indicates item type fields
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AzureRowAttr : Attribute
    {
    }
}