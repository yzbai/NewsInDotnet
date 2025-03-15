using System.Collections;

using NewsInCSharp.Accessories;

namespace NewsInCSharp
{
    class Enumerables : IRunable
    {
        public void Run()
        {
            TestEnumerable();
            TestAsyncEnumerable();
        }

        private void TestAsyncEnumerable()
        {

            //Task.WhenEach()

        }

        private void TestEnumerable()
        {
            RandomRiver rr = new RandomRiver();

            foreach (var word in rr)
            {
                Console.WriteLine(word);
            }
        }

        class RandomRiver : IEnumerable<string>
        {
            private List<string> _buffer = new List<string>(10);

            public RandomRiver()
            {
                _buffer.AddRange(Enumerable.Range(0, 10).Select(_ => Utils.GetRandomWords()));
            }

            public IEnumerator<string> GetEnumerator()
            {
                return new RandomRiverEnumerator(_buffer);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            class RandomRiverEnumerator : IEnumerator<string>
            {
                private List<string>? _buffer;
                private int _index = -1;
                private string? _current;

                public RandomRiverEnumerator(List<string> _buffer)
                {
                    this._buffer = _buffer;
                    _index = -1;
                    _current = default;
                }

                public string Current => _current!;

                public bool MoveNext()
                {
                    _index++;
                    _current = _buffer![_index];
                    return _index < _buffer!.Count - 1;
                }

                public void Reset()
                {
                    _index = -1;
                }

                object IEnumerator.Current => Current;

                public void Dispose()
                {
                    _buffer = null;
                }
            }
        }
    }
}
