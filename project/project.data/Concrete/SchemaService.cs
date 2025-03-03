namespace project.data.Concrete{
    public class SchemaService{
        private static string _schema;

        public string GetSchema() => _schema ?? "Bilgitas";

        public void SetSchema(string schema)
        {
            _schema = schema;
        }
    }
}
