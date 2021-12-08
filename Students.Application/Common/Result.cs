namespace Students.Application.Common
{
    public class Result<T>
    {
        public bool Ok { get; set; }
        public T Response { get; set; }
        public string ExceptionMessage { get; set; }

    }
}
