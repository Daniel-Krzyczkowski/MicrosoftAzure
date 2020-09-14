namespace ExcelFileContentExtractor.Core.Model
{
    public class ExcelFileRawDataModel : IEntity
    {
        public string Id { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public decimal CarPrice { get; set; }
        public int CarAvailability { get; set; }
    }
}
