using System;
using System.Linq;

namespace Checkmate.Detector.Domain.Pieces
{
    
    public enum  PieceKind
    {
        None, Pawn, Bishop, Knight, Rook, King
    }

    public static class PieceKindExtensions
    {
        private const string KNIGHT_NOTATION = "N";
        private const string KING_NOTATION = "K";
        public static PieceKind PieceKindFromInitial(this string letter)
        {
            if (string.Equals(letter, KING_NOTATION, StringComparison.CurrentCultureIgnoreCase))
            {
                return PieceKind.King;
            }
            if (string.Equals(letter, KNIGHT_NOTATION, StringComparison.CurrentCultureIgnoreCase))
            {
                return PieceKind.Knight;
            }
            var kindName = Enum.GetNames(typeof(PieceKind))
                .FirstOrDefault(x => x.StartsWith(letter));
            
            if(kindName == null)
            {
                return PieceKind.None;
            }
            return Enum.Parse<PieceKind>(kindName);
        }
        public static string GetInitial(this PieceKind kind)
        {
            if (kind == PieceKind.King)
            {
                return KING_NOTATION;
            }
            if (kind == PieceKind.Knight)
            {
                return KNIGHT_NOTATION;
            }
            return kind.ToString().Substring(0, 1);
        }
    }
    
}
