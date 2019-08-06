﻿using System;
using System.Linq;
using System.Runtime.CompilerServices;
using NumSharp.Backends.Unmanaged;
using NumSharp.Utilities;

namespace NumSharp
{
    public static partial class np
    {
        [MethodImpl((MethodImplOptions)768)]
        public static NDArray array(NDArray nd, bool copy = false)
        {
            return copy ? new NDArray(nd.Storage.Clone()) : new NDArray(nd.Storage);
        }

        public static NDArray array(Array array, Type dtype = null, int ndmin = 1, bool copy = true, char order = 'C')
        {
            var arrType = array.ResolveElementType();

            //handle dim expansion and extract shape
            Shape shape;
            var dims = array.ResolveRank();
            var missing = dims - ndmin;

            if (missing < 0)
            {
                shape = Arrays.Concat(Enumerable.Repeat(1, Math.Abs(missing)).ToArray(), Shape.ExtractShape(array));
            }
            else
            {
                shape = Shape.ExtractShape(array);
            }

            //flatten
            if (shape.NDim > 1)
            {
                array = Arrays.Flatten(array);
                copy = false;
            }

            if (dtype != null && dtype != arrType)
            {
                array = ArrayConvert.To(array, dtype);
                copy = false;
            }

            return new NDArray(copy ? (Array)array.Clone() : array, shape, order);
        }


        public static NDArray array<T>(params T[] data) where T : unmanaged
        {
            return new NDArray(ArraySlice.FromArray(data), new Shape(data.Length));
        }

        public static NDArray array<T>(T[][] data)
        {
            var array = data.SelectMany(inner => inner).ToArray(); //todo! not use selectmany.
            return new NDArray(array, new Shape(data.Length, data[0].Length));
        }

        public static NDArray array<T>(T[][][] data)
        {
            var array = data.SelectMany(inner => inner //todo! not use selectmany.
                    .SelectMany(innerInner => innerInner))
                .ToArray();

            return new NDArray(array, new Shape(data.Length, data[0].Length, data[0][0].Length));
        }

        public static NDArray array<T>(T[][][][] data)
        {
            var array = data.SelectMany(inner => inner //todo! not use selectmany.
                    .SelectMany(innerInner => innerInner
                        .SelectMany(innerInnerInner => innerInnerInner)))
                .ToArray();

            return new NDArray(array, new Shape(data.Length, data[0].Length, data[0][0].Length, data[0][0][0].Length));
        }

        public static NDArray array<T>(T[,] data) where T : unmanaged
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return new NDArray(ArraySlice.FromArray(data.Cast<T>().ToArray()), new Shape(data.GetLength(0), data.GetLength(1)));
        }

        public static NDArray array<T>(T[,,] data) where T : unmanaged
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return new NDArray(ArraySlice.FromArray(data.Cast<T>().ToArray()), new Shape(data.GetLength(0), data.GetLength(1), data.GetLength(2)));
        }

        public static NDArray array<T>(T[,,,] data) where T : unmanaged
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            return new NDArray(ArraySlice.FromArray(data.Cast<T>().ToArray()), new Shape(data.GetLength(0), data.GetLength(1), data.GetLength(2), data.GetLength(3)));
        }
    }
}
