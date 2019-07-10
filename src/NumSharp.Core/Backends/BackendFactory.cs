﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NumSharp.Backends
{
    public class BackendFactory
    {
        [DebuggerNonUserCode]
        public static TensorEngine GetEngine(BackendType backendType = BackendType.Default)
        {
            switch (backendType)
            {
                case BackendType.Default:
                    return EngineCache<DefaultEngine>.Value;
                default:
                    throw new ArgumentOutOfRangeException(nameof(backendType), backendType, null);
            }
        }

        [DebuggerNonUserCode]
        public static TensorEngine GetEngine<T>() where T : TensorEngine, new()
        {
            return EngineCache<T>.Value;
        }

        private static class EngineCache<T> where T : TensorEngine, new()
        {
            public static T Value { get; } = new T();
        }
    }
}
