using System;
using System.Collections.Generic;
using System.Linq;
using MasterDetail.Core.Model;

namespace MasterDetail.Core.Helper
{
    public class EnumerableLearningSequences
    {
        public IEnumerable<int> BuildIntSequence()
        {
            var values = Enumerable.Repeat(-1, 10);
            return values;
        }

        public IEnumerable<string> BuildStringSequence()
        {
            var rand = new Random();
            var values = Enumerable.Range(0,10)
                                    .Select(i => ((char)('A' + rand.Next(0,26))).ToString());
            return values;
        }

        public IEnumerable<Person> BuildPersonSequence()
        {
            var values = Enumerable.Repeat(new Person("a", "b", "c", DateTime.UtcNow), 10);
            return values;
        }

        public IEnumerable<int> CompareSequencesIntersect()
        {
            var sequ1 = Enumerable.Range(0, 10);
            var sequ2 = Enumerable.Range(0, 10).Select(i => i * i);

            return Enumerable.Intersect(sequ1, sequ2);
            //return sequ1.Intersect(sequ2);
        }
        public IEnumerable<int> CompareSequencesExcept()
        {
            var sequ1 = Enumerable.Range(0, 10);
            var sequ2 = Enumerable.Range(0, 10).Select(i => i * i);

            return sequ2.Except(sequ1);
        }
        public IEnumerable<int> CompareSequencesConcat()
        {
            var sequ1 = Enumerable.Range(0, 10);
            var sequ2 = Enumerable.Range(0, 10).Select(i => i * i);

            return sequ2.Concat(sequ1);
        }
    }
}
