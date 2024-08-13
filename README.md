# XunitFixtureSink

Reproduction for https://github.com/xunit/xunit/issues/3001

## The problem

In xUnit.net v2 it's possible to inject an `IMessageSink` into fixtures.

In xUnit.net v3 ([xunit.v3/0.2.0-pre.69](https://www.nuget.org/packages/xunit.v3/0.2.0-pre.69) on [xunit.runner.visualstudio/3.0.0-pre.24](https://www.nuget.org/packages/xunit.runner.visualstudio/3.0.0-pre.24)) it doesn't work.

## Reproduction steps

Running `dotnet test XunitFixtureSinkV2` produces the following output.

```
Test run for ~/XunitFixtureSink/XunitFixtureSinkV2/bin/Debug/net8.0/XunitFixtureSinkV2.dll (.NETCoreApp,Version=v8.0)
Microsoft (R) Test Execution Command Line Tool Version 17.10.0 (arm64)
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
[xUnit.net 00:00:00.05] XunitFixtureSinkV2: Hello from message sink

Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: < 1 ms - XunitFixtureSinkV2.dll (net8.0)
```

✅ We can see **Hello from message sink** printed, as expected.

Running `dotnet test XunitFixtureSinkV3` produces the following output.

```
Test run for ~/XunitFixtureSink/XunitFixtureSinkV3/bin/Debug/net8.0/XunitFixtureSinkV3.dll (.NETCoreApp,Version=v8.0)
Microsoft (R) Test Execution Command Line Tool Version 17.10.0 (arm64)
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: 9 ms - XunitFixtureSinkV3.dll (net8.0)
```

❌ The diagnostic message sent in the `SampleFixture` is not printed.

Moreover, running the `XunitFixtureSinkV3` in JetBrains Rider 2024.2 RC 1 produces the following error.

```
Xunit.Sdk.TestPipelineException
Class fixture type 'XunitFixtureSink.SampleFixture' had one or more unresolved constructor arguments: IMessageSink messageSink
   at Xunit.v3.FixtureMappingManager.GetFixture(Type fixtureType) in D:\a\xunit\xunit\src\xunit.v3.core\Utility\FixtureMappingManager.cs:line 166
   at Xunit.v3.FixtureMappingManager.InitializeAsync(IReadOnlyCollection`1 fixtureTypes) in D:\a\xunit\xunit\src\xunit.v3.core\Utility\FixtureMappingManager.cs:line 226
   at Xunit.v3.ExceptionAggregator.RunAsync(Func`1 code) in D:\a\xunit\xunit\src\xunit.v3.core\Exceptions\ExceptionAggregator.cs:line 120
```