namespace GivskudZoo.Model   //In the folder GivskudZoo we have the models of the web application
{
    public enum FieldsEnum
    {
        NoField = 0,
        Title,
        ShortDescription,
        Description,
        Author
    }

    public enum OrderByFieldsEnum
    {
        NoField = 0,
        Title,
        CreationDate,
        LastUpdateDate
    }

    public abstract class FilterBaseDto
    {
        public FieldsEnum Field { get; set; }
        public int FieldInt => (int)Field;
        public OrderByFieldsEnum OrderByField { get; set; }
        public int OrderByFieldInt => (int)OrderByField;
        public string Query { get; set; }
        public virtual bool? OrderByDescending { get; set; }
    }

    public class FilterDto : FilterBaseDto
    {
    }

    public class ComplexFilterDto : FilterBaseDto
    {
        public new bool OrderByDescending { get; set; }
    }
}