// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

#if WINDOWS_UWP
using System.Drawing;
using WindowsPoint = Windows.Foundation.Point;
using WindowsSize = Windows.Foundation.Size;
using WindowsRect = Windows.Foundation.Rect;
using WindowsColor = Windows.UI.Color;

namespace Vortice.Mathematics;

public static class Extensions
{
    public static Point ToSystemPoint(this WindowsPoint point)
    {
        if (point.X > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(point.X));

        if (point.Y > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(point.Y));

        return new Point((int)point.X, (int)point.Y);
    }

    public static PointF ToSystemPointF(this WindowsPoint point) =>
        new((float)point.X, (float)point.Y);

    public static WindowsPoint ToPlatformPoint(this Point point) =>
        new(point.X, point.Y);

    public static WindowsPoint ToPlatformPoint(this PointF point) =>
        new(point.X, point.Y);


    public static Size ToSystemSize(this WindowsSize size)
    {
        if (size.Width > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(size.Width));

        if (size.Height > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(size.Height));

        return new Size((int)size.Width, (int)size.Height);
    }

    public static SizeF ToSystemSizeF(this WindowsSize size) =>
        new((float)size.Width, (float)size.Height);

    public static WindowsSize ToPlatformSize(this Size size) =>
        new(size.Width, size.Height);

    public static WindowsSize ToPlatformSize(this SizeF size) =>
        new(size.Width, size.Height);

    public static Rectangle ToSystemRect(this WindowsRect rect)
    {
        if (rect.X > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(rect.X));

        if (rect.Y > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(rect.Y));

        if (rect.Width > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(rect.Width));

        if (rect.Height > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(rect.Height));

        return new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
    }

    public static RectangleF ToSystemRectF(this WindowsRect rect) =>
        new RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);

    public static WindowsRect ToPlatformRect(this Rectangle rect) =>
        new WindowsRect(rect.X, rect.Y, rect.Width, rect.Height);

    public static WindowsRect ToPlatformRect(this RectangleF rect) =>
        new WindowsRect(rect.X, rect.Y, rect.Width, rect.Height);

    public static Color ToSystemColor(this WindowsColor color) =>
            new Color(color.R, color.G, color.B, color.A);

    public static WindowsColor ToPlatformColor(this Color color) =>
           WindowsColor.FromArgb(color.A, color.R, color.G, color.B);

    public static System.Drawing.Color ToSystemDrawingColor(this WindowsColor color) =>
            System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);

    public static WindowsColor ToPlatformColor(this System.Drawing.Color color) =>
           WindowsColor.FromArgb(color.A, color.R, color.G, color.B);
}
#endif