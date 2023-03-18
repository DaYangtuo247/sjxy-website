using System;

#nullable enable

namespace DevelopServer.Parse
{
    public abstract class ParserBase
    {
        private bool _done = false;
        protected int CurrentPosition { get; set; }
        public string Source { get; }

        public ParserBase(string source)
        {
            Source = source;
        }

        // Return true if eof.
        protected bool ReadSpace()
        {
            while (CurrentPosition < Source.Length && char.IsWhiteSpace(Source[CurrentPosition]))
                CurrentPosition++;

            return CurrentPosition == Source.Length;
        }

        protected int ReadSpaceAndReturnSpaceCount()
        {
            var count = 0;
            while (CurrentPosition < Source.Length && char.IsWhiteSpace(Source[CurrentPosition]))
            {
                count++;
                CurrentPosition++;
            }

            return count;
        }

        protected string Read(int count)
        {
            ReadSpace();

            if (CurrentPosition + count > Source.Length)
                throw new Exception("Unexpected end of source.");

            string result = Source.Substring(CurrentPosition, count);

            CurrentPosition += count;

            return result;
        }

        protected string? ReadWithoutAdvance(int count)
        {
            ReadSpace();

            if (CurrentPosition + count > Source.Length)
                return null;

            string result = Source.Substring(CurrentPosition, count);

            return result;
        }

        protected string ReadWord()
        {
            if (ReadSpace())
                throw new Exception("Unexpected end of source.");

            int start = CurrentPosition;

            while (CurrentPosition < Source.Length && (char.IsLetterOrDigit(Source[CurrentPosition]) || Source[CurrentPosition] == '_'))
                CurrentPosition++;

            return Source.Substring(start, CurrentPosition - start);
        }

        protected string ReadUntil(string until, bool considerParenthesis = true)
        {
            int bracketStack = 0;

            if (ReadSpace())
                throw new Exception("Unexpected end of source.");

            int start = CurrentPosition;

            while (CurrentPosition < Source.Length && !(Source.AsSpan(CurrentPosition).StartsWith(until) && (!considerParenthesis || bracketStack == 0)))
            {
                if (considerParenthesis && Source[CurrentPosition] == '(')
                    bracketStack++;
                else if (considerParenthesis && Source[CurrentPosition] == ')')
                    bracketStack--;
                CurrentPosition++;
            }

            return Source.Substring(start, CurrentPosition - start);
        }

        protected abstract void DoParse();

        public void Parse()
        {
            if (_done) return;

            DoParse();

            _done = true;
        }
    }
}
