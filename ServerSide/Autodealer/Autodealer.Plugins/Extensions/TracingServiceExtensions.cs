﻿using Microsoft.Xrm.Sdk;

using System.Runtime.CompilerServices;

namespace Autodealer.Plugins.Extensions
{
    public static class TracingServiceExtensions
    {
        public static void TraceCaller(this ITracingService tracer, string message, [CallerMemberName] string memberName = "")
        {
            tracer.Trace($"{memberName}: {message}");
        }
    }
}
