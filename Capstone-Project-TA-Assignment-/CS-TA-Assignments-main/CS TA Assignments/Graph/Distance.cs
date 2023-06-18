using System;
using System.Collections.Generic;

namespace cwu.cs.TaAssignments
{
    /// <summary>
    /// Represents the distance of a vertex v.
    /// It stores the sum of edge weights from start to v and the number of edges from start to v.
    /// That allows to break ties between equal-weight paths based on number of edges used.
    /// </summary>
    public struct Distance
    {
        /// <summary>
        /// The sum of edge weights from start to v.
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// The number of edges from start to v
        /// </summary>
        public int Edges { get; set; }

        /// <summary>
        /// States if the current distance is considered infinite.
        /// </summary>
        public bool IsInfinite => Edges == int.MaxValue || Weight == int.MaxValue;


        /// <summary>
        /// Represents the maximum possible distance.
        /// </summary>
        public static Distance Infinite
        {
            get
            {
                return new Distance { Edges = int.MaxValue, Weight = int.MaxValue };
            }
        }

        /// <summary>
        /// Represents distance zero (sets both internal values to 0).
        /// </summary>
        public static Distance Zero
        {
            get
            {
                return new Distance { Edges = 0, Weight = 0 };
            }
        }


        /// <summary>
        /// Adds an edge with the given distance to the current distance.
        /// </summary>
        public static Distance operator +(Distance lhs, int rhs)
        {
            return new Distance
            {
                Weight = lhs.Weight + rhs,
                Edges = lhs.Edges + 1
            };
        }

        /// <summary>
        /// Compares two distances.
        /// Is true if the left-hand side is strictly smaller than the right-hand side.
        /// </summary>
        public static bool operator <(Distance lhs, Distance rhs)
        {
            return
                lhs.Weight < rhs.Weight ||
                (
                    lhs.Weight == rhs.Weight &&
                    lhs.Edges < rhs.Edges
                );
        }

        /// <summary>
        /// Compares two distances.
        /// Is true if the left-hand side is strictly larger than the right-hand side.
        /// </summary>
        public static bool operator >(Distance lhs, Distance rhs)
        {
            return !(lhs <= rhs);
        }

        /// <summary>
        /// Compares two distances.
        /// Is true if the left-hand side is smaller than or equal to the right-hand side.
        /// </summary>
        public static bool operator <=(Distance lhs, Distance rhs)
        {
            return
                lhs.Weight < rhs.Weight ||
                (
                    lhs.Weight == rhs.Weight &&
                    lhs.Edges <= rhs.Edges
                );
        }

        /// <summary>
        /// Compares two distances.
        /// Is true if the left-hand side is larger than or equal to the right-hand side.
        /// </summary>
        public static bool operator >=(Distance lhs, Distance rhs)
        {
            return !(lhs < rhs);
        }

    }
}
