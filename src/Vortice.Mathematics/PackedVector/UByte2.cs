// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using static Vortice.Mathematics.VectorUtilities;

namespace Vortice.Mathematics.PackedVector;

/// <summary>
/// Packed vector type containing two 8 bit unsigned integer components.
/// </summary>
/// <remarks>Equivalent of XMUBYTE2.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly struct UByte2 : IPackedVector<ushort>, IEquatable<UByte2>
{
    [FieldOffset(0)]
    private readonly ushort _packedValue;

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
    /// Initializes a new instance of the <see cref="UByte2"/> struct.
    /// </summary>
    /// <param name="packedValue">The packed value to assign.</param>
    public UByte2(ushort packedValue)
    {
        _packedValue = packedValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UByte2"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    public UByte2(byte x, byte y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UByte2"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    public UByte2(float x, float y)
    {
        Vector128<float> vector = Clamp(Vector128.Create(x, y, 0.0f, 0.0f), Vector128<float>.Zero, UByteMax);
        vector = Round(vector);

        X = (byte)vector.GetX();
        Y = (byte)vector.GetY();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UByte2"/> struct.
    /// </summary>
    /// <param name="vector">The <see cref="Vector2"/> containing X and Y value.</param>
    public UByte2(Vector2 vector)
        : this(vector.X, vector.Y)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UByte2"/> struct.
    /// </summary>
    /// <param name="vector">The <see cref="Vector4"/> containing X and Y value.</param>
    public UByte2(in Vector4 vector)
        : this(vector.X, vector.Y)
    {
    }

    /// <summary>
    /// Gets the packed value.
    /// </summary>
    public ushort PackedValue => _packedValue;

    /// <summary>
    /// Expands the packed representation to a <see cref="Vector2"/>.
    /// </summary>
    public Vector2 ToVector2() => new(X, Y);

    Vector4 IPackedVector.ToVector4()
    {
        Vector2 vector = ToVector2();
        return new Vector4(vector.X, vector.Y, 0.0f, 1.0f);
    }

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is UByte2 other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(UByte2 other) => PackedValue.Equals(other.PackedValue);

    /// <summary>
    /// Compares two <see cref="UByte2"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="UByte2"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="UByte2"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UByte2 left, UByte2 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="UByte2"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="UByte2"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="UByte2"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UByte2 left, UByte2 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => PackedValue.GetHashCode();

    /// <inheritdoc/>
    public override string ToString() => PackedValue.ToString("X8", CultureInfo.InvariantCulture);
}
