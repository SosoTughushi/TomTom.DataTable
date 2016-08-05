namespace TomTom.DataTable
{

    public class SelectListItem
    {
        public string Text { get; set; }

        public string Value { get; set; }

        public SelectListItem()
        {

        }

        public SelectListItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

    }
}
