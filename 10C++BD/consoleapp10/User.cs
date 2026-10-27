namespace DapperExample
{
    internal class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public object Id { get; internal set; }
    }
}