using System;
using System.Threading.Tasks;

namespace StudioMote.Components.AsyncEx.Internal.PlatformEnlightenment
{
    public static class AsyncEnlightenment
    {
        public static TaskCreationOptions AddDenyChildAttach(TaskCreationOptions options)
        {
            return options | TaskCreationOptions.DenyChildAttach;
        }
    }
}
