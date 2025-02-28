﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;

#nullable disable

namespace Microsoft.VisualStudio.TestPlatform.ObjectModel.Client.Interfaces;

/// <summary>
/// Interface defining contract for custom test host implementations
/// </summary>
public interface ITestHostLauncher2 : ITestHostLauncher
{
    /// <summary>
    /// Attach debugger to already running custom test host process.
    /// </summary>
    /// <param name="pid">Process ID of the process to which the debugger should be attached.</param>
    /// <returns><see cref="true"/> if the debugger was successfully attached to the requested process, <see cref="false"/> otherwise.</returns>
    bool AttachDebuggerToProcess(int pid);

    /// <summary>
    /// Attach debugger to already running custom test host process.
    /// </summary>
    /// <param name="pid">Process ID of the process to which the debugger should be attached.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><see cref="true"/> if the debugger was successfully attached to the requested process, <see cref="false"/> otherwise.</returns>
    bool AttachDebuggerToProcess(int pid, CancellationToken cancellationToken);
}

[Obsolete("Do not use this api, it is not ready yet.")]
public interface ITestHostLauncher3 : ITestHostLauncher2
{
    bool AttachDebuggerToProcess(AttachDebuggerInfo attachDebuggerInfo);
}

[Obsolete("Do not use this api, it is not ready yet.")]
public class AttachDebuggerInfo
{
    public Version Version { get; set; }
    public int ProcessId { get; set; }
    public Framework TargetFramework { get; set; }
    public CancellationToken CancellationToken { get; set; }
}
