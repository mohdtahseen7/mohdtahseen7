namespace GOLDLOAN.ModelClass.DateFormat
{
    public class RemoveSpecialCharacter
    {

        public static dynamic removeCharacter(string a)
        {
            var replacechar = a.Replace("[", " ").Replace("]", "").Replace("\"", "").Replace("~", "").Replace("`", "").Replace("&", "").Replace("$", "").Replace("*", "").Replace("^", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", "").Replace("=", "").Replace("+", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("|", "").Replace("@", "").Replace("%", "").Replace("!", "").Replace("{", "").Replace("}", "").Replace(":", "").Replace(";", "").Replace("'", "").Replace(".", "").Replace("/","").TrimEnd();

            return replacechar;

        }

        public static dynamic removeSpecialCharname(string b)
        {
            var name = b.Replace("[", " ").Replace("]", "").Replace("\"", "").Replace("\"", "").Replace("~", "").Replace("`", "").Replace("&", "").Replace("$", "").Replace("*", "").Replace("^", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", "").Replace("=", "").Replace("+", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("|", "").Replace("@", "").Replace("%", "").Replace("!", "").Replace("{", "").Replace("}", "").Replace(":", "").Replace(";", "").Replace("'", "").Replace(".", "").Replace("#", "").Replace(",", "").Replace("/", "").TrimStart(new char[] { '0', '1', '2' }).TrimEnd();
            return name;
        }


    }
}
