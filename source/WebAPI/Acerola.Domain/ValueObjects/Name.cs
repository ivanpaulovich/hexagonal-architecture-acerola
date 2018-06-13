namespace Acerola.Domain.ValueObjects
{
    public class Name
    {
        private string _text;

        public Name(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new NameShouldNotBeEmptyException("The 'Name' field is required");

            this._text = text;
        }

        public override string ToString()
        {
            return _text.ToString();
        }

        public static implicit operator Name(string text)
        {
            return new Name(text);
        }
    }
}
