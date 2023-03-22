// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

// This file includes code based on code from https://github.com/microsoft/DirectXMath
// The original code is Copyright � Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Vortice.Mathematics.Vector4Utilities;

namespace Vortice.Mathematics.PackedVector;

/// <summary>
/// Packed vector type containing four 8 bit unsigned normalized integer components.
/// </summary>
/// <remarks>Equivalent of XMUBYTEN4.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly struct UByteN4 : IPackedVector<uint>, IEquatable<UByteN4>
{
    [FieldOffset(0)]
    private readonly uint _packedValue;

    /// <summary>
    /// The X component of the vector.
    /// </summary>
    [FieldOffset(0)]
    public readonly byte X;

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    [FieldOffset(1)]
    public readonly byte Y;

    /// <summary>
    /// The Z component of the vector.
    /// </summary>
    [FieldOffset(1)]
    public readonly byte Z;

    /// <summary>
    /// The W component of the vector.
    /// </summary>
    [FieldOffset(1)]
    public readonly byte W;

    /// <summary>
    /// Initializes a new instance of the <see cref="UByteN4"/> struct.
    /// </summary>
    /// <param name="packedValue">The packed value to assign.</param>
    public UByteN4(uint packedValue)
    {
        Unsafe.SkipInit(out this);

        _packedValue = packedValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UByteN4"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    /// <param name="z">The z value.</param>
    /// <param name="w">The w value.</param>
    public UByteN4(byte x, byte y, byte z, byte w)
    {
        Unsafe.SkipInit(out this);

        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UByteN4"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    /// <param name="z">The z value.</param>
    /// <param name="w">The w value.</param>
    public UByteN4(float x, float y, float z, float w)
        : this(new Vector4(x, y, z, w))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UByteN4"/> struct.
    /// </summary>
    /// <param name="vector">The <see cref="Vector4"/> containing X and Y value.</param>
    public UByteN4(in Vector4 vector)
    {
        Unsafe.SkipInit(out this);

        Vector4 result = Saturate(vector);
        result = Vector4.Multiply(result, UByteMax);
        result = Truncate(result);

        X = (byte)result.X;
        Y = (byte)result.Y;
        Z = (byte)result.Z;
        W = (byte)result.W;
    }

    /// <summary>Constructs a vector from the given <see cref="ReadOnlySpan{Single}" />. The span must contain at least 3 elements.</summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public UByteN4(ReadOnlySpan<float> values)
    {
        if (values.Length < 4)
        {
            throw new ArgumentOutOfRangeException(nameof(values));
        }

        Unsafe.SkipInit(out this);

        Vector4 vector = new(values);
        Vector4 result = Saturate(vector);
        result = Vector4.Multiply(result, UByteMax);
        result = Truncate(result);

        X = (byte)result.X;
        Y = (byte)result.Y;
        Z = (byte)result.Z;
        W = (byte)result.W;
    }

    /// <summary>
    /// Gets the packed value.
    /// </summary>
    public uint PackedValue => _packedValue;

    /// <summary>
    /// Expands the packed representation to a <see cref="Vector4"/>.
    /// </summary>
    public Vector4 ToVector4()
    {
        return new(X / 255.0f, Y / 255.0f, Z / 255.0f, W / 255.0f);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is UByteN4 other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(UByteN4 other) => PackedValue.Equals(other.PackedValue);

    /// <summary>
    /// Compares two <see cref="UByteN4"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="UByteN4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="UByteN4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UByteN4 left, UByteN4 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="UByteN4"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="UByteN4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="UByteN4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UByteN4 left, UByteN4 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => PackedValue.GetHashCode();

    /// <inheritdoc/>
    public override string ToString() => PackedValue.ToString("X8", CultureInfo.InvariantCulture);
}
