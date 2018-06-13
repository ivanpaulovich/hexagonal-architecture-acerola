namespace Acerola.Domain.ValueObjects
{
    public class PIN
    {
        public string _text { get; private set; }

        public PIN(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new PINShouldNotBeEmptyException("The 'PIN' field is required");

            this._text = text;
        }

        public override string ToString()
        {
            return _text.ToString();
        }

        public static implicit operator PIN(string text)
        {
            return new PIN(text);
        }
    }
}
