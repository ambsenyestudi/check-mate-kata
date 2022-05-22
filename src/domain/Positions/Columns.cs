using System;

namespace Checkmate.Detector.Domain.Positions
{
    public enum Columns
    {
        None, A,B,C,D,E,F,G,H
    }
    public static class ColumnExtension
    {
        public static Columns Add(this Columns input, int offset)
        {
            var result = input + offset;
            if (!Enum.IsDefined(typeof(Columns), result))
            {
                return Columns.None;
            }
            return (Columns)result;
        }
    }
}
