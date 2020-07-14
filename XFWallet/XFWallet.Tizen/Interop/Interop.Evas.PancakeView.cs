using System;
using System.Runtime.InteropServices;

namespace Xamarin.Forms.PancakeView.Tizen
{
    internal static class Libraries
    {
        internal const string Evas = "libevas.so.1";
    }
    internal static partial class Interop
    {
        internal static partial class Evas
        {
            [DllImport(Libraries.Evas)]
            internal static extern IntPtr evas_map_new(int count);

            [DllImport(Libraries.Evas)]
            internal static extern void evas_map_util_points_populate_from_object_full(IntPtr map, IntPtr obj, int z);

            [DllImport(Libraries.Evas)]
            internal static extern void evas_map_point_color_set(IntPtr map, int idx, int r, int g, int b, int a);

            [DllImport(Libraries.Evas)]
            internal static extern void evas_object_map_set(IntPtr obj, IntPtr map);

            [DllImport(Libraries.Evas)]
            internal static extern void evas_map_util_points_color_set(IntPtr map, int r, int g, int b, int a);

            [DllImport("libevas.so.1")]
            internal static extern void evas_object_map_enable_set(IntPtr obj, bool enabled);

            [DllImport(Libraries.Evas)]
            internal static extern int evas_map_count_get(IntPtr map);
        }
    }
}
