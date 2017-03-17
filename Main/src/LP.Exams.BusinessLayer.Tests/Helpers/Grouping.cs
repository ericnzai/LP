using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LP.Exams.BusinessLayer.Tests.Helpers
{
    internal sealed class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
    {
        private readonly TKey _key;
        private readonly IEnumerable<TElement> _elements;

        public Grouping()
        {
            
        }

        public Grouping(TKey key, IEnumerable<TElement> elements)
        {
            if (elements == null) throw new ArgumentNullException("elements");

            _key = key;
            _elements = elements;
        }

        public TKey Key
        {
            get { return _key; }
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
