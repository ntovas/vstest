﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#nullable disable

namespace Microsoft.VisualStudio.TestPlatform.Common.Interfaces;

public interface IBaseTestEventsRegistrar
{
    /// <summary>
    /// Log warning message before request is created.
    /// </summary>
    /// <param name="message">message string</param>
    void LogWarning(string message);
}
