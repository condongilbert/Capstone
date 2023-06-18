using System;
using System.Collections.Generic;


namespace cwu.cs.TaAssignments
{
    partial class Section : IComparable<Section>
    {
        public string title;
        public Instructor instructor;

        public int ID;
        public int number;
        public string section;
        public string room;

        // Each bit represents one timeslot.
        // Least significant bit represents Monday 8am.
        // Most significant bit represents Thursday 3pm.
        public Time labTimes;

        public bool inEburg;
        public bool inSamLab;

        private Section() { }

        public int CompareTo(Section other)
        {
            if (this.number != other.number)
            {
                return this.number.CompareTo(other.number);
            }

            if (this.section != other.section)
            {
                return this.section.CompareTo(other.section);
            }

            return 0;
        }

        public override bool Equals(object obj)
        {
            return CompareTo(obj as Section) == 0;
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}
