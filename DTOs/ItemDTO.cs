namespace MicroTransation.DTOs
{
    public class ItemDTO
    {
        public class ItemGetDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int price { get; set; }
        }

        public class ItemCreateDTO
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int price { get; set; }
        }

        public class ItemUpdateDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int price { get; set; }
        }
        public class ItemUpdateNameDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class ItemUpdateDescriptionDTO
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }
        public class ItemUpdatepriceDTO
        {
            public int Id { get; set; }
            public int price { get; set; }
        }
    }
}
