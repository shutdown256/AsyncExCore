using System;
using System.Runtime.ExceptionServices;

namespace StudioMote.Components.AsyncEx.Internal.PlatformEnlightenment
{
    public static class ExceptionEnlightenment
    {
        public static Exception PrepareForRethrow(Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
            return exception;
        }
    }
}
