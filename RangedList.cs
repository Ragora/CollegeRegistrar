using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrarConsole
{
    /// <summary>
    /// A ranged list is a list where a range of keys may be mapped to a single value.
    /// </summary>
    /// <typeparam name="StoredType"></typeparam>
    public class RangedList<StoredType>
    {
        /// <summary>
        /// A class representing an index in the ranged list.
        /// </summary>
        public class PayloadIndex
        {
            /// <summary>
            /// Minimum key range.
            /// </summary>
            public DateTime Minimum;

            /// <summary>
            /// Maximum key range.
            /// </summary>
            public DateTime Maximum;

            /// <summary>
            /// Stored value.
            /// </summary>
            public StoredType Value;
        };

        /// <summary>
        /// A list of indices in the ranged list.
        /// </summary>
        public List<PayloadIndex> Payload;

        /// <summary>
        /// Constructor to build a new ranged list.
        /// </summary>
        public RangedList()
        {
            Payload = new List<PayloadIndex>();
        }

        /// <summary>
        /// Add a value range to the ranged list.
        /// </summary>
        /// <param name="minimum">
        /// Minimum key range to map.
        /// </param>
        /// <param name="maximum">
        /// Maximum key range to map.
        /// </param>
        /// <param name="value">
        /// The value map.
        /// </param>
        public void AddRange(DateTime minimum, DateTime maximum, StoredType value)
        {
            PayloadIndex result = new PayloadIndex()
            {
                Minimum = minimum,
                Maximum = maximum,
                Value = value,
            };

            List<PayloadIndex> temporary = new List<PayloadIndex>(Payload);
            foreach (PayloadIndex index in temporary)
                if (minimum >= index.Minimum && maximum <= index.Maximum)
                    continue;
                else
                {
                    int listIndex = Payload.IndexOf(index);
                    Payload.Insert(listIndex + 1, result);
                }

           Payload.Add(result);
        }

        /// <summary>
        /// Gets the data stored at the input index. If it falls into some numeric range, that
        /// index data is returned. Null otherwise.
        /// </summary>
        /// <param name="desiredIndex">
        /// The index to lookup.
        /// </param>
        /// <returns>
        /// A PayLoadIndex if the index maps to something. Null otherwise.
        /// </returns>
        public PayloadIndex GetIndex(DateTime desiredIndex)
        {
            foreach (PayloadIndex index in Payload)
                if (desiredIndex <= index.Maximum && desiredIndex >= index.Minimum)
                    return index;

            return null;
        }
    }
}
