﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if !NETSTANDARD1_0

using System;
using System.Collections.Concurrent;

namespace Microsoft.VisualStudio.TestPlatform.Utilities;

/// <summary>
/// !!!NEVER USE A FLAG TO ENABLE FUNCTIONALITY!!!
/// 
/// The reasoning is:
/// 
/// * New version will automatically ship with the feature enabled. There is no action needed to be done just before release.
/// * Anyone interested in the new feature will get it automatically by grabbing our preview.
/// * Anyone who needs more time before using the new feature can disable it in the released package.
/// * If we remove the flag from our code, we will do it after the feature is the new default, or after the functionality was completely removed from our codebase.
/// * If there is a very outdated version of an assembly that for some reason loaded with the newest version of TP and it cannot find a feature flag, because we removed that feature flag in the meantime, it will just run with all it's newest features enabled.
///
/// Use constants so the feature name will be compiled directly into the assembly that references this, to avoid backwards compatibility issues, when the flag is removed in newer version.
/// </summary>

// !!! SDK USED FEATURE NAMES MUST BE KEPT IN SYNC IN https://github.com/dotnet/sdk/blob/main/src/Cli/dotnet/commands/dotnet-test/VSTestFeatureFlag.cs !!!
internal partial class FeatureFlag : IFeatureFlag
{
    private readonly ConcurrentDictionary<string, bool> _cache = new();

    public static IFeatureFlag Instance { get; private set; } = new FeatureFlag();

    private FeatureFlag() { }

    // Only check the env variable once, when it is not set or is set to 0, consider it unset. When it is anything else, consider it set.
    public bool IsSet(string featureFlag) => _cache.GetOrAdd(featureFlag, f => (Environment.GetEnvironmentVariable(f)?.Trim() ?? "0") != "0");

    private const string VSTEST_ = nameof(VSTEST_);

    // Added for artifact post-processing, it enable/disable the post processing.
    // Added in 17.2-preview 7.0-preview
    public const string DISABLE_ARTIFACTS_POSTPROCESSING = VSTEST_ + nameof(DISABLE_ARTIFACTS_POSTPROCESSING);

    // Added for artifact post-processing, it will show old output for dotnet sdk scenario.
    // It can be useful if we need to restore old UX in case users are parsing the console output.
    // Added in 17.2-preview 7.0-preview
    public const string DISABLE_ARTIFACTS_POSTPROCESSING_NEW_SDK_UX = VSTEST_ + nameof(DISABLE_ARTIFACTS_POSTPROCESSING_NEW_SDK_UX);

    // Faster JSON serialization relies on less internals of NewtonsoftJson, and on some additional caching.
    public const string DISABLE_FASTER_JSON_SERIALIZATION = VSTEST_ + nameof(DISABLE_FASTER_JSON_SERIALIZATION);

    // Forces vstest.console to run all sources using the same target framework (TFM) and architecture, instead of allowing
    // multiple different tfms and architectures to run at the same time.
    public const string DISABLE_MULTI_TFM_RUN = VSTEST_ + nameof(DISABLE_MULTI_TFM_RUN);

    [Obsolete("Only use this in tests.")]
    internal static void Reset()
    {
        Instance = new FeatureFlag();
    }

    [Obsolete("Only use this in tests.")]
    internal void SetFlag(string key, bool value)
    {
        _cache[key] = value;
    }
}

#endif
