// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


#if NETFRAMEWORK
using System;
#endif
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable

namespace Microsoft.VisualStudio.TestPlatform.ObjectModel;

/// <summary>
/// Class that holds Trait.
/// A traits is Name, Value pair.
/// </summary>
#if NETFRAMEWORK
[Serializable]
#endif
[DataContract]
public class Trait
{
    [DataMember(Name = "Key")]
    public string Name { get; set; }

    [DataMember(Name = "Value")]
    public string Value { get; set; }

    internal Trait(KeyValuePair<string, string> data)
        : this(data.Key, data.Value)
    {
    }

    public Trait(string name!!, string value)
    {
        Name = name;
        Value = value;
    }
}
