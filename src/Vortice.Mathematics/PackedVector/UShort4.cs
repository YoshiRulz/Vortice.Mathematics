// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using static Vortice.Mathematics.VectorUtilities;

namespace Vortice.Mathematics.PackedVector;

/// <summary>
/// Packed vector type containing four 16-bit unsigned integer components.
/// </summary>
/// <remarks>Equivalent of XMUSHORT4.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly struct UShort4 : IPackedVector<ulong>, IEquatable<UShort4>
{
    [FieldOffset(0)]
    private readonly ulong _packedValue;

    /// <summary>
    /// The X component of the vector.
    /// </summary>
    [FieldOffset(0)]
    public readonly ushort X;

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    [FieldOffset(2)]
    public readonly ushort Y;

    /// <summary>
    /// The Z component of the vector.
    /// </summary>
    [FieldOffset(4)]
    public readonly ushort Z;

    /// <summary>
    /// The W component of the vector.
    /// </summary>
    [FieldOffset(6)]
    public readonly ushort W;

    /// <summary>
    /// Initializes a new instance of the <see cref="UShort4"/> struct.
    /// </summary>
    /// <param name="packedValue">The packed value to assign.</param>
    public UShort4(ulong packedValue)
    {
        _packedValue = packedValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UShort4"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    /// <param name="z">The z value.</param>
    /// <param name="w">The w value.</param>
    public UShort4(ushort x, ushort y, ushort z, ushort w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UShort4Normalized"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    /// <param name="z">The z value.</param>
    /// <param name="w">The w value.</param>
    public UShort4(float x, float y, float z, float w)
    {
        Vector128<float> vector = Clamp(Vector128.Create(x, y, z, w), Vector128<float>.Zero, UShortMax);
        vector = Round(vector);

        X = (ushort)vector.GetX();
        Y = (ushort)vector.GetY();
        Z = (ushort)vector.GetZ();
        W = (ushort)vector.GetW();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UShort4"/> struct.
    /// </summary>
    /// <param name="vector">The <see cref="Vector4"/> containing X, Y, Z and W value.</param>
    public UShort4(in Vector4 vector)
        : this(vector.X, vector.Y, vector.Z, vector.W)
    {
    }

    /// <summary>
    /// Constructs a vector from the given <see cref="ReadOnlySpan{Single}" />. The span must contain at least 3 elements.
    /// </summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public UShort4(ReadOnlySpan<float> values)
    {
        Vector128<float> vector = Clamp(Vector128.Create(values), Vector128<float>.Zero, UShortMax);
        vector = Round(vector);

        X = (byte)vector.GetX();
        Y = (byte)vector.GetY();
        Z = (byte)vector.GetZ();
        W = (byte)vector.GetW();
    }

    /// <summary>
    /// Gets the packed value.
    /// </summary>
    public ulong PackedValue => _packedValue;

    /// <summary>
    /// Expands the packed representation to a <see cref="Vector4"/>.
    /// </summary>
    public Vector4 ToVector4() => new(X, Y, Z, W);

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is UShort4 other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(UShort4 other) => PackedValue.Equals(other.PackedValue);

    /// <summary>
    /// Compares two <see cref="UShort4"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="UShort4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="UShort4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UShort4 left, UShort4 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="UShort4"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="UShort4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="UShort4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UShort4 left, UShort4 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => PackedValue.GetHashCode();

    /// <inheritdoc/>
    public override string ToString() => PackedValue.ToString("X16", CultureInfo.InvariantCulture);
}
