namespace _01_ExclusiveQuery.Contracts.Product
{
    public class ProductPictureQueryModel
    {
        public int ProductId { get; set; }

        public string Picture { get; set; }

        public string PictureAlt { get; set; }

        public string PictureTitle { get; set; }

        public bool IsRemoved { get; set; }

    }
}
