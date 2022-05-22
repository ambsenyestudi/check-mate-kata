using System;

namespace Checkmate.Detector.Domain.Pieces
{
    public enum  PieceKind
    {
        None, Pawn, Bishop, Knight, Rook
    }

    public static class PieceKindExtensions
    {
        public static PieceKind PieceKindFromInitial(this string letter)
        {
            var result = PieceKind.None;
            foreach (var item in Enum.GetValues(typeof(PieceKind)))
            {
                if(item.ToString().StartsWith(letter))
                {
                    result = (PieceKind)item;
                }
            }
            return result;
        }
        public static string GetInitial(this PieceKind kind)
        {
            return kind.ToString().Substring(0, 1);
        }
    }
    
}
