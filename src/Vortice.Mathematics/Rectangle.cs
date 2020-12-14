﻿// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

// Implementation is based on, see below
// ImageSharp: https://github.com/SixLabors/ImageSharp/blob/master/LICENSE
// SkiaSharp: https://github.com/mono/SkiaSharp/blob/master/LICENSE.md

using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines an integer rectangle structure defining X, Y, Width, Height.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct Rectangle : IEquatable<Rectangle>
    {
        /// <summary>
        /// Returns a <see cref="Rectangle"/> with all of its values set to zero.
        /// </summary>
        public static readonly Rectangle Empty = default;

        /// <summary>
		/// Initializes a new instance of the <see cref="Rectangle"/> struct.
		/// </summary>
		/// <param name="x">The horizontal position of the rectangle.</param>
        /// <param name="y">The vertical position of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
		public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
		/// Initializes a new instance of the <see cref="Rectangle"/> struct.
		/// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
		public Rectangle(int width, int height)
        {
            X = 0;
            Y = 0;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> struct.
        /// </summary>
        /// <param name="point">
        /// The <see cref="Point"/> which specifies the rectangles point in a two-dimensional plane.
        /// </param>
        /// <param name="size">
        /// The <see cref="Size"/> which specifies the rectangles height and width.
        /// </param>
        public Rectangle(Point point, Size size)
        {
            X = point.X;
            Y = point.Y;
            Width = size.Width;
            Height = size.Height;
        }

        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="Rectangle"/>.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="Rectangle"/>.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Gets or sets the width of this <see cref="Rectangle"/>.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets or sets the height of this <see cref="Rectangle"/>.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets or sets the upper-left value of the <see cref="Rectangle"/>.
        /// </summary>
        public Point Location => new Point(X, Y);

        /// <summary>
        /// Gets or sets the size of this <see cref="Rectangle"/>.
        /// </summary>
        public Size Size => new Size(Width, Height);

        /// <summary>
        /// Gets or sets the x-coordinate of the left edge of this <see cref="Rectangle"/>.
        /// </summary>
        public int Left => X;

        /// <summary>
        /// Gets or sets the y-coordinate of the left edge of this <see cref="Rectangle"/>.
        /// </summary>
        public int Top => Y;

        /// <summary>
        /// Gets the x-coordinate of the right edge of this <see cref="Rectangle"/>.
        /// </summary>
        public int Right => unchecked(X + Width);

        /// <summary>
        /// Gets the y-coordinate of the bottom edge of this <see cref="Rectangle"/>.
        /// </summary>
        public int Bottom => unchecked(Y + Height);

        /// <summary>
        /// Gets the <see cref="Point"/> that specifies the center of this <see cref="Rectangle"/>.
        /// </summary>
        public Point Center => new Point(X + (Width / 2), Y + (Height / 2));

        /// <summary>
        /// Gets the x-coordinate of the center of this <see cref="Rectangle"/>.
        /// </summary>
        public int CenterX => unchecked(X + (Width / 2));

        /// <summary>
        /// Gets the y-coordinate of the center of this <see cref="Rectangle"/>.
        /// </summary>
        public int CenterY => unchecked(Y + (Height / 2));

        /// <summary>
        /// Gets the aspect ratio of this <see cref="Rectangle"/>.
        /// </summary>
        public float AspectRatio => (float)Width / Height;

        /// <summary>
        /// Gets the position of the top-left corner of this <see cref="Rectangle"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Point TopLeft => new Point(X, Y);

        /// <summary>
        /// Gets the position of the top-right corner of this <see cref="Rectangle"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Point TopRight => new Point(Right, Y);

        /// <summary>
        /// Gets the position of the bottom-left corner of <see cref="Rectangle"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Point BottomLeft => new Point(X, Bottom);

        /// <summary>
        /// Gets the position of the bottom-right corner of <see cref="Rectangle"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Point BottomRight => new Point(Right, Bottom);

        /// <summary>
        /// Gets a value indicating whether this <see cref="Rectangle"/> is empty.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsEmpty => (Width == 0) && (Height == 0) && (X == 0) && (Y == 0);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Rectangle"/> to <see cref="RectangleF" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RectangleF(Rectangle value) => new RectangleF(value.X, value.Y, value.Width, value.Height);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Rectangle"/> to <see cref="System.Drawing.Rectangle" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.Rectangle(Rectangle value) => new System.Drawing.Rectangle(value.X, value.Y, value.Width, value.Height);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.Rectangle"/> to <see cref="Rectangle" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Rectangle(System.Drawing.Rectangle value) => new Rectangle(value.X, value.Y, value.Width, value.Height);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Rectangle"/> to <see cref="System.Drawing.RectangleF" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.RectangleF(Rectangle value) => new System.Drawing.RectangleF(value.X, value.Y, value.Width, value.Height);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Rectangle"/> to <see cref="Vector4" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Vector4(Rectangle value) => new Vector4(value.X, value.Y, value.Width, value.Height);

        /// <summary>
        /// Creates a new <see cref='Rectangle'/> with the specified left, top, right and bottom coordinate.
        /// </summary>
        public static Rectangle FromLTRB(int left, int top, int right, int bottom) =>
            new Rectangle(left, top, unchecked(right - left), unchecked(bottom - top));

        /// <summary>
        /// Inflates this <see cref="Rectangle"/> by the specified amount.
        /// </summary>
        /// <param name="horizontalAmount">X inflate amount.</param>
        /// <param name="verticalAmount">Y inflate amount.</param>
        /// <returns>New inflated <see cref="Rectangle"/>.</returns>
        public Rectangle Inflate(int horizontalAmount, int verticalAmount)
        {
            unchecked
            {
                int x = X - horizontalAmount;
                int y = Y - verticalAmount;
                int width = Width + 2 * horizontalAmount;
                int height = Height + 2 * verticalAmount;
                return new Rectangle(x, y, width, height);
            }
        }

        /// <summary>
        /// Inflates this <see cref="Rectangle"/> by the specified amount.
        /// </summary>
        /// <param name="size">The size amount.</param>
        public void Inflate(Size size) => Inflate(size.Width, size.Height);

        /// <summary>
        /// Creates a <see cref="Rectangle"/> that is inflated by the specified amount.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="horizontalAmount">The amount to inflate the width by.</param>
        /// <param name="verticalAmount">The amount to inflate the height by.</param>
        /// <returns>A new <see cref="Rectangle"/>.</returns>
        public static Rectangle Inflate(Rectangle rectangle, int horizontalAmount, int verticalAmount)
        {
            unchecked
            {
                return new Rectangle(
                    rectangle.X - horizontalAmount,
                    rectangle.Y - verticalAmount,
                    rectangle.Width + 2 * horizontalAmount,
                    rectangle.Height + 2 * verticalAmount
                    );
            }
        }

        /// <summary>
        /// Creates a rectangle that represents the intersection between <paramref name="a"/> and
        /// <paramref name="b"/>. If there is no intersection, an empty rectangle is returned.
        /// </summary>
        /// <param name="a">The first rectangle.</param>
        /// <param name="b">The second rectangle.</param>
        /// <returns>The <see cref="Rectangle"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle Intersect(Rectangle a, Rectangle b)
        {
            int x1 = Math.Max(a.X, b.X);
            int x2 = Math.Min(a.Right, b.Right);
            int y1 = Math.Max(a.Y, b.Y);
            int y2 = Math.Min(a.Bottom, b.Bottom);

            if (x2 >= x1 && y2 >= y1)
            {
                return new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }

            return Empty;
        }

        /// <summary>
        /// Creates a rectangle that represents the union between <paramref name="a"/> and <paramref name="b"/>.
        /// </summary>
        /// <param name="a">The first rectangle.</param>
        /// <param name="b">The second rectangle.</param>
        /// <returns>The <see cref="Rectangle"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle Union(Rectangle a, Rectangle b)
        {
            int x1 = Math.Min(a.X, b.X);
            int x2 = Math.Max(a.Right, b.Right);
            int y1 = Math.Min(a.Y, b.Y);
            int y2 = Math.Max(a.Bottom, b.Bottom);

            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        /// Translates this rectangle by a specified offset.
        /// </summary>
        /// <param name="offset">The offset value.</param>
        public void Offset(Point offset) => Offset(offset.X, offset.Y);

        /// <summary>
        /// Translates this rectangle by a specified offset.
        /// </summary>
        /// <param name="offsetX">The amount to offset the x-coordinate.</param>
        /// <param name="offsetY">The amount to offset the y-coordinate.</param>
        /// <returns>The new offseted <see cref="Rectangle"/>.</returns>
        public Rectangle Offset(int offsetX, int offsetY)
        {
            unchecked
            {
                return new Rectangle(X + offsetX, Y + offsetY, Width, Height);
            }
        }

        /// <summary>
        /// Determines whether the specified coordinates are inside this rectangle.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>Returns true if the coordinates are inside this rectangle, otherwise false.</returns>
        public bool Contains(int x, int y) => X <= x && x < Right && Y <= y && y < Bottom;

        /// <summary>
        /// Determines whether the specified coordinates are inside this rectangle.
        /// </summary>
        /// <param name="point">The point to test.</param>
        /// <returns>Returns true if the coordinates are inside this rectangle, otherwise false.</returns>
        public bool Contains(Point point) => Contains(point.X, point.Y);

        /// <summary>
        /// Determines whether the specified rectangle is inside this rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to test.</param>
        /// <returns>Returns true if the rectangle is inside this rectangle, otherwise false.</returns>
        public bool Contains(Rectangle rectangle)
        {
            return
                (X <= rectangle.X) && (rectangle.Right <= Right)
                && (Y <= rectangle.Y) && (rectangle.Bottom <= Bottom);
        }

        /// <summary>
        /// Determines if the specfied <see cref="Rectangle"/> intersects the rectangular region defined by
        /// this <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rectangle">The other Rectange. </param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IntersectsWith(Rectangle rectangle)
        {
            return (rectangle.X < Right) && (X < rectangle.Right)
                && (rectangle.Y < Bottom) && (Y < rectangle.Bottom);
        }

        /// <summary>
        /// Converts a <see cref="RectangleF"/> to a <see cref="Rectangle"/> by performing a truncate operation on all the coordinates.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="Rectangle"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle Truncate(RectangleF rectangle)
        {
            unchecked
            {
                return new Rectangle(
                    (int)rectangle.X,
                    (int)rectangle.Y,
                    (int)rectangle.Width,
                    (int)rectangle.Height);
            }
        }

        /// <summary>
        /// Converts a <see cref="RectangleF"/> to a <see cref="Rectangle"/> by performing a round operation on all the coordinates.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="Rectangle"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle Round(RectangleF rectangle)
        {
            unchecked
            {
                return new Rectangle(
                    (int)MathF.Round(rectangle.X),
                    (int)MathF.Round(rectangle.Y),
                    (int)MathF.Round(rectangle.Width),
                    (int)MathF.Round(rectangle.Height));
            }
        }

        /// <summary>
        /// Transforms a rectangle by the given matrix.
        /// </summary>
        /// <param name="rectangle">The source rectangle.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>A transformed rectangle.</returns>
        public static RectangleF Transform(Rectangle rectangle, Matrix3x2 matrix)
        {
            PointF bottomRight = Point.Transform(new Point(rectangle.Right, rectangle.Bottom), matrix);
            PointF topLeft = Point.Transform(rectangle.Location, matrix);
            return new RectangleF(topLeft, new SizeF(bottomRight - topLeft));
        }

        /// <inheritdoc/>
		public override bool Equals(object? obj) => obj is Rectangle value && Equals(value);

        /// <summary>
        /// Determines whether the specified <see cref="Rectangle"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Rectangle"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Rectangle other)
        {
            return X == other.X
                && Y == other.Y
                && Width == other.Width
                && Height == other.Height;
        }

        /// <summary>
        /// Compares two <see cref="Rectangle"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Rectangle"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Rectangle"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Rectangle left, Rectangle right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="Rectangle"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Rectangle"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Rectangle"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Rectangle left, Rectangle right) => !left.Equals(right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(X);
                hashCode.Add(Y);
                hashCode.Add(Width);
                hashCode.Add(Height);
            }
            return hashCode.ToHashCode();
        }

        /// <inheritdoc/>
        public override string ToString() => $"{{X={X},Y={Y},Width={Width},Height={Height}}}";
    }
}
