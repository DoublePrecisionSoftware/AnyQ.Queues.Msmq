# AnyQ.Queues.Msmq

MSMQ Extensions for AnyQ

[![Build status](https://ci.appveyor.com/api/projects/status/3twfg2k1uwamjehy?svg=true)](https://ci.appveyor.com/project/nibblesnbits/anyq-queues-msmq)
[![Coverity status](https://scan.coverity.com/projects/15081/badge.svg)](https://scan.coverity.com/projects/doubleprecisionsoftware-anyq-queues-msmq)
[![NuGet version](https://img.shields.io/nuget/v/AnyQ.Queues.Msmq.svg)](https://www.nuget.org/packages/AnyQ.Queues.Msmq/)

## Summary

AnyQ.Queues.Msmq provides concrete classes for extending the [AnyQ](https://github.com/DoublePrecisionSoftware/AnyQ) library with support for MSMQ via the `System.Messaging` namespace.

## Usage

For most use cases, it should be sufficient to create an instance of the `MsmqJobQueueFactory` class to pass to a `JobQueueListener`.
The `MsmqJobQueueFactory` class has two dependencies: an `IPayloadFormatter`, and an `IRequestSerializer`.

### Example

```cs
var jobQueueFactory = 
    new AnyQ.Queues.Msmq.MsmqJobQueueFactory(new JsonPayloadFormatter(), new JsonRequestSerializer());
var listener = new JobQueueListener(jobQueueFactory);
```

For more control, you may implement your own `JobHandler` using the `MsmqMessageFactory` and `MsmqMessageQueueFactory` classes.

